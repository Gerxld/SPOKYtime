using MySql.Data.MySqlClient;
using SpookyTime.Datos;
using SpookyTime.Entidades;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpookyTime
{
    public partial class PantallaAdministrador : Form
    {
       
        public PantallaAdministrador()
        {
            InitializeComponent();
            this.Load += new EventHandler(PantallaAdministrador_Load);
            
            this.Load += new EventHandler(PantallaAdministrador_Load);
        }

        private void PantallaAdministrador_Load(object sender, EventArgs e)
        {
            CargarParadas();
            CargarCaramelos();
           
        }

        private void btnRegistrarInvitado_Click(object sender, EventArgs e)
        {
            string nombre = txtRPnombre.Text.Trim();
            string contrasena = txtRPcontra.Text.Trim();

            if (RegistrarUsuario(nombre, contrasena))
            {
                MessageBox.Show("Usuario registrado exitosamente.");
                // Aquí podrías hacer una consulta para obtener el código de invitación
                MostrarCodigoInvitacion(nombre);
            }
            else
            {
                MessageBox.Show("Error al registrar el usuario.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool RegistrarUsuario(string nombre, string contrasena)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "INSERT INTO Usuarios (TipoUsuario, Nombre, Contrasena) VALUES ('Cliente', @nombre, @contrasena)";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int rowsAffected = cmd.ExecuteNonQuery();
                    return rowsAffected > 0; // Retorna true si se insertó correctamente
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al registrar el usuario: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void MostrarCodigoInvitacion(string nombre)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT CodigoInvitacion FROM Usuarios WHERE Nombre = @nombre";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    var codigoInvitacion = cmd.ExecuteScalar();
                    if (codigoInvitacion != null)
                    {
                        lblCodigoGenerado.Text = codigoInvitacion.ToString();
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el código de invitación: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnCrearParada_Click(object sender, EventArgs e)
        {
            List<int> caramelosIds = ObtenerCaramelosSeleccionados();
            int cantidadInicial = ObtenerCantidadInicial();

            if (caramelosIds.Count == 0)
            {
                MessageBox.Show("Por favor, selecciona al menos un caramelo.");
                return;
            }

            if (CrearParada(caramelosIds, cantidadInicial))
            {
                MessageBox.Show("Parada creada exitosamente.");
                CargarParadas(); // Refrescar la lista de paradas
            }
            else
            {
                MessageBox.Show("Error al crear la parada.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool CrearParada(List<int> caramelosIds, int cantidadInicial)
        {
            using (var conexion = new Conexion())
            {
                MySqlTransaction transaction = null;

                try
                {
                    conexion.Abrir();
                    transaction = conexion.GetCommand().Connection.BeginTransaction();

                    // Insertar la nueva parada
                    string queryParada = "INSERT INTO Paradas (CantidadInicial) VALUES (@cantidadInicial)";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = queryParada;
                    cmd.Parameters.AddWithValue("@cantidadInicial", cantidadInicial);
                    cmd.ExecuteNonQuery();

                    // Obtener el ID de la parada recién creada
                    int idParada = (int)cmd.LastInsertedId;

                    // Insertar las relaciones en la tabla Caramelos_Paradas
                    string insertRelacionQuery = "INSERT INTO Caramelos_Paradas (IdCaramelo, IdParada) VALUES (@idCaramelo, @idParada)";

                    foreach (var idCaramelo in caramelosIds)
                    {
                        var cmdRelacion = conexion.GetCommand(); // Crear nuevo comando para cada caramelo
                        cmdRelacion.CommandText = insertRelacionQuery;

                        // Limpiar cualquier parámetro existente antes de agregar uno nuevo
                        cmdRelacion.Parameters.Clear();

                        // Agregar los nuevos parámetros
                        cmdRelacion.Parameters.AddWithValue("@idCaramelo", idCaramelo);
                        cmdRelacion.Parameters.AddWithValue("@idParada", idParada);

                        // Ejecutar el comando
                        cmdRelacion.ExecuteNonQuery();
                    }

                    transaction.Commit();
                    return true;
                }
                catch (Exception ex)
                {
                    transaction?.Rollback();
                    MessageBox.Show("Error al crear la parada: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }


        private void btnEliminarParada_Click(object sender, EventArgs e)
        {
            if (lsbParadas.SelectedItem != null)
            {
                int idParada = ObtenerIdParadaSeleccionada(); // Implementa este método para obtener el Id de la parada seleccionada

                if (EliminarParada(idParada))
                {
                    MessageBox.Show("Parada eliminada exitosamente.");
                    CargarParadas(); // Cargar la lista de paradas después de la eliminación
                }
                else
                {
                    MessageBox.Show("Error al eliminar la parada.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona una parada para eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EliminarParada(int idParada)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "DELETE FROM Paradas WHERE IdParada = @idParada";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@idParada", idParada);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar la parada: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnRellenar_Click(object sender, EventArgs e)
        {
            string tipoCaramelo = txtNombreCandy.Text.Trim();
            int cantidadInicial;

            if (int.TryParse(txtCantidadInicial.Text, out cantidadInicial))
            {
                if (AgregarCaramelo(tipoCaramelo, cantidadInicial))
                {
                    MessageBox.Show("Caramelo agregado exitosamente.");
                    CargarCaramelos(); // Cargar la lista de caramelos después de la inserción
                }
                else
                {
                    MessageBox.Show("Error al agregar el caramelo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, ingresa una cantidad válida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool AgregarCaramelo(string tipoCaramelo, int cantidadInicial)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "INSERT INTO Caramelos (TipoCaramelo, CantidadInicial) VALUES (@tipoCaramelo, @cantidadInicial)";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@tipoCaramelo", tipoCaramelo);
                    cmd.Parameters.AddWithValue("@cantidadInicial", cantidadInicial);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al agregar el caramelo: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            Caramelo carameloSeleccionado = ObtenerCarameloSeleccionado();
            if (carameloSeleccionado != null)
            {
                int cantidadActualizar;

                if (int.TryParse(txtCantidadActualizar.Text, out cantidadActualizar))
                {
                    if (ActualizarCaramelo(carameloSeleccionado.Id, cantidadActualizar))
                    {
                        MessageBox.Show("Caramelo actualizado exitosamente.");
                        CargarCaramelos(); // Cargar la lista de caramelos después de la actualización
                    }
                    else
                    {
                        MessageBox.Show("Error al actualizar el caramelo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa una cantidad válida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un dulce para actualizar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }



        private bool ActualizarCaramelo(int idCaramelo, int cantidadInicial)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "UPDATE Caramelos SET CantidadInicial = @cantidadInicial WHERE IdCaramelo = @idCaramelo";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@cantidadInicial", cantidadInicial);
                    cmd.Parameters.AddWithValue("@idCaramelo", idCaramelo);

                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el caramelo: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void CargarParadas()
         {
             lsbParadas.Items.Clear();
             using (var conexion = new Conexion())
             {
                 try
                 {
                     conexion.Abrir();
                     string query = "SELECT IdParada, CodigoParada FROM Paradas";
                     var cmd = conexion.GetCommand();
                     cmd.CommandText = query;

                     using (var reader = cmd.ExecuteReader())
                     {
                         while (reader.Read())
                         {
                             int idParada = reader.GetInt32(0);
                             string codigoParada = reader.GetString(1);
                             lsbParadas.Items.Add($"{codigoParada} (ID: {idParada})");
                         }
                     }
                 }
                 catch (Exception ex)
                 {
                     MessageBox.Show("Error al cargar paradas: " + ex.Message);
                 }
                 finally
                 {
                     conexion.Cerrar();
                 }
             }
         }

       

        private void CargarCaramelos()
        {
            clbCaramelos.Items.Clear();
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT IdCaramelo, TipoCaramelo, CantidadInicial FROM Caramelos";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCaramelo = reader.GetInt32(0);
                            string tipoCaramelo = reader.GetString(1);
                            int cantidadInicial = reader.GetInt32(2);

                            // Creamos un objeto Caramelo
                            Caramelo caramelo = new Caramelo { Id = idCaramelo, Tipo = tipoCaramelo, Cantidad = cantidadInicial };

                            // Lo agregamos al CheckedListBox
                            clbCaramelos.Items.Add(caramelo);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar caramelos: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }
     

        private List<int> ObtenerCaramelosSeleccionados()
        {
            List<int> caramelosIds = new List<int>();

            foreach (Caramelo caramelo in clbCaramelos.CheckedItems)
            {
                caramelosIds.Add(caramelo.Id); // Agregar el ID directamente
            }

            return caramelosIds;
        }


        private int ObtenerIdParadaSeleccionada()
        {
            if(lsbParadas.SelectedItem != null)
            {
                string selectedItem = lsbParadas.SelectedItem.ToString();
                string[] parts = selectedItem.Split(new[] { " (ID: " }, StringSplitOptions.None);
                if (parts.Length == 2)
                {
                    return int.Parse(parts[1].TrimEnd(')'));
                }
            }
            return -1;
        }

        private int ObtenerCantidadInicial()
        {
            int cantidadInicial;
            if(int.TryParse(txtCantidadInicial.Text, out cantidadInicial))
            {
                return cantidadInicial;
            }
            return 0;
        }

        private int ObtenerIdCarameloSeleccionado()
        {
            if (clbCaramelos.SelectedItem != null)
            {
                string selectedItem = clbCaramelos.SelectedItem.ToString();
                string[] parts = selectedItem.Split(new[] { "(Cantidad: ", ") (ID: " }, StringSplitOptions.None);

                if (parts.Length == 3)
                {
                    string idPart = parts[2].Trim();
                    if (int.TryParse(idPart, out int idCaramelo))
                    {
                        return idCaramelo;
                    }
                    else
                    {
                        MessageBox.Show("El ID del caramelo no tiene un formato válido.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("El formato del caramelo seleccionado no es válido.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            return -1;
        }

        private Caramelo ObtenerCarameloSeleccionado()
        {
            if (clbCaramelos.SelectedItem != null)
            {
                // Convertir el item seleccionado en un objeto Caramelo
                Caramelo carameloSeleccionado = (Caramelo)clbCaramelos.SelectedItem;
                return carameloSeleccionado;
            }

            return null;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaLogin pantallaLogin = new PantallaLogin();
            pantallaLogin.Show();
            this.Hide();
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Caramelo carameloSeleccionado = ObtenerCarameloSeleccionado();
            if (carameloSeleccionado != null)
            {
                // Confirmar la eliminación
                var confirmResult = MessageBox.Show("¿Estás seguro de que deseas eliminar este caramelo?",
                                                     "Confirmar eliminación",
                                                     MessageBoxButtons.YesNo,
                                                     MessageBoxIcon.Question);
                if (confirmResult == DialogResult.Yes)
                {
                    if (EliminarCaramelo(carameloSeleccionado.Id))
                    {
                        MessageBox.Show("Caramelo eliminado exitosamente.");
                        CargarCaramelos(); // Recargar la lista de caramelos
                    }
                    else
                    {
                        MessageBox.Show("Error al eliminar el caramelo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un caramelo para eliminar.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool EliminarCaramelo(int idCaramelo)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "DELETE FROM Caramelos WHERE IdCaramelo = @idCaramelo";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@idCaramelo", idCaramelo);
                    cmd.ExecuteNonQuery();
                    return true;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al eliminar el caramelo: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

    }
}

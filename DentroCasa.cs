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
    public partial class DentroCasa : Form
    {
        private int idParada;
        public DentroCasa(int idParada)
        {
            InitializeComponent();
            this.idParada = idParada;
            this.Load += new EventHandler(DentroCasa_Load);
        }

        private void DentroCasa_Load(object sender, EventArgs e)
        {
            lblCasa.Text = $"Casa {idParada}";
            CargarCaramelos();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarCaramelos()
        {
            clbACara.Items.Clear();
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = @"
                SELECT c.IdCaramelo, c.TipoCaramelo, c.CantidadInicial
                FROM Caramelos c
                JOIN Caramelos_Paradas cp ON c.IdCaramelo = cp.IdCaramelo
                WHERE cp.IdParada = @idParada";

                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@idParada", idParada);

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int idCaramelo = reader.GetInt32(0);
                            string tipoCaramelo = reader.GetString(1);
                            int cantidadInicial = reader.GetInt32(2);

                            Caramelos caramelo = new Caramelos
                            {
                                IdCaramelo = idCaramelo,
                                TipoCaramelo = tipoCaramelo,
                                CantidadInicial = cantidadInicial
                            };

                            clbACara.Items.Add(caramelo);
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

        private void btnAgarra_Click(object sender, EventArgs e)
        {
            if (clbACara.SelectedItem is Caramelos carameloSeleccionado)
            {
                if (int.TryParse(txtCantidadAgarra.Text, out int cantidadAgarra) && cantidadAgarra > 0)
                {
                    if (cantidadAgarra <= carameloSeleccionado.CantidadInicial)
                    {
                        int nuevaCantidad = carameloSeleccionado.CantidadInicial - cantidadAgarra;

                        // Actualizar la cantidad en la base de datos
                        ActualizarCantidadCaramelo(carameloSeleccionado.IdCaramelo, nuevaCantidad);

                        MessageBox.Show("Cantidad agarrada exitosamente.");
                        CargarCaramelos(); // Recargar la lista de caramelos
                    }
                    else
                    {
                        MessageBox.Show("No hay suficiente cantidad disponible.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa una cantidad válida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un caramelo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }


        private void ActualizarCantidadCaramelo(int idCaramelo, int nuevaCantidad)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "UPDATE Caramelos SET CantidadInicial = @nuevaCantidad WHERE IdCaramelo = @idCaramelo";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nuevaCantidad", nuevaCantidad);
                    cmd.Parameters.AddWithValue("@idCaramelo", idCaramelo);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    {
                        MessageBox.Show("Cantidad actualizada exitosamente.");
                    }
                    else
                    {
                        MessageBox.Show("No se encontró el registro del caramelo para actualizar.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar la cantidad del caramelo: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void VerificarCantidadCaramelo(int idCaramelo)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT CantidadInicial FROM Caramelos WHERE IdCaramelo = @idCaramelo";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@idCaramelo", idCaramelo);

                    int cantidad = Convert.ToInt32(cmd.ExecuteScalar());
                    MessageBox.Show($"Cantidad actual en la base de datos: {cantidad}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar cantidad: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaParadas pantallaParadas = new PantallaParadas();
            pantallaParadas.Show();
            this.Hide(); // Ocultar DentroCasa
        }

        private void btnRegresar_Click_1(object sender, EventArgs e)
        {
         PantallaParadas pantallaParadas = new PantallaParadas();
         pantallaParadas.Show();
         this.Hide(); // Ocultar DentroCasa
        }

        private void btnAgarra_Click_1(object sender, EventArgs e)
        {
            if (clbACara.SelectedItem is Caramelos carameloSeleccionado)
            {
                if (int.TryParse(txtCantidadAgarra.Text, out int cantidadAgarra) && cantidadAgarra > 0)
                {
                    if (cantidadAgarra <= carameloSeleccionado.CantidadInicial)
                    {
                        int nuevaCantidad = carameloSeleccionado.CantidadInicial - cantidadAgarra;

                        // Actualizar la cantidad en la base de datos
                        ActualizarCantidadCaramelo(carameloSeleccionado.IdCaramelo, nuevaCantidad);

                        MessageBox.Show("Cantidad agarrada exitosamente.");
                        CargarCaramelos(); // Recargar la lista de caramelos
                    }
                    else
                    {
                        MessageBox.Show("No hay suficiente cantidad disponible.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                else
                {
                    MessageBox.Show("Por favor, ingresa una cantidad válida.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un caramelo.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

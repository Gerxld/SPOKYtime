using SpookyTime.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpookyTime
{
    public partial class PantallaCliente : Form
    {
        public PantallaCliente()
        {
            InitializeComponent();
            this.Load += new EventHandler(PantallaCliente_Load);
        }
        private void PantallaCliente_Load(object sender, EventArgs e)
        {
            CargarParticipantes(); // Cargar participantes al iniciar el formulario
        }

        private void btnRegistroC_Click(object sender, EventArgs e)
        {
            string nombre = txtRCnombre.Text.Trim();
            string contrasena = txtRCcontra.Text.Trim();
            string nombreDisfraz = txtRCnombreD.Text.Trim();
            byte[] fotoDisfraz = ImageToByteArray(pcbDisfraz.Image); // Convertir la imagen a un byte array
            string codigoInvitacion = txtRCcodigo.Text.Trim(); // Obtener el código de invitación ingresado

            if (ValidarCredenciales(nombre, contrasena))
            {
                if (ValidarCodigoInvitacion(nombre, codigoInvitacion)) // Validar el código de invitación
                {
                    ActualizarUsuario(nombre, nombreDisfraz, fotoDisfraz);
                    MessageBox.Show("Datos actualizados exitosamente.");
                    CargarParticipantes(); // Cargar la lista de participantes después de la actualización
                }
                else
                {
                    MessageBox.Show("Código de invitación incorrecto.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Nombre o contraseña incorrectos.", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private bool ValidarCodigoInvitacion(string nombre, string codigoInvitacion)
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
                    var codigoDb = cmd.ExecuteScalar()?.ToString();

                    return codigoDb == codigoInvitacion; // Comparar el código ingresado con el de la base de datos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al validar el código de invitación: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private bool ValidarCredenciales(string nombre, string contrasena)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE Nombre = @nombre AND Contrasena = @contrasena";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al validar credenciales: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void ActualizarUsuario(string nombre, string nombreDisfraz, byte[] fotoDisfraz)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "UPDATE Usuarios SET NombreDisfraz = @nombreDisfraz, FotoDisfraz = @fotoDisfraz WHERE Nombre = @nombre";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombreDisfraz", nombreDisfraz);
                    cmd.Parameters.AddWithValue("@fotoDisfraz", fotoDisfraz);
                    cmd.Parameters.AddWithValue("@nombre", nombre);

                    cmd.ExecuteNonQuery();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al actualizar el usuario: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private byte[] ImageToByteArray(Image imageIn)
        {
            using (var ms = new MemoryStream())
            {
                imageIn.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return ms.ToArray();
            }
        }

        private void CargarParticipantes()
        {
            LsbParticipantes.Items.Clear();
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT Nombre, CodigoInvitacion FROM Usuarios WHERE TipoUsuario = 'Cliente'";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            string nombre = reader["Nombre"].ToString();
                            string codigoInvitacion = reader["CodigoInvitacion"].ToString();
                            LsbParticipantes.Items.Add($"{nombre} - {codigoInvitacion}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar participantes: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void LsbParticipantes_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (LsbParticipantes.SelectedItem != null)
            {
                string selectedItem = LsbParticipantes.SelectedItem.ToString();
                string nombre = selectedItem.Split('-')[0].Trim();
                MostrarFotoDisfraz(nombre);
            }
        }

        private void MostrarFotoDisfraz(string nombre)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT FotoDisfraz FROM Usuarios WHERE Nombre = @nombre";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    var fotoDisfraz = cmd.ExecuteScalar();

                    if (fotoDisfraz != null)
                    {
                        byte[] imageBytes = (byte[])fotoDisfraz;
                        using (var ms = new MemoryStream(imageBytes))
                        {
                            pcbDisfrazShow.Image = Image.FromStream(ms);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al mostrar la foto del disfraz: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private string ObtenerCodigoInvitacion(string nombre)
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
                    return cmd.ExecuteScalar()?.ToString();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al obtener el código de invitación: " + ex.Message);
                    return string.Empty;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnVotar_Click(object sender, EventArgs e)
        {
            if (LsbParticipantes.SelectedItem != null && int.TryParse(txtVoto.Text, out int voto) && voto >= 1 && voto <= 10)
            {
                string selectedItem = LsbParticipantes.SelectedItem.ToString();
                string nombre = selectedItem.Split('-')[0].Trim();
                GuardarVoto(nombre, voto);
            }
            else
            {
                MessageBox.Show("Por favor, selecciona un participante y un voto válido (1-5).", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GuardarVoto(string nombre, int voto)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "INSERT INTO Puntuaciones (IdUsuario, Puntuacion) SELECT IdUsuario, @voto FROM Usuarios WHERE Nombre = @nombre";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@voto", voto);

                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Voto guardado exitosamente.");
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al guardar el voto: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void btnVisitarParadas_Click(object sender, EventArgs e)
        {
            PantallaParadas pantallaParadas = new PantallaParadas();
            pantallaParadas.Show();
            this.Hide(); // Ocultar el formulario actual
        }

        private void pcbDisfraz_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog.InitialDirectory = "C:\\";
                openFileDialog.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp";
                openFileDialog.Title = "Selecciona una imagen";

                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Cargar la imagen seleccionada en el PictureBox
                    pcbDisfraz.Image = new Bitmap(openFileDialog.FileName);
                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaLogin pantallaLogin = new PantallaLogin();
            pantallaLogin.Show();
            this.Hide();
        }

        private void btnConcurso_Click(object sender, EventArgs e)
        {
            PantallaPodio pantallaPodio = new PantallaPodio();
            pantallaPodio.Show();
            this.Hide();
        }

        private void PantallaCliente_Load_1(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}

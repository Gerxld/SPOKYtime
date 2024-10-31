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
    public partial class PantallaLoginAdministrador : Form
    {
        public PantallaLoginAdministrador()
        {
            InitializeComponent();
            txtContraAdmin.UseSystemPasswordChar = true;
        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombreAdmin = txtUsuarioAdmin.Text.Trim();
            string contrasenaAdmin = txtContraAdmin.Text.Trim();


            if (VerificarCredenciales(nombreAdmin, contrasenaAdmin))
            {
                PantallaAdministrador pantallaAdministrador = new PantallaAdministrador();
                pantallaAdministrador.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Usuario o contraseña incorrectos", "ERROR", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

        }
        private bool VerificarCredenciales(string nombre, string contrasena)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT COUNT(*) FROM Usuarios WHERE nombre = @nombre AND contrasena = @contrasena AND TipoUsuario = 'Admin'";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    return count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar credenciales: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();


                }
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaLogin pantallaLogin = new PantallaLogin();
            pantallaLogin.Show();
            this.Hide();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void PantallaLoginAdministrador_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}



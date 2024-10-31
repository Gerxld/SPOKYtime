using SpookyTime.Datos;
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
    public partial class PantallaLoginCliente : Form
    {
        public PantallaLoginCliente()
        {
            InitializeComponent();
        }

        private void groupBox1_Enter(object sender, EventArgs e)
        {

        }

        private void btnIngresar_Click(object sender, EventArgs e)
        {
            string nombre = txtNombre.Text.Trim();
            string codigoInvitacion = txtCodigo.Text.Trim();
            string contrasena = txtContrasena.Text.Trim();

            // Validar que los campos no estén vacíos
            if (string.IsNullOrEmpty(nombre) || string.IsNullOrEmpty(codigoInvitacion) || string.IsNullOrEmpty(contrasena))
            {
                MessageBox.Show("Por favor, complete todos los campos.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            using (var conexion = new Conexion()) // Asumiendo que tienes una clase Conexion para manejar la base de datos
            {
                try
                {
                    conexion.Abrir();
                    string query = @"
                        SELECT COUNT(*)
                        FROM usuarios
                        WHERE Nombre = @nombre
                        AND CodigoInvitacion = @codigoInvitacion
                        AND Contrasena = @contrasena
                        AND TipoUsuario = 'Cliente'";

                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@codigoInvitacion", codigoInvitacion);
                    cmd.Parameters.AddWithValue("@contrasena", contrasena);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());

                    if (count > 0) // Usuario encontrado
                    {
                        // Mostrar el formulario PantallaCliente
                        PantallaCliente pantallaCliente = new PantallaCliente();
                        pantallaCliente.Show();
                        this.Hide(); // Oculta el formulario de login
                    }
                    else
                    {
                        MessageBox.Show("Credenciales incorrectas. Por favor, intente de nuevo.", "Acceso Denegado", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al intentar ingresar: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private void PantallaLoginCliente_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaLogin pantallaLogin = new PantallaLogin();
            pantallaLogin.Show();
            this.Hide();
        }
    }
}

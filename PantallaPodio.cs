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
    public partial class PantallaPodio : Form
    {
        public PantallaPodio()
        {
            InitializeComponent();
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaCliente pantallaCliente = new PantallaCliente();
            pantallaCliente.Show();
            this.Hide();
        }

        private void PantallaPodio_Load(object sender, EventArgs e)
        {
            CargarTopDisfraces();
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void CargarTopDisfraces()
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    // Consulta para obtener el top 3 de disfraces con los datos de la tabla usuarios
                    string query = @"
                        SELECT p.Puntuacion, u.Nombre AS NombreUsuario, u.NombreDisfraz, u.FotoDisfraz
                        FROM puntuaciones p
                        JOIN usuarios u ON p.IdUsuario = u.IdUsuario
                        ORDER BY p.Puntuacion DESC
                        LIMIT 3";

                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        // Leer y asignar el Top 1
                        if (reader.Read())
                        {
                            lblTop1.Text = $"{reader["NombreUsuario"]} - {reader["NombreDisfraz"]}";
                            pcbTop1.Image = ByteArrayToImage((byte[])reader["FotoDisfraz"]);
                        }

                        // Leer y asignar el Top 2
                        if (reader.Read())
                        {
                            lblTop2.Text = $"{reader["NombreUsuario"]} - {reader["NombreDisfraz"]}";
                            pcbTop2.Image = ByteArrayToImage((byte[])reader["FotoDisfraz"]);
                        }

                        // Leer y asignar el Top 3
                        if (reader.Read())
                        {
                            lblTop3.Text = $"{reader["NombreUsuario"]} - {reader["NombreDisfraz"]}";
                            pcbTop3.Image = ByteArrayToImage((byte[])reader["FotoDisfraz"]);
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al cargar el top de disfraces: " + ex.Message);
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

        private Image ByteArrayToImage(byte[] byteArrayIn)
        {
            using (var ms = new MemoryStream(byteArrayIn))
            {
                return Image.FromStream(ms);
            }
        }
    }
}

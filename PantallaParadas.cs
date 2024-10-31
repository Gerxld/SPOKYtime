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
    public partial class PantallaParadas : Form
    {
        public PantallaParadas()
        {
            InitializeComponent();
            CargarParadas();
        }

        private void CargarParadas()
        {
            flowLayoutPanel.Controls.Clear(); // Asegurarse de que el panel esté vacío
            btnCasa.Visible = false; // Ocultar el botón plantilla para que no se vea en el formulario

            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT IdParada FROM Paradas";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;

                    using (var reader = cmd.ExecuteReader())
                    {
                        Random rnd = new Random(); // Instancia para posiciones aleatorias

                        while (reader.Read())
                        {
                            int idParada = reader.GetInt32(0);

                            // Crear el nuevo botón basado en btnCasa
                            Button newBtnCasa = new Button
                            {
                                Width = btnCasa.Width,
                                Height = btnCasa.Height,
                                BackgroundImage = btnCasa.BackgroundImage,
                                BackgroundImageLayout = btnCasa.BackgroundImageLayout,
                                Tag = idParada, // Guardar el ID de la parada en el Tag
                                Name = $"btnCasa{idParada}"
                            };

                            // Verificar si hay caramelos en la parada y asignar eventos
                            if (HayCaramelosEnParada(idParada))
                            {
                                newBtnCasa.Click += BtnCasa_Click; // Asignar el evento Click solo si hay caramelos
                                newBtnCasa.Enabled = true; // Habilitar el botón
                            }
                            else
                            {
                                newBtnCasa.Enabled = false; // Deshabilitar el botón si no hay caramelos
                            }

                            // Añadir el nuevo botón al flowLayoutPanel
                            flowLayoutPanel.Controls.Add(newBtnCasa);

                            // Opcional: Crear y añadir una etiqueta para el ID de la parada debajo del botón
                            Label lblIdParada = new Label
                            {
                                Text = $"ID: {idParada}",
                                AutoSize = true
                            };
                            flowLayoutPanel.Controls.Add(lblIdParada);
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

        private bool HayCaramelosEnParada(int idParada)
        {
            using (var conexion = new Conexion())
            {
                try
                {
                    conexion.Abrir();
                    string query = "SELECT COUNT(*) FROM Caramelos_Paradas WHERE IdParada = @idParada";
                    var cmd = conexion.GetCommand();
                    cmd.CommandText = query;
                    cmd.Parameters.AddWithValue("@idParada", idParada);

                    int count = Convert.ToInt32(cmd.ExecuteScalar());
                    return count > 0; // Retorna true si hay caramelos
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al verificar caramelos en la parada: " + ex.Message);
                    return false;
                }
                finally
                {
                    conexion.Cerrar();
                }
            }
        }

            private void BtnCasa_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;
            int idParada = (int)btn.Tag; // Obtener el ID de la parada desde el Tag

            DentroCasa dentroCasa = new DentroCasa(idParada); // Pasar el idParada
            dentroCasa.Show();
            this.Hide(); // Ocultar PantallaParadas para abrir DentroCasa
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaCliente pantallaCliente = new PantallaCliente();
            pantallaCliente.Show();
            this.Hide();
        }

        private void PantallaParadas_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }
    }
}

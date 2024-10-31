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
    public partial class SCREAMER : Form
    {
        private ReproductorMusica reproductorMusica;
        private Timer timerParpadeo;
        private bool colorRojo;

        public SCREAMER()
        {
            InitializeComponent();
            reproductorMusica = new ReproductorMusica();
            timerParpadeo = new Timer();
            timerParpadeo.Interval = 200; // Intervalo de 0.20 segundos
            timerParpadeo.Tick += TimerParpadeo_Tick;
            timerParpadeo.Start();
        }
        private void TimerParpadeo_Tick(object sender, EventArgs e)
        {
            if (colorRojo)
            {
                this.BackColor = Color.White;
                colorRojo = false;
            }
            else
            {
                this.BackColor = Color.Red;
                colorRojo = true;
            }
        }

        private void btnRegresar_Click(object sender, EventArgs e)
        {
            PantallaLogin pantallaLogin = new PantallaLogin();
            pantallaLogin.Show();
            this.Hide();
            reproductorMusica.Stop(); // Detener la música al regresar
            timerParpadeo.Stop(); // Detener el parpadeo al regresar
        }

        private void SCREAMER_Load(object sender, EventArgs e)
        {
            string pathDelArchivo = "C:\\Users\\HP\\OneDrive - Universidad Tecnológica de Panamá\\Documentos\\Universidad\\2024 2°semestre\\D. Software IV\\Parcial 2 SPOOKY TIME\\ZZZ.mp3"; // Asegúrate de poner la ruta correcta aquí
            reproductorMusica.Play(pathDelArchivo);
        }

        private void SCREAMER_FormClosed(object sender, FormClosedEventArgs e)
        {
            reproductorMusica.Stop(); // Detener la música al cerrar el formulario
            timerParpadeo.Stop(); // Detener el parpadeo al cerrar
        }
    }
}

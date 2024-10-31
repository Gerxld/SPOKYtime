using SpookyTime.Datos;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SpookyTime
{
    public partial class PantallaLogin : Form
    {
        private ReproductorMusica reproductor;
        public PantallaLogin()
        {
            InitializeComponent();
            reproductor = new ReproductorMusica();

        }

        private void btnAdministrador_Click(object sender, EventArgs e)
        {
            PantallaLoginAdministrador pantallaLoginAdministrador = new PantallaLoginAdministrador();
            pantallaLoginAdministrador.Show();
            this.Hide();
        }

        private void PantallaLogin_Load(object sender, EventArgs e)
        {
            Conexion c = new Conexion();
            string musicFilePath = "C:\\Users\\HP\\OneDrive - Universidad Tecnológica de Panamá\\Documentos\\Universidad\\2024 2°semestre\\D. Software IV\\Parcial 2 SPOOKY TIME\\HALLOWEEN.mp3";
            reproductor.Play(musicFilePath);
            this.WindowState = FormWindowState.Maximized;
            this.FormBorderStyle = FormBorderStyle.None; // Opcional, solo si quieres ocultar los bordes
            this.StartPosition = FormStartPosition.CenterScreen;
        }

        private void btnCliente_Click(object sender, EventArgs e)
        {
            PantallaLoginCliente pantallaLoginCliente = new PantallaLoginCliente();
            pantallaLoginCliente.Show();
            this.Hide();
        }
        protected override void OnFormClosed(FormClosedEventArgs e)
        {
            reproductor.Stop(); // Detén la música cuando se cierra el formulario
            base.OnFormClosed(e);
        }

        private void btnBackDoor_Click(object sender, EventArgs e)
        {
            SCREAMER screamer = new SCREAMER();
            screamer.Show();
            this.Hide();
        }
    }
}

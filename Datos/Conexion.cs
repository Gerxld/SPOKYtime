using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
using System.Security.Cryptography.X509Certificates;
using System.Configuration;
using System.Data;


namespace SpookyTime.Datos
{
    public class Conexion : IDisposable
    {
        private MySqlConnection conexion;
        private MySqlCommand Cmd;
        private bool disposed = false;


        public Conexion()
        {
            Initialize();
        }

        private void Initialize()
        {
            conexion = new MySqlConnection(ConfigurationManager.ConnectionStrings["Conexion"].ConnectionString);
            Cmd = new MySqlCommand { Connection = conexion };
        }

        public void Abrir()
        {
            if (conexion.State == ConnectionState.Closed)
            {
                conexion.Open();
            }
        }


        public void Cerrar()
        {
            if (conexion.State == ConnectionState.Open)
            {
                conexion.Close();
            }
        }

        public MySqlCommand GetCommand()
        {
            return Cmd;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposed)
            {
                if (disposing)
                {
                    Cerrar();
                    conexion.Dispose();
                    Cmd.Dispose();
                }
                disposed = true;
            }
        }
    }
}




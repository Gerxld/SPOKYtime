using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpookyTime.Entidades
{
    internal class Usuarios
    {
        public int IdUsuario { get; set; }
        public TipoUsuario TipoUsuari { get; set; }
        public string Nombre { get; set; }
        public string Contrasena { get; set; }
        public string NombreDisfraz { get; set; }
        public string CodigoInvitacion { get; set; }
        public string FotoDisfraz { get; set; }

        public enum TipoUsuario
        {
            Administrador,
            Jugador
        }
    }
}

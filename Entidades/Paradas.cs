using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpookyTime.Entidades
{
    internal class Paradas
    {
        public int IdParada { get; set; }
        public int IdCaramelo { get; set; }
        public int CantidadInicial { get; set; }
        public int CantidadActual { get; set; }
        public string Codigoparada { get; set; }
        public List<int> CaramelosIds { get; set; } // Lista de Ids de Caramelos asociados

        public Paradas()
        {
            CaramelosIds = new List<int>();
        }
    }
}

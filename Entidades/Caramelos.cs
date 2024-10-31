using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpookyTime.Entidades
{
    internal class Caramelos
    {
        public int IdCaramelo { get; set; }
        public string TipoCaramelo { get; set; }
        public int CantidadInicial { get; set; }

        public override string ToString()
        {
            return $"{TipoCaramelo} (Cantidad: {CantidadInicial})";
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SpookyTime.Entidades
{
    public class Caramelo
    {
        public int Id { get; set; }
        public string Tipo { get; set; }
        public int Cantidad { get; set; }

        public override string ToString()
        {
            return $"{Tipo} (Cantidad: {Cantidad})";
        }
    }
}

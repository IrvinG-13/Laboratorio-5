using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_5
{
    internal class Entradas
    {
        public double Platino {get ;set;}
        public double Vip { get; set; }
        public double General { get; set; }

        public Entradas(double platino, double VIP, double general)
        {
            Platino = platino;
            Vip = VIP;
            General = general;
        }

    }
}

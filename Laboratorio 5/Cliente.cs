using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Laboratorio_5
{
    internal class Cliente
    {
        public string Nombre { get; set; }


        public Cliente(string nombre)
        {
            validarCampos(nombre);

            Nombre = nombre;

        }



        public void validarCampos(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
            {
                throw new ArgumentException("El nombre no puede quedar vacio");
            }
        }


    }


    
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EAutor
    {
        string claveAutor;

        string nombre;

        string apellido1;

        string apellido2;

        public EAutor()
        {
        }

        public EAutor(string claveAutor, string nombre, string apellido1, string apellido2)
        {
            this.claveAutor = claveAutor;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
        }

        public string ClaveAutor { get => claveAutor; set => claveAutor = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido1 { get => apellido1; set => apellido1 = value; }
        public string Apellido2 { get => apellido2; set => apellido2 = value; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ECategoria
    {
        string claveCategoria;

        string descripcion;

        public ECategoria()
        {
            this.claveCategoria = string.Empty;

            this.descripcion = string.Empty;
        }

        public ECategoria(string claveCategoria, string descripcion)
        {
            this.claveCategoria = claveCategoria;
            this.descripcion = descripcion;
        }

        public string ClaveCategoria { get; set;}
        public string Descripcion { get; set;}
    }
}

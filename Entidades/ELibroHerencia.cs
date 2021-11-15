using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ELibroHerencia
    {
        string claveEstado;

        string descripcion;

        public ELibroHerencia()
        {
        }

        public ELibroHerencia(string claveEstado, string descripcion)
        {
            this.claveEstado = claveEstado;
            this.descripcion = descripcion;
        }

        public string ClaveEstado { get => claveEstado; set => claveEstado = value; }
        public string Descripcion { get => descripcion; set => descripcion = value; }
    }
}

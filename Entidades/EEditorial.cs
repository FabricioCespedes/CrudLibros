using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EEditorial : ELibroHerencia
    {
        public EEditorial(string claveEstado, string descripcion) : base(claveEstado, descripcion)
        {
        }
    }
}

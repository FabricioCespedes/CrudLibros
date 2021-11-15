using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class ECondicion : ELibroHerencia
    {
        public ECondicion(string claveEstado, string descripcion) : base(claveEstado, descripcion)
        {
        }
    }
}

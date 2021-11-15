using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EEstado : ELibroHerencia
    {
        public EEstado(string claveEstado, string descripcion) : base(claveEstado, descripcion)
        {
        }
    }
}

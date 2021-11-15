using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EPrestamo
    {
        string clavePrestamo;

        EEjemplar eEjemplar;

        EUsuario eUsuario;

        DateTime fechaPrestamo;

        DateTime fechaDevolucion;

        public EPrestamo()
        {
        }

        public EPrestamo(string clavePrestamo, EEjemplar eEjemplar, EUsuario eUsuario, DateTime fechaPrestamo, DateTime fechaDevolucion)
        {
            this.clavePrestamo = clavePrestamo;
            this.eEjemplar = eEjemplar;
            this.eUsuario = eUsuario;
            this.fechaPrestamo = fechaPrestamo;
            this.fechaDevolucion = fechaDevolucion;
        }

        public string ClavePrestamo { get => clavePrestamo; set => clavePrestamo = value; }
        public DateTime FechaPrestamo { get => fechaPrestamo; set => fechaPrestamo = value; }
        public DateTime FechaDevolucion { get => fechaDevolucion; set => fechaDevolucion = value; }
        public  EEjemplar EEjemplar { get => eEjemplar; set => eEjemplar = value; }
        public  EUsuario EUsuario { get => eUsuario; set => eUsuario = value; }
    }
}

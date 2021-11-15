using System;
using System.Collections.Generic;
using System.Text;

namespace Entidades
{
    public class EUsuario
    {
        string claveUsuario;

        public override string ToString()
        {
            return nombre + " " + apellido1;
        }

        string curp;

        string nombre;

        string apellido1;

        string apellido2;

        DateTime fechaNacimiento;

        string email;

        string direccion;
        public EUsuario()
        {
        }

        public EUsuario(string claveUsuario, string curp, string nombre, string apellido1, string apellido2, DateTime fechaNacimiento, string email, string direccion)
        {
            this.claveUsuario = claveUsuario;
            this.curp = curp;
            this.nombre = nombre;
            this.apellido1 = apellido1;
            this.apellido2 = apellido2;
            this.fechaNacimiento = fechaNacimiento;
            this.email = email;
            this.direccion = direccion;
        }

        public string ClaveUsuario { get => claveUsuario; set => claveUsuario = value; }
        public string Curp { get => curp; set => curp = value; }
        public string Nombre { get => nombre; set => nombre = value; }
        public string Apellido1 { get => apellido1; set => apellido1 = value; }
        public string Apellido2 { get => apellido2; set => apellido2 = value; }
        public DateTime FechaNacimiento { get => fechaNacimiento; set => fechaNacimiento = value; }
        public string Email { get => email; set => email = value; }
        public string Direccion { get => direccion; set => direccion = value; }

    }
}

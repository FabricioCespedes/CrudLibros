using System;
using System.Collections.Generic;
using System.Text;
using AcessoDatos;
using Entidades;
namespace LogicaNegocio
{
    public class LNLibro
    {
        string mensaje;

        string cadConexion;

        public LNLibro()
        {
            mensaje = string.Empty;
            cadConexion = string.Empty;
        }

        public LNLibro(string mensaje, string cadConexion)
        {
            this.mensaje = mensaje;
            this.cadConexion = cadConexion;
        }


        public bool libroRepetido(ELibro libro)
        {
            bool result = false;

            ADLibro adLibro = new ADLibro(cadConexion);

            
            try
            {
                result = adLibro.libroRepetido(libro);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool claveLibroRepetida(string claveLibro)
        {
            bool result = false;
            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.claveLibroRepetido(claveLibro);
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }
    }
}

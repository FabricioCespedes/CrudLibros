using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AcessoDatos;
using Entidades;

namespace LogicaNegocio
{
    public class LNPrestamo
    {

        string cadConexion;

        public LNPrestamo(string cadConexion)
        {
            this.cadConexion = cadConexion;
        }

        public DataSet listarEjemplares(string condicion = "")
        {
            DataSet setLibros;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                setLibros = aDPrestamo.listarEjemplares(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setLibros;
        }


        public ELibro listarLibros(string condicion)
        {
            ELibro setLibros;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                setLibros = aDPrestamo.listarLibros(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setLibros;
        }

        public List<EUsuario> listarUsuarios(string condicion = "")
        {
            List<EUsuario> setUsuarios;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                setUsuarios = aDPrestamo.listarUsuarios(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setUsuarios;
        }

        public DataSet listarPrestamos(string condicion = "")
        {
            DataSet setPrestamos;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                setPrestamos = aDPrestamo.listarPrestamos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setPrestamos;
        }

        public int insertarPrestamos(EPrestamo ePrestamo)
        {
            int resultado;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                resultado = aDPrestamo.insertarPrestamos(ePrestamo);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }

        public int eliminar(EPrestamo ePrestamo)
        {
            int resultado;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                resultado = aDPrestamo.eliminar(ePrestamo);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return resultado;
        }

        public int modificar(EPrestamo ePrestamo, string claveVieja = "")
        {
            int resultado;

            ADPrestamo aDPrestamo = new ADPrestamo(cadConexion);

            try
            {
                resultado = aDPrestamo.modificar(ePrestamo);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return resultado;
        }
    }
}

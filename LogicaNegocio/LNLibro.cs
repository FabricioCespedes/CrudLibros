﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using AcessoDatos;
using Entidades;
namespace LogicaNegocio
{
    public class LNLibro
    {
        string mensaje;

        string cadConexion;



        public LNLibro( string cadConexion)
        {
            this.mensaje = string.Empty;
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

                throw ex;
            }
            return result;
        }

        public int insertarLibro(ELibro libro)
        {
            int resultado;

            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                resultado = adLibro.insertarLibro(libro);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }


        public DataSet listarTodos(string condicion = "")
        {
            DataSet setLibros;

            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                setLibros = adLibro.listarTodos(condicion);
            }
            catch (Exception ex)
            {

                throw ex;
            }

            return setLibros;
        }

        public bool claveAutorExiste(EAutor eAutor)
        {
            bool result = false;

            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                result = adLibro.claveAutorExiste(eAutor);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return result;
        }

        public bool claveCategoriaExiste(ECategoria eCategoria)
        {
            bool resultado;

            ADLibro adLibro = new ADLibro(cadConexion);

            try
            {
                resultado = adLibro.claveCategoriaExiste(eCategoria);
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return resultado;
        }
    }
}

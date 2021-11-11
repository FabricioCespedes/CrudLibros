using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
using System.Data;

namespace AcessoDatos
{
    public class ADLibro
    {
        string mensaje;

        string cadConexion;

        public ADLibro(string cadena)
        {
            this.mensaje = string.Empty;
            this.cadConexion = cadena;
        }

        public ADLibro()
        {
            this.mensaje = string.Empty;
            this.cadConexion = string.Empty;
        }
        public bool libroRepetido(ELibro libro)
        {
            bool result = false;

            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            SqlDataReader dataReader;

            comandoSQL.Connection = conexionSQL;

            comandoSQL.CommandText = $"SELECT 1 FROM LIBRO WHERE titulo='{libro.Titulo}' and claveAutor='{libro.ClaveAutor}'";

            try
            {
                conexionSQL.Open();

                dataReader = comandoSQL.ExecuteReader();
                result = dataReader.HasRows ? true : false;
                conexionSQL.Close();
            }
            catch (Exception)
            {
                conexionSQL.Close();
                throw new Exception("Se ha presentado un error realizando la consulta en el titulo del libro");
            }
            finally
            {
                comandoSQL.Dispose();
                conexionSQL.Dispose();
            }
            return result;
        }

        public bool claveLibroRepetido(string clave)
        {
            bool result = false;
            object obEscalar;
            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);
            //SqlDataReader dataReader;
            comandoSQL.CommandText = $"SELECT 1 FROM LIBRO WHERE claveLibro= @clave";
            comandoSQL.Parameters.AddWithValue("@clave", clave);
            comandoSQL.Connection = conexionSQL;
            try
            {
                conexionSQL.Open();
                obEscalar = comandoSQL.ExecuteScalar();
                if (obEscalar != null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
                conexionSQL.Close();

            }
            catch (Exception)
            {
                conexionSQL.Close();
                throw new Exception("Se ha presentado un error realizando la consulta del libro repetido");
            }
            finally
            {
                comandoSQL.Dispose();
                conexionSQL.Dispose();
            }
            return result;
        }

        public int insertarLibro(ELibro libro)
        {
            int resultado = -1;
            string sentencia = "INSERT INTO Libro(claveLibro,titulo,claveAutor,claveCategoria) " +
                "VALUES (@claveLibro,@titulo,@claveAutor,@claveCategoria) ";
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia,conexion);
            comando.Parameters.AddWithValue("@claveLibro", libro.ClaveLibro);
            comando.Parameters.AddWithValue("@titulo", libro.Titulo);
            comando.Parameters.AddWithValue("@claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("@claveCategoria", libro.ClaveCategoria.ClaveCategoria);

            try
            {
                conexion.Open();
                resultado = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("La consulta no se ha realizado correctamente");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return resultado;

        }

        public string Mensaje { get;}

        public DataSet listarTodos(string condicion = "")
        {
            DataSet setLibros = new DataSet();
            string sentecia = "SELECT [claveLibro],[titulo],[claveAutor],[claveCategoria] FROM [LIBRO]";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentecia = string.Format("{0} where {1}",sentecia,condicion);

                //sentencia = $"{sentencia} where {condicion}";
            }
            SqlConnection connection = new SqlConnection(cadConexion);
            SqlDataAdapter dataAdapter;

            try
            {
                dataAdapter = new SqlDataAdapter(sentecia, connection);
                dataAdapter.Fill(setLibros);
                connection.Close();
                dataAdapter.Dispose();
            }
            catch (Exception)
            {
                connection.Close();
                throw new Exception("Ha ocurrido un error en la selección de libros");
            }
            finally
            {
                connection.Dispose();                
            }

            return setLibros;
        }

        #region Constructores
        
        #endregion
    }
}

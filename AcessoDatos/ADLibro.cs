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
            comandoSQL.CommandText = "SELECT 1 FROM LIBRO WHERE claveLibro= @clave";
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

        public bool claveAutorExiste(EAutor eAutor)
        {
            bool result = false;

            SqlCommand comandoSQL = new SqlCommand();

            SqlConnection conexionSQL = new SqlConnection(cadConexion);

            SqlDataReader dataReader;

            comandoSQL.Connection = conexionSQL;

            comandoSQL.CommandText = $"SELECT 1 FROM AUTOR WHERE claveAutor='{eAutor.ClaveAutor}'";

            try
            {
                conexionSQL.Open();

                dataReader = comandoSQL.ExecuteReader();

                if(dataReader.HasRows) result = true;

                conexionSQL.Close();
            }
            catch (Exception)
            {
                conexionSQL.Close();

                throw new Exception("Se ha presentado un error realizando la consulta para verificar si existe un autor");
            }
            finally
            {
                comandoSQL.Dispose();

                conexionSQL.Dispose();
            } 
            return result;
        }

        public bool claveCategoriaExiste(ECategoria eCategoria)
        {
            bool resultado = false;
            object obEscalar;
            SqlCommand comandoSQL = new SqlCommand();
            SqlConnection conexionSQL = new SqlConnection(cadConexion);

            comandoSQL.CommandText = "SELECT 1 FROM CATEGORIA WHERE claveCategoria= @claveCategoria";
            comandoSQL.Parameters.AddWithValue("@claveCategoria", eCategoria.ClaveCategoria);
            comandoSQL.Connection = conexionSQL;
            try
            {
                conexionSQL.Open();
                obEscalar = comandoSQL.ExecuteScalar();
                if (obEscalar == null)  resultado = true;               
                conexionSQL.Close();
            }
            catch (Exception)
            {
                conexionSQL.Close();
                throw new Exception("Se ha presentado un error realizando la consulta de existencia de la categoria");
            }
            finally
            {
                comandoSQL.Dispose();
                conexionSQL.Dispose();
            }
            return resultado;
        }

        public ELibro devolverLibro(string condicion)
        {
            ELibro eLibro= new ELibro();

            ECategoria categoria = new ECategoria();

            string sentencia = $"SELECT claveLibro, titulo, claveAutor, claveCategoria FROM Libro " + $" WHERE {condicion}";

            SqlConnection connection = new SqlConnection(cadConexion);

            SqlCommand comando = new SqlCommand(sentencia, connection);

            SqlDataReader sqlDataReader;

            try
            {
                connection.Open();

                sqlDataReader = comando.ExecuteReader();

                if (sqlDataReader.HasRows)
                {
                    sqlDataReader.Read();

                    eLibro.ClaveLibro = sqlDataReader.GetString(0);

                    eLibro.Titulo = sqlDataReader.GetString(1);

                    eLibro.ClaveAutor = sqlDataReader.GetString(2);

                    //  MEJORA: REVISAR CATEGORIA HACIENDO UN SELECT.
                    eLibro.ClaveCategoria.ClaveCategoria = sqlDataReader.GetString(3);
                }

                connection.Close();     
            }
            catch (Exception)
            {
                connection.Close();
                throw new Exception("No se ha encontrado el libro");
            }
            finally { connection.Dispose(); comando.Dispose(); }

            return eLibro;
        }

        public int eliminar(ELibro eLibro)
        {
            int resultado = 0;

            string sentecia = "Delete from libro where claveLibro = @claveLibro";



            SqlConnection connection = new SqlConnection(cadConexion);

            SqlCommand comando = new SqlCommand(sentecia,connection);

            comando.Parameters.AddWithValue("@claveLibro", eLibro.ClaveLibro);

            try
            {
                connection.Open();
                resultado = comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                resultado = -1;
                //  throw new Exception("Se ha producido un error en eliminacion");
            }
            finally {
                connection.Dispose();

            }
            return resultado;
        }

        public string eliminarProcedure(ELibro eLibro)
        {

            string sentencia = "EliminarLibro";
            SqlConnection connection = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia,connection);

            comando.CommandType = CommandType.StoredProcedure;
            comando.Parameters.AddWithValue("@clave", eLibro.ClaveLibro);
            comando.Parameters.Add("@msj", SqlDbType.VarChar, 100).Direction= ParameterDirection.Output;

            try
            {
                connection.Open();
                comando.ExecuteNonQuery();
                mensaje = comando.Parameters["@msj"].Value.ToString();
                connection.Close();
            }
            catch (Exception)
            {

                throw new Exception("Se ha producido un error en el proceso eliminar producto");
            }finally
            {
                connection.Dispose();
                comando.Dispose();
            }

            return mensaje;
        }

        public int modificar(ELibro libro, string claveVieja = "")
        {
            int resultado = -1;
            string sentencia;
            SqlConnection sqlConnection = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();
            if (claveVieja == "")
            {
                sentencia = "UPDATE libro set titulo = @titulo, claveAutor = @claveAutor, claveCategoria= @claveCategoria Where claveLibro = @claveLibro";
            }
            else
            {
                sentencia = $"UPDATE libro set claveLibro = @clave; titulo = @titulo, claveAutor = @claveAutor, claveCategoria= @claveCategoria Where claveLibro = '{claveVieja}'";
            }
            comando.Connection = sqlConnection;
            comando.CommandText = sentencia;

            comando.Parameters.AddWithValue("clave",libro.ClaveLibro);
            comando.Parameters.AddWithValue("titulo", libro.Titulo);
            comando.Parameters.AddWithValue("claveAutor", libro.ClaveAutor);
            comando.Parameters.AddWithValue("claveCategoria", libro.ClaveCategoria);

            try
            {
                sqlConnection.Open();
                resultado = comando.ExecuteNonQuery();
                sqlConnection.Close();
            }
            catch (Exception)
            {
                sqlConnection.Close();
                throw new Exception("Error al actualizar");
            }finally
            {
                sqlConnection.Dispose();
                comando.Dispose();
            }

            return resultado;
        }
    }
}

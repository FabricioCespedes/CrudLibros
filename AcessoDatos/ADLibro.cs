using Entidades;
using System;
using System.Collections.Generic;
using System.Text;

using System.Data.SqlClient;
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

            comandoSQL.CommandText = $"SELECT 1 FROM LIBRO WHERE titulo={libro.Titulo} and claveAutor={libro.ClaveAutor}";

            try
            {
                conexionSQL.Open();
                dataReader = comandoSQL.ExecuteReader();
                result = dataReader.HasRows ? true : false;
                conexionSQL.Close();
            }
            catch (Exception)
            {

                throw new Exception("Se ha presentado un error realizando la consulta");
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
            SqlDataReader dataReader;
            comandoSQL.CommandText = $"SELECT 1 FROM LIBRO WHERE claveLibro= @clave";
            comandoSQL.Parameters.AddWithValue("@clave", clave);
            comandoSQL.Connection = conexionSQL;
            try
            {
                conexionSQL.Open();
                obEscalar = comandoSQL.ExecuteScalar();
                conexionSQL.Close();
                if (obEscalar!=null)
                {
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception)
            {

                throw new Exception("Se ha presentado un error realizando la consulta");
            }
            finally
            {
                comandoSQL.Dispose();
                conexionSQL.Dispose();
            }
            return result;
        }

        public string Mensaje { get;}

        #region Constructores
        
        #endregion
    }
}

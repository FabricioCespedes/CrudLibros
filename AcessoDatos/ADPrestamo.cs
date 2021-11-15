using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;

namespace AcessoDatos
{
    public class ADPrestamo
    {
        string cadConexion;

        public ADPrestamo(string cadConexion)
        {
            this.cadConexion = cadConexion;
        }

        public DataSet listarEjemplares(string condicion = "")
        {
            DataSet setEjemplares = new DataSet();
            string sentecia = " SELECT E.claveEjemplar , E.edicion, E.numeroPaginas, EST.descripcion AS Estado," +
                "  L.titulo, (A.nombre + ' '+ A.apPaterno) AS Autor," +
                "c.descripcion AS Condicion, ED.nombre AS Editorial, " +
                "CAT.descripcion AS Categoria FROM EJEMPLAR E  " +
                "JOIN LIBRO L ON L.claveLibro = L.claveLibro " +
                "JOIN AUTOR A ON A.claveAutor = L.claveAutor " +
                "JOIN CONDICION C ON C.claveCondicion = E.claveCondicion " +
                "JOIN ESTADO EST ON EST.claveEstado = E.claveEstado " +
                "JOIN EDITORIAL ED ON ED.claveEditorial = E.claveEditorial " +
                "JOIN CATEGORIA CAT ON CAT.claveCategoria = L.claveCategoria";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentecia = string.Format("{0} where {1}", sentecia, condicion);

            }
            SqlConnection connection = new SqlConnection(cadConexion);
            SqlDataAdapter dataAdapter;

            try
            {
                dataAdapter = new SqlDataAdapter(sentecia, connection);
                dataAdapter.Fill(setEjemplares);
                connection.Close();
                dataAdapter.Dispose();
            }
            catch (Exception)
            {
                connection.Close();
                throw new Exception("Ha ocurrido un error en la selección de ejemplares");
            }
            finally
            {
                connection.Dispose();
            }

            return setEjemplares;
        }

        public List<ELibro> listarLibros()
        {
            List<ELibro> setLibros = new List<ELibro>();
            string sentecia = "SELECT ' ' UNION SELECT[titulo] FROM [LIBRO]";
            SqlConnection connection = new SqlConnection(cadConexion);

            try
            {
                connection.Open();
                SqlCommand comando = new SqlCommand(sentecia, connection);
                SqlDataReader registro = comando.ExecuteReader();

                if (registro.HasRows)
                {
                    while (registro.Read())
                    {
                        ELibro eLibro = new ELibro();

                        eLibro.Titulo = registro.GetString(0);

                        setLibros.Add(eLibro);
                    }
                }

                connection.Close();
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

        public List<EUsuario> listarUsuarios(string condicion = "")
        {
            List<EUsuario> setUsuarios = new List<EUsuario>();
            string sentecia = " SELECT claveUsuario, nombre, apPaterno AS Usuario FROM Usuario";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentecia = string.Format("{0} where {1}", sentecia, condicion);

            }
            SqlConnection connection = new SqlConnection(cadConexion);

            try
            {
                connection.Open();
                SqlCommand comando = new SqlCommand(sentecia, connection);
                SqlDataReader registro = comando.ExecuteReader();

                if (registro.HasRows)
                {
                    while (registro.Read())
                    {
                        EUsuario eUsuario = new EUsuario();

                        eUsuario.ClaveUsuario = registro.GetString(0);

                        eUsuario.Nombre = registro.GetString(1);

                        eUsuario.Apellido1 = registro.GetString(2);

                        setUsuarios.Add(eUsuario);
                    }
                }
            }
            catch (Exception)
            {
                connection.Close();
                throw new Exception("Ha ocurrido un error en la selección de usuarios");
            }
            finally
            {
                connection.Dispose();
            }

            return setUsuarios;
        }

        public DataSet listarPrestamos(string condicion = "")
        {
            DataSet setPrestamos = new DataSet();
            string sentecia = " SELECT P.clavePrestamo,U.claveUsuario ,(U.nombre + ' ' + U.apPaterno) AS Usuario, E.claveEjemplar, L.titulo, P.fechaDevolucion, p.fechaPrestamo FROM PRESTAMO P JOIN USUARIO U ON P.claveUsuario = U.claveUsuario JOIN Ejemplar E ON E.claveEjemplar = P.claveEjemplar JOIN Libro L ON L.claveLibro = E.claveLibro";
            if (!string.IsNullOrEmpty(condicion))
            {
                sentecia = string.Format("{0} where {1}", sentecia, condicion);

            }
            SqlConnection connection = new SqlConnection(cadConexion);
            SqlDataAdapter dataAdapter;

            try
            {
                dataAdapter = new SqlDataAdapter(sentecia, connection);
                dataAdapter.Fill(setPrestamos);
                connection.Close();
                dataAdapter.Dispose();
            }
            catch (Exception)
            {
                connection.Close();
                throw new Exception("Ha ocurrido un error en la selección de prestamos");
            }
            finally
            {
                connection.Dispose();
            }

            return setPrestamos;
        }

        public int insertarPrestamos(EPrestamo ePrestamo)
        {
            int resultado = -1;
            string sentencia = "INSERT INTO PRESTAMO(clavePrestamo,claveEjemplar,claveUsuario,fechaPrestamo,fechaDevolucion)" +
                " VALUES (@clavePrestamo,@claveEjemplar,@claveUsuario,@fechaPrestamo,@fechaDevolucion)";
            SqlConnection conexion = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand(sentencia, conexion);
            comando.Parameters.AddWithValue("@clavePrestamo", ePrestamo.ClavePrestamo);
            comando.Parameters.AddWithValue("@claveEjemplar", ePrestamo.EEjemplar.ClaveEjemplar);
            comando.Parameters.AddWithValue("@claveUsuario", ePrestamo.EUsuario.ClaveUsuario);
            comando.Parameters.AddWithValue("@fechaPrestamo", ePrestamo.FechaPrestamo.ToString("yyyy-mm-dd"));
            comando.Parameters.AddWithValue("@fechaDevolucion", ePrestamo.FechaDevolucion.ToString("yyyy-mm-dd"));
            try
            {
                conexion.Open();
                resultado = comando.ExecuteNonQuery();
                conexion.Close();
            }
            catch (Exception)
            {
                conexion.Close();
                throw new Exception("No se ha insertado el prestamos de forma correcta");
            }
            finally
            {
                conexion.Dispose();
                comando.Dispose();
            }
            return resultado;

        }

        public int eliminar(EPrestamo ePrestamo)
        {
            int resultado = 0;

            string sentecia = $"DELETE FROM PRESTAMO where clavePrestamo = '{ePrestamo.ClavePrestamo}'";

            SqlConnection connection = new SqlConnection(cadConexion);

            SqlCommand comando = new SqlCommand(sentecia, connection);

            try
            {
                connection.Open();
                resultado = comando.ExecuteNonQuery();
                connection.Close();
            }
            catch (Exception)
            {
                resultado = -1;
            }
            finally
            {
                connection.Dispose();

            }
            return resultado;
        }

        public int modificar(EPrestamo ePrestamo, string claveVieja = "")
        {
            int resultado = -1;
            string sentencia;
            SqlConnection sqlConnection = new SqlConnection(cadConexion);
            SqlCommand comando = new SqlCommand();
            if (claveVieja == "")
            {
                sentencia = "UPDATE Prestamo set claveEjemplar = @claveEjemplar, claveUsuario = @claveUsuario, fechaPrestamo= @fechaPrestamo, fechaDevolucion= @fechaDevolucion Where clavePrestamo = @clavePrestamo";
            }
            else
            {
                sentencia = $"UPDATE Prestamo set clavePrestamo= @clavePrestamo,claveEjemplar = @claveEjemplar, claveUsuario = @claveUsuario, fechaPrestamo= @fechaPrestamo, fechaDevolucion= @fechaDevolucion Where clavePrestamo = '{claveVieja}'";
            }
            comando.Connection = sqlConnection;
            comando.CommandText = sentencia;

            comando.Parameters.AddWithValue("@clavePrestamo", ePrestamo.ClavePrestamo);
            comando.Parameters.AddWithValue("@claveEjemplar,", ePrestamo.EEjemplar.ClaveEjemplar);
            comando.Parameters.AddWithValue("@claveUsuario", ePrestamo.EUsuario.ClaveUsuario);
            comando.Parameters.AddWithValue("@fechaPrestamo", ePrestamo.FechaPrestamo.ToString("yyyy-mm-dd"));
            comando.Parameters.AddWithValue("@fechaDevolucion", ePrestamo.FechaDevolucion.ToString("yyyy-mm-dd"));

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
            }
            finally
            {
                sqlConnection.Dispose();
                comando.Dispose();
            }

            return resultado;
        }
    }
}

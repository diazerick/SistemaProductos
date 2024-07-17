using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using WebProductos.Models;

namespace WebProductos.Datos
{
    public class D_Producto
    {
        private string cadenaConexion = "server=localhost;database=Generacion28;user=sa;password=devo123";

        public List<E_Producto> ObtenerTodos()
        {
            //Creamos lista para almacenar los productos
            List<E_Producto> lista = new List<E_Producto>();
            //Cadena de conexion para conectarnos a la BD
            //Objeto de la clase SqlConnecion para conectarnos a la BD
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                //Abrir la conexion
                conexion.Open();
                //query a ejecutar
                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos";
                //Objeto de la clase SqlCommand para ejecutar el query
                SqlCommand comando = new SqlCommand(query, conexion);
                //Ejecutamos el query y el resultado lo almacenamos en un objeto SqlDataReader
                SqlDataReader reader = comando.ExecuteReader();

                //Recorrer el reader con los resultados del query
                while (reader.Read())
                {
                    //Crear un producto y le asignamos valores a sus propiedades
                    E_Producto producto = new E_Producto();
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["descripcion"].ToString();
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);
                    //Agregar el producto a la lista
                    lista.Add(producto);
                }
            }
            catch (Exception ex)
            {
                //Cuando ocurre un error en la capa de datos
                //Lanzamos nuevamente la excepcion para que sea controlada por try-catch del controlador
                throw ex;
            }
            finally
            {
                //Cerrar la conexion
                conexion.Close();
            }
            return lista;
        }

        public void Agregar(E_Producto objProducto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            conexion.Open();

            //Declarar el query utilizando parametros
            string query = "INSERT INTO Productos(descripcion,precio,fechaIngreso,disponible) " +
                                         "VALUES(@descripcion,@precio,@fechaIngreso,@disponible)";

            //Objeto SqlCommand para ejecutar el query
            SqlCommand comando = new SqlCommand(query, conexion);

            //Asignarle valores a los parametros del query
            comando.Parameters.AddWithValue("@descripcion", objProducto.Descripcion);
            comando.Parameters.AddWithValue("@precio", objProducto.Precio);
            comando.Parameters.AddWithValue("@fechaIngreso", objProducto.FechaIngreso);
            comando.Parameters.AddWithValue("@disponible", objProducto.Disponible);

            comando.ExecuteNonQuery();

            conexion.Close();
        }

        public void Eliminar(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();
                string query = "DELETE Productos WHERE idProducto = @idProducto";

                SqlCommand comando = new SqlCommand(query,conexion);

                comando.Parameters.AddWithValue("@idProducto", idProducto);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
        }

        public E_Producto ObtenerPorId(int idProducto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            E_Producto producto = new E_Producto();
            try
            {
                conexion.Open();

                string query = "SELECT idProducto,descripcion,precio,fechaIngreso,disponible FROM Productos " +
                                "WHERE idProducto = @idProducto";

                SqlCommand comando = new SqlCommand(query,conexion);
                comando.Parameters.AddWithValue("@idProducto", idProducto);

                SqlDataReader reader = comando.ExecuteReader();
                //Si el query nos devolvio un resultado, lo leemos
                if (reader.Read())
                {
                    producto.IdProducto = Convert.ToInt32(reader["idProducto"]);
                    producto.Descripcion = reader["descripcion"].ToString();
                    producto.Precio = Convert.ToDecimal(reader["precio"]);
                    producto.FechaIngreso = Convert.ToDateTime(reader["fechaIngreso"]);
                    producto.Disponible = Convert.ToBoolean(reader["disponible"]);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }
            return producto;
        }

        public void Editar(E_Producto producto)
        {
            SqlConnection conexion = new SqlConnection(cadenaConexion);
            try
            {
                conexion.Open();

                string query = "UPDATE Productos SET descripcion=@descripcion, precio=@precio," +
                               "fechaIngreso=@fechaIngreso, disponible=@disponible " +
                               "WHERE idProducto = @idProducto";

                SqlCommand comando = new SqlCommand(query, conexion);

                comando.Parameters.AddWithValue("@descripcion", producto.Descripcion);
                comando.Parameters.AddWithValue("@precio", producto.Precio);
                comando.Parameters.AddWithValue("@fechaIngreso", producto.FechaIngreso);
                comando.Parameters.AddWithValue("@disponible", producto.Disponible);
                comando.Parameters.AddWithValue("@idProducto", producto.IdProducto);

                comando.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                conexion.Close();
            }

        }

    }
}
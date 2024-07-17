using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebProductos.Datos;
using WebProductos.Models;

namespace WebProductos.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Producto
        public ActionResult Index()
        {
            //Declararmos una lista de productos vacia
            List<E_Producto> productos = new List<E_Producto>();
            try
            {
                //Creamos un objeto de la capa de datos
                D_Producto datos = new D_Producto();
                //Obtenemos la lista de productos desde la capa de datos
                productos = datos.ObtenerTodos();
                //Pasamos la lista de productos como MODELO a la vista
                return View("Principal", productos);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return View("Principal", productos);
            }
        }

        public ActionResult IrAgregar()
        {
            return View("VistaAgregar");
        }

        public ActionResult Agregar(E_Producto producto)
        {
            //Crear objeto de la capa de datos
            D_Producto datos = new D_Producto();

            datos.Agregar(producto);

            TempData["mensaje"] = $"El producto {producto.Descripcion} se registro correctamente";

            //Redirigimos a la accion Index, para que en esa accion obtengamos el modelo de la vista
            return RedirectToAction("Index");
        }

        public ActionResult Eliminar(int idProducto)
        {
            try
            {
                D_Producto datos = new D_Producto();
                datos.Eliminar(idProducto);
                TempData["mensaje"] = $"El producto con ID {idProducto} se elimino";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }

        public ActionResult ObtenerParaEditar(int idProducto)
        {
            try
            {
                //Crear objeto de la capa de datos
                D_Producto datos = new D_Producto();
                //Obtener los datos del producto
                E_Producto producto = datos.ObtenerPorId(idProducto);
                //Mandamos a la vista Editar con el producto como modelo
                return View("VistaEditar", producto);
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
                return RedirectToAction("Index");
            }
        }

        public ActionResult Editar(E_Producto objProducto)
        {
            try
            {
                D_Producto datos = new D_Producto();

                datos.Editar(objProducto);

                TempData["mensaje"] = $"El producto con id {objProducto.IdProducto} se edito";
            }
            catch (Exception ex)
            {
                TempData["error"] = ex.Message;
            }

            return RedirectToAction("Index");
        }
    }
}
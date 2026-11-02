using System;
using System.Collections.Generic;
using System.Linq;
using Entidades;

namespace CapaNegocio
{
    public class ProductoNegocio
    {
        private readonly CapaDato.ProductoDatos datos = new CapaDato.ProductoDatos();

        // =====================================================================
        // CONSULTAS
        // =====================================================================

        public List<Producto> Consultar()
        {
            return datos.Consultar();
        }

        public List<Producto> Buscar(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                return datos.Consultar();

            return datos.Buscar(nombre.Trim());
        }

        public List<Producto> ConsultarDisponibles()
        {
            return datos.ConsultarDisponibles();
        }

        public Producto ObtenerPorId(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto no es válido.");

            return datos.ObtenerPorId(idProducto);
        }

        // =====================================================================
        // VALIDACIONES DE STOCK
        // =====================================================================

        public bool HayStock(int idProducto, int cantidad)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto no es válido.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            return datos.HayStock(idProducto, cantidad);
        }

        public void DescontarStock(int idProducto, int cantidad)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto no es válido.");
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad debe ser mayor a 0.");

            datos.DescontarStock(idProducto, cantidad);
        }

        public void DescontarStockMultiple(List<(int IdProducto, int Cantidad)> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Debe indicar al menos un producto para descontar.");

            foreach (var item in items)
            {
                if (item.IdProducto <= 0)
                    throw new ArgumentException("El ID del producto no es válido.");
                if (item.Cantidad <= 0)
                    throw new ArgumentException("La cantidad debe ser mayor a 0.");
            }

            datos.DescontarStockMultiple(items);
        }

        // =====================================================================
        // CRUD
        // =====================================================================

        public int Insertar(Producto producto)
        {
            Validar(producto);
            return datos.Insertar(producto);
        }

        public void Actualizar(Producto producto)
        {
            if (producto == null || producto.IdProducto <= 0)
                throw new ArgumentException("El ID del producto no es válido.");

            Validar(producto);
            datos.Actualizar(producto);
        }

        public void Eliminar(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El ID del producto no es válido.");

            datos.Eliminar(idProducto);
        }

        // =====================================================================
        // VALIDACIÓN CENTRALIZADA
        // =====================================================================

        private void Validar(Producto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto),
                    "Debe proporcionar los datos del producto.");

            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");

            if (producto.Precio <= 0)          // ← antes era < 0, ahora coincide con la BD
                throw new ArgumentException("El precio debe ser mayor a 0.");

            if (producto.CantidadDisponible < 0)
                throw new ArgumentException("La cantidad no puede ser negativa.");

            if (producto.IdCategoria <= 0)
                throw new ArgumentException("Debe seleccionar una categoría.");
        }
    }
}
using System;
using System.Collections.Generic;
using System.Linq;
using EntidadProducto = Entidades.Producto;

namespace CapaDato
{
    public class ProductoDatos
    {

        public List<EntidadProducto> Consultar()
        {
            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    return contexto.Productoes
                        .Select(p => new EntidadProducto
                        {
                            IdProducto = p.IdProducto,
                            Nombre = p.Nombre,
                            Precio = p.Precio,
                            CantidadDisponible = p.CantidadDisponible,
                            IdCategoria = p.IdCategoria
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar los productos: " + ex.Message, ex);
            }
        }

        public List<EntidadProducto> Buscar(string nombre)
        {
            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    return contexto.Productoes
                        .Where(p => p.Nombre.Contains(nombre))
                        .Select(p => new EntidadProducto
                        {
                            IdProducto = p.IdProducto,
                            Nombre = p.Nombre,
                            Precio = p.Precio,
                            CantidadDisponible = p.CantidadDisponible,
                            IdCategoria = p.IdCategoria
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al buscar productos: " + ex.Message, ex);
            }
        }

  
        public List<EntidadProducto> ConsultarDisponibles()
        {
            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    return contexto.Productoes
                        .Where(p => p.CantidadDisponible > 0)
                        .Select(p => new EntidadProducto
                        {
                            IdProducto = p.IdProducto,
                            Nombre = p.Nombre,
                            Precio = p.Precio,
                            CantidadDisponible = p.CantidadDisponible,
                            IdCategoria = p.IdCategoria
                        })
                        .ToList();
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar productos disponibles: " + ex.Message, ex);
            }
        }

        public EntidadProducto ObtenerPorId(int idProducto)
        {
            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var p = contexto.Productoes.Find(idProducto);
                    if (p == null) return null;

                    return new EntidadProducto
                    {
                        IdProducto = p.IdProducto,
                        Nombre = p.Nombre,
                        Precio = p.Precio,
                        CantidadDisponible = p.CantidadDisponible,
                        IdCategoria = p.IdCategoria
                    };
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el producto: " + ex.Message, ex);
            }
        }

      
        public bool HayStock(int idProducto, int cantidad)
        {
            if (cantidad <= 0) return false;

            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var p = contexto.Productoes.Find(idProducto);
                    return p != null && p.CantidadDisponible >= cantidad;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al verificar el stock: " + ex.Message, ex);
            }
        }

      
        public void DescontarStock(int idProducto, int cantidad)
        {
            if (cantidad <= 0)
                throw new ArgumentException("La cantidad a descontar debe ser mayor a 0.");

            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var p = contexto.Productoes.Find(idProducto);

                    if (p == null)
                        throw new InvalidOperationException("El producto no existe.");

                    if (p.CantidadDisponible < cantidad)
                        throw new InvalidOperationException(
                            $"Stock insuficiente para '{p.Nombre}'. Disponible: {p.CantidadDisponible}.");

                    p.CantidadDisponible -= cantidad;
                    contexto.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {
                throw; // Re-lanza mensajes de negocio tal cual
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descontar el stock: " + ex.Message, ex);
            }
        }

        public void DescontarStockMultiple(List<(int IdProducto, int Cantidad)> items)
        {
            if (items == null || items.Count == 0)
                throw new ArgumentException("Debe indicar al menos un producto para descontar.");

            try
            {
                using (var contexto = new InventarioVentasEntities())
                using (var transaccion = contexto.Database.BeginTransaction())
                {
                    foreach (var item in items)
                    {
                        if (item.Cantidad <= 0)
                            throw new ArgumentException("La cantidad a descontar debe ser mayor a 0.");

                        var p = contexto.Productoes.Find(item.IdProducto);

                        if (p == null)
                            throw new InvalidOperationException("El producto no existe.");

                        if (p.CantidadDisponible < item.Cantidad)
                            throw new InvalidOperationException(
                                $"Stock insuficiente para '{p.Nombre}'. Disponible: {p.CantidadDisponible}.");

                        p.CantidadDisponible -= item.Cantidad;
                    }

                    contexto.SaveChanges();
                    transaccion.Commit();
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (ArgumentException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al descontar el stock: " + ex.Message, ex);
            }
        }

    
        public int Insertar(EntidadProducto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");
            if (producto.CantidadDisponible < 0)
                throw new ArgumentException("La cantidad disponible no puede ser negativa.");

            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var nuevo = new Producto
                    {
                        Nombre = producto.Nombre,
                        Precio = producto.Precio,
                        CantidadDisponible = producto.CantidadDisponible,
                        IdCategoria = producto.IdCategoria
                    };

                    contexto.Productoes.Add(nuevo);
                    contexto.SaveChanges();

                    producto.IdProducto = nuevo.IdProducto;
                    return nuevo.IdProducto;
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al insertar el producto: " + ex.Message, ex);
            }
        }

   
        public void Actualizar(EntidadProducto producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));
            if (producto.IdProducto <= 0)
                throw new ArgumentException("El Id del producto no es válido.");
            if (string.IsNullOrWhiteSpace(producto.Nombre))
                throw new ArgumentException("El nombre del producto es obligatorio.");
            if (producto.Precio <= 0)
                throw new ArgumentException("El precio debe ser mayor a 0.");
            if (producto.CantidadDisponible < 0)
                throw new ArgumentException("La cantidad disponible no puede ser negativa.");

            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var existente = contexto.Productoes.Find(producto.IdProducto);

                    if (existente == null)
                        throw new InvalidOperationException("El producto no existe.");

                    existente.Nombre = producto.Nombre;
                    existente.Precio = producto.Precio;
                    existente.CantidadDisponible = producto.CantidadDisponible;
                    existente.IdCategoria = producto.IdCategoria;

                    contexto.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al actualizar el producto: " + ex.Message, ex);
            }
        }

   
        public void Eliminar(int idProducto)
        {
            if (idProducto <= 0)
                throw new ArgumentException("El Id del producto no es válido.");

            try
            {
                using (var contexto = new InventarioVentasEntities())
                {
                    var existente = contexto.Productoes.Find(idProducto);

                    if (existente == null)
                        throw new InvalidOperationException("El producto no existe.");

                    contexto.Productoes.Remove(existente);
                    contexto.SaveChanges();
                }
            }
            catch (InvalidOperationException)
            {
                throw;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al eliminar el producto: " + ex.Message, ex);
            }
        }
    }
}
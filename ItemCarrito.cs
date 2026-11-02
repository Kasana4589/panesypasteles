namespace Entidades
{
    public class ItemCarrito
    {
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal Subtotal
        {
            get
            {
                return Producto.Precio * Cantidad;
            }
        }
    }
}
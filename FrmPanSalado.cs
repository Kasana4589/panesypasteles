using Entidades;
using System;

namespace Pagina_14
{
    public class FrmPanSalado : FrmCatalogoProductos
    {
        public FrmPanSalado()
            : base(2, "Pan Salado")
        {
        }

        protected override bool CorrespondeALaCategoria(Producto producto)
        {
            if (producto.IdCategoria != 2)
                return false;

            bool esPanSimple =
                producto.Nombre.IndexOf("Bolillo", StringComparison.OrdinalIgnoreCase) >= 0
                || producto.Nombre.IndexOf("Pan Simple", StringComparison.OrdinalIgnoreCase) >= 0;

            return !esPanSimple;
        }
    }
}

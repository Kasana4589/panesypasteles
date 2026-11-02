using Entidades;
using System;

namespace Pagina_14
{
    public class FrmPanSimple : FrmCatalogoProductos
    {
        public FrmPanSimple()
            : base(2, "Pan Simple")
        {
        }

        protected override bool CorrespondeALaCategoria(Producto producto)
        {
            return producto.Nombre.IndexOf("Bolillo", StringComparison.OrdinalIgnoreCase) >= 0
                || producto.Nombre.IndexOf("Pan Simple", StringComparison.OrdinalIgnoreCase) >= 0;
        }
    }
}

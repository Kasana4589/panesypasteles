using CapaNegocio;
using Entidad;
using Entidades;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Windows.Forms;

namespace Pagina_14
{
    public partial class FrmCatalogoProductos : Form
    {
        private readonly int idCategoria;

        private readonly ProductoNegocio negocio = new ProductoNegocio();

        private List<Producto> productos = new List<Producto>();
        private readonly List<ItemCarrito> carrito = new List<ItemCarrito>();

        private Button btnCarrito;
        private Button btnReporteGeneral;  
        private Button btnReportePorId;     

        public FrmCatalogoProductos()
            : this(2, "Pan Salado")
        {
        }

        public FrmCatalogoProductos(int idCategoria, string titulo)
        {
            this.idCategoria = idCategoria;
            InitializeComponent();
            this.Text = titulo;
            this.lblTituloCategoria.Text = titulo;
            CrearBotonCarrito();
            CrearBotonesReporte();
        }

        private void FrmCatalogoProductos_Load(object sender, EventArgs e)
        {
            CargarProductos();
        }

        private void CargarProductos()
        {
            try
            {
                productos = negocio.ConsultarDisponibles()
                                   .FindAll(CorrespondeALaCategoria);

                productos = QuitarDuplicados(productos);
                MostrarProductos(productos);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al cargar los productos: " + ex.Message,
                    "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        protected virtual bool CorrespondeALaCategoria(Producto producto)
        {
            return producto.IdCategoria == idCategoria;
        }

        private static List<Producto> QuitarDuplicados(List<Producto> lista)
        {
            var unicos = new List<Producto>();
            var ids = new HashSet<int>();

            foreach (Producto producto in lista)
            {
                if (producto == null || ids.Contains(producto.IdProducto))
                    continue;

                ids.Add(producto.IdProducto);
                unicos.Add(producto);
            }

            return unicos;
        }

        private void MostrarProductos(List<Producto> lista)
        {
            flpProductos.SuspendLayout();
            flpProductos.Controls.Clear();

            if (lista == null || lista.Count == 0)
            {
                Label vacio = new Label();
                vacio.AutoSize = false;
                vacio.Width = 420;
                vacio.Height = 80;
                vacio.Margin = new Padding(12);
                vacio.Text = "No hay productos disponibles en esta categoría.";
                vacio.Font = new Font("Segoe UI", 12F, FontStyle.Italic);
                vacio.ForeColor = Color.FromArgb(128, 90, 70);
                vacio.TextAlign = ContentAlignment.MiddleLeft;
                flpProductos.Controls.Add(vacio);
                flpProductos.ResumeLayout();
                return;
            }

            foreach (Producto producto in lista)
            {
                Panel tarjeta = CrearTarjeta(producto);
                flpProductos.Controls.Add(tarjeta);
            }

            flpProductos.ResumeLayout();
        }

        private Panel CrearTarjeta(Producto producto)
        {
            Color cafe = Color.FromArgb(107, 62, 46);
            Color naranja = Color.FromArgb(224, 138, 61);
            Color cremaFoto = Color.FromArgb(243, 230, 216);

            Panel tarjeta = new Panel();
            tarjeta.Width = 230;
            tarjeta.Height = 365;
            tarjeta.Margin = new Padding(12);
            tarjeta.BackColor = Color.White;
            tarjeta.Padding = new Padding(0);
            tarjeta.Paint += (s, pe) =>
            {
                using (Pen borde = new Pen(Color.FromArgb(232, 214, 196)))
                {
                    Rectangle recuadro = tarjeta.ClientRectangle;
                    recuadro.Width -= 1;
                    recuadro.Height -= 1;
                    pe.Graphics.DrawRectangle(borde, recuadro);
                }
            };

            Panel acento = new Panel();
            acento.Size = new Size(230, 6);
            acento.Location = new Point(0, 0);
            acento.BackColor = naranja;
            tarjeta.Controls.Add(acento);

            PictureBox imagen = new PictureBox();
            imagen.Size = new Size(208, 138);
            imagen.Location = new Point(11, 16);
            imagen.BackColor = cremaFoto;
            imagen.SizeMode = PictureBoxSizeMode.Zoom;
            imagen.Image = ImagenCatalogo.Obtener(producto);
            tarjeta.Controls.Add(imagen);

            Label nombre = new Label();
            nombre.Text = producto.Nombre;
            nombre.Size = new Size(208, 40);
            nombre.Location = new Point(11, 160);
            nombre.TextAlign = ContentAlignment.MiddleCenter;
            nombre.Font = new Font("Segoe UI Semibold", 10.5F, FontStyle.Bold);
            nombre.ForeColor = cafe;
            tarjeta.Controls.Add(nombre);

            Label precio = new Label();
            precio.Text = "$" + producto.Precio.ToString("0.00");
            precio.Size = new Size(208, 26);
            precio.Location = new Point(11, 200);
            precio.TextAlign = ContentAlignment.MiddleCenter;
            precio.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            precio.ForeColor = naranja;
            tarjeta.Controls.Add(precio);

            Label stock = new Label();
            stock.Text = "Disponibles: " + producto.CantidadDisponible;
            stock.Size = new Size(208, 20);
            stock.Location = new Point(11, 226);
            stock.TextAlign = ContentAlignment.MiddleCenter;
            stock.Font = new Font("Segoe UI", 8.5F);
            stock.ForeColor = Color.FromArgb(140, 110, 90);
            tarjeta.Controls.Add(stock);

            Button menos = CrearBotonCantidad("-", new Point(52, 254));
            Label cantidad = new Label();
            cantidad.Text = "1";
            cantidad.Size = new Size(46, 32);
            cantidad.Location = new Point(92, 254);
            cantidad.TextAlign = ContentAlignment.MiddleCenter;
            cantidad.Font = new Font("Segoe UI", 11F, FontStyle.Bold);
            cantidad.ForeColor = cafe;
            cantidad.BackColor = cremaFoto;
            Button mas = CrearBotonCantidad("+", new Point(142, 254));
            tarjeta.Controls.Add(menos);
            tarjeta.Controls.Add(cantidad);
            tarjeta.Controls.Add(mas);

            Button agregar = new Button();
            agregar.Text = "Agregar al carrito";
            agregar.Size = new Size(208, 40);
            agregar.Location = new Point(11, 304);
            agregar.BackColor = naranja;
            agregar.ForeColor = Color.White;
            agregar.FlatStyle = FlatStyle.Flat;
            agregar.FlatAppearance.BorderSize = 0;
            agregar.Font = new Font("Segoe UI Semibold", 9.5F, FontStyle.Bold);
            agregar.Cursor = Cursors.Hand;
            tarjeta.Controls.Add(agregar);

            // SUMAR
            mas.Click += (s, e) =>
            {
                int valor = Convert.ToInt32(cantidad.Text);
                if (valor < producto.CantidadDisponible)
                {
                    valor++;
                    cantidad.Text = valor.ToString();
                }
            };

            // RESTAR
            menos.Click += (s, e) =>
            {
                int valor = Convert.ToInt32(cantidad.Text);
                if (valor > 1)
                {
                    valor--;
                    cantidad.Text = valor.ToString();
                }
            };

            // AGREGAR AL CARRITO
            agregar.Click += (s, e) =>
            {
                int cantidadSeleccionada = Convert.ToInt32(cantidad.Text);

                bool ok = AgregarAlCarrito(producto, cantidadSeleccionada);

                if (ok)
                {
                    cantidad.Text = "1";
                }
            };

            return tarjeta;
        }

        private static Button CrearBotonCantidad(string texto, Point ubicacion)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Size = new Size(36, 32);
            boton.Location = ubicacion;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderColor = Color.FromArgb(232, 214, 196);
            boton.BackColor = Color.FromArgb(255, 246, 236);
            boton.ForeColor = Color.FromArgb(107, 62, 46);
            boton.Font = new Font("Segoe UI", 12F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            return boton;
        }

        private Button CrearBotonAccion(string texto, Color fondo, EventHandler click)
        {
            Button boton = new Button();
            boton.Text = texto;
            boton.Width = 158;
            boton.Height = 38;
            boton.Margin = new Padding(6, 2, 6, 2);
            boton.BackColor = fondo;
            boton.ForeColor = Color.White;
            boton.FlatStyle = FlatStyle.Flat;
            boton.FlatAppearance.BorderSize = 0;
            boton.Font = new Font("Segoe UI Semibold", 9F, FontStyle.Bold);
            boton.Cursor = Cursors.Hand;
            boton.Click += click;
            return boton;
        }

        // ==========================================
        // CARRITO
        // ==========================================

        private bool AgregarAlCarrito(Producto producto, int cantidad)
        {
            ItemCarrito itemExistente = carrito.Find(
                x => x.Producto.IdProducto == producto.IdProducto);

            if (itemExistente != null)
            {
                if (itemExistente.Cantidad + cantidad <= producto.CantidadDisponible)
                {
                    itemExistente.Cantidad += cantidad;
                }
                else
                {
                    MessageBox.Show(
                        "No hay suficiente inventario disponible.",
                        "Inventario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }
            }
            else
            {
                if (cantidad > producto.CantidadDisponible)
                {
                    MessageBox.Show(
                        "No hay suficiente inventario disponible.",
                        "Inventario",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Warning);
                    return false;
                }

                carrito.Add(new ItemCarrito(producto, cantidad));
            }

            ActualizarBotonCarrito();
            return true;
        }

        private void ActualizarBotonCarrito()
        {
            int cantidadTotal = 0;
            foreach (ItemCarrito item in carrito)
                cantidadTotal += item.Cantidad;

            btnCarrito.Text = $"Carrito ({cantidadTotal})";
        }

        private void CrearBotonCarrito()
        {
            btnCarrito = CrearBotonAccion("Carrito (0)", Color.FromArgb(224, 138, 61), BtnCarrito_Click);
            flpAcciones.Controls.Add(btnCarrito);
        }

        private void BtnCarrito_Click(object sender, EventArgs e)
        {
            MostrarCarrito();
        }

        // ==========================================
        // BOTONES DE REPORTE (RA11)
        // ==========================================

        private void CrearBotonesReporte()
        {
            
            btnReporteGeneral = CrearBotonAccion("Reporte general", Color.FromArgb(90, 122, 148), BtnReporteGeneral_Click);
            btnReportePorId = CrearBotonAccion("Reporte por Id", Color.FromArgb(90, 122, 148), BtnReportePorId_Click);
            flpAcciones.Controls.Clear();
            flpAcciones.Controls.Add(btnReporteGeneral);
            flpAcciones.Controls.Add(btnReportePorId);
            flpAcciones.Controls.Add(btnCarrito);
        }

        private void BtnReporteGeneral_Click(object sender, EventArgs e)
        {
            try
            {
                var lista = negocio.Consultar();   
                if (lista == null || lista.Count == 0)
                {
                    MessageBox.Show("No existen productos para mostrar.",
                        "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                using (FrmReporteProductos formulario = new FrmReporteProductos(lista))
                {
                    formulario.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No fue posible generar el reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void BtnReportePorId_Click(object sender, EventArgs e)
        {
            try
            {
                string entrada = Microsoft.VisualBasic.Interaction.InputBox(
                    "Ingrese el Id del producto:", "Reporte individual", "");

                if (string.IsNullOrWhiteSpace(entrada))
                    return;

                if (!int.TryParse(entrada.Trim(), out int idProducto) || idProducto <= 0)
                {
                    MessageBox.Show("Id inválido.", "Reporte",
                        MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                Producto p = negocio.ObtenerPorId(idProducto);

                if (p == null)
                {
                    MessageBox.Show("No se encontró un producto con ese Id.",
                        "Reporte", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    return;
                }

                var lista = new List<Producto> { p };

                using (FrmReporteProductos formulario = new FrmReporteProductos(lista))
                {
                    formulario.ShowDialog(this);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No fue posible generar el reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        // ==========================================
        // VENTANA DEL CARRITO
        // ==========================================

        private void MostrarCarrito()
        {
            Form ventanaCarrito = new Form();
            ventanaCarrito.Text = "Carrito de compras";
            ventanaCarrito.Width = 520;
            ventanaCarrito.Height = 500;
            ventanaCarrito.StartPosition = FormStartPosition.CenterParent;
            ventanaCarrito.FormBorderStyle = FormBorderStyle.FixedDialog;
            ventanaCarrito.MaximizeBox = false;
            ventanaCarrito.MinimizeBox = false;

            // LISTA
            ListBox listaCarrito = new ListBox();
            listaCarrito.Location = new Point(20, 20);
            listaCarrito.Width = 460;
            listaCarrito.Height = 280;
            ventanaCarrito.Controls.Add(listaCarrito);

            // TOTAL
            Label lblTotal = new Label();
            lblTotal.Font = new Font("Segoe UI", 14, FontStyle.Bold);
            lblTotal.AutoSize = true;
            lblTotal.Location = new Point(20, 315);
            ventanaCarrito.Controls.Add(lblTotal);

           
            Action refrescar = null;
            refrescar = () =>
            {
                listaCarrito.Items.Clear();
                decimal totalActual = 0;

                foreach (ItemCarrito item in carrito)
                {
                    listaCarrito.Items.Add(
                        $"{item.Cantidad} x {item.Producto.Nombre}   ${item.Subtotal:0.00}");
                    totalActual += item.Subtotal;
                }

                lblTotal.Text = $"TOTAL: ${totalActual:0.00}";
            };

            refrescar();

            // BOTÓN QUITAR SELECCIONADO
            Button btnQuitar = new Button();
            btnQuitar.Text = "Quitar seleccionado";
            btnQuitar.Width = 150;
            btnQuitar.Height = 35;
            btnQuitar.Location = new Point(20, 360);
            btnQuitar.Click += (s, e) =>
            {
                if (listaCarrito.SelectedIndex < 0)
                {
                    MessageBox.Show("Seleccione un producto para quitar.");
                    return;
                }

                carrito.RemoveAt(listaCarrito.SelectedIndex);
                ActualizarBotonCarrito();
                refrescar();
            };
            ventanaCarrito.Controls.Add(btnQuitar);

            // BOTÓN CONFIRMAR PEDIDO
            Button btnConfirmar = new Button();
            btnConfirmar.Text = "Confirmar pedido";
            btnConfirmar.Width = 150;
            btnConfirmar.Height = 35;
            btnConfirmar.Location = new Point(185, 360);
            btnConfirmar.BackColor = Color.DarkOrange;
            btnConfirmar.ForeColor = Color.White;
            btnConfirmar.FlatStyle = FlatStyle.Flat;
            btnConfirmar.FlatAppearance.BorderSize = 0;
            btnConfirmar.Click += (s, e) =>
            {
                if (carrito.Count == 0)
                {
                    MessageBox.Show("El carrito está vacío.");
                    return;
                }

                try
                {
                   
                    var items = new List<(int, int)>();
                    foreach (var i in carrito)
                        items.Add((i.Producto.IdProducto, i.Cantidad));

                    negocio.DescontarStockMultiple(items);

                   
                    var factura = new Factura
                    {
                        NumeroFactura = "FAC-" + DateTime.Now.ToString("yyyyMMdd-HHmmss"),
                        Fecha = DateTime.Now,
                        Cliente = "Consumidor Final",
                        Items = new List<FacturaItem>()
                    };

                    foreach (var i in carrito)
                    {
                        factura.Items.Add(new FacturaItem
                        {
                            Nombre = i.Producto.Nombre,
                            Cantidad = i.Cantidad,
                            PrecioUnitario = i.Producto.Precio
                        });
                    }

                   
                    var resp = MessageBox.Show(
                        $"Pedido confirmado.\nTotal: ${factura.Total:0.00}\n\n¿Desea imprimir la factura?",
                        "Pedido confirmado",
                        MessageBoxButtons.YesNo,
                        MessageBoxIcon.Question);

                    if (resp == DialogResult.Yes)
                    {
                        ImprimirFactura(factura);
                    }

                   
                    carrito.Clear();
                    ActualizarBotonCarrito();
                    ventanaCarrito.Close();
                    CargarProductos();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Error al confirmar el pedido: " + ex.Message,
                        "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            };
            ventanaCarrito.Controls.Add(btnConfirmar);

            // BOTÓN CERRAR
            Button btnCerrar = new Button();
            btnCerrar.Text = "Cerrar";
            btnCerrar.Width = 150;
            btnCerrar.Height = 35;
            btnCerrar.Location = new Point(350, 360);
            btnCerrar.Click += (s, e) => ventanaCarrito.Close();
            ventanaCarrito.Controls.Add(btnCerrar);

            ventanaCarrito.ShowDialog(this);
        }

        // ==========================================
        // IMPRESIÓN DE FACTURA
        // ==========================================

        private void ImprimirFactura(Factura factura)
        {
            PrintDocument documento = new PrintDocument();
            documento.DocumentName = factura.NumeroFactura;
            documento.DefaultPageSettings.Margins = new Margins(50, 50, 50, 50);

            Factura facturaParaImprimir = factura;

            documento.PrintPage += (s, e) =>
            {
                DibujarFactura(e, facturaParaImprimir);
            };

            PrintPreviewDialog preview = new PrintPreviewDialog();
            preview.Document = documento;
            preview.Width = 900;
            preview.Height = 700;
            preview.StartPosition = FormStartPosition.CenterParent;
            preview.ShowDialog(this);
        }

        private void DibujarFactura(PrintPageEventArgs e, Factura factura)
        {
            Graphics g = e.Graphics;

            Font titulo = new Font("Segoe UI", 18, FontStyle.Bold);
            Font subtitulo = new Font("Segoe UI", 10, FontStyle.Regular);
            Font header = new Font("Segoe UI", 10, FontStyle.Bold);
            Font normal = new Font("Segoe UI", 10, FontStyle.Regular);
            Font total = new Font("Segoe UI", 13, FontStyle.Bold);

            float y = 40;
            float margenIzq = 50;
            float ancho = e.PageBounds.Width - 100;

            g.DrawString("PANADERÍA EL TRIGO", titulo, Brushes.DarkOrange, margenIzq, y);
            y += 30;
            g.DrawString("Factura de venta", subtitulo, Brushes.Black, margenIzq, y);
            y += 25;

            g.DrawString("N°: " + factura.NumeroFactura, normal, Brushes.Black, margenIzq, y);
            y += 18;
            g.DrawString("Fecha: " + factura.Fecha.ToString("dd/MM/yyyy HH:mm:ss"),
                         normal, Brushes.Black, margenIzq, y);
            y += 18;
            g.DrawString("Cliente: " + factura.Cliente, normal, Brushes.Black, margenIzq, y);
            y += 30;

            g.DrawLine(Pens.Black, margenIzq, y, margenIzq + ancho, y);
            y += 10;

            g.DrawString("Cant.", header, Brushes.Black, margenIzq, y);
            g.DrawString("Producto", header, Brushes.Black, margenIzq + 60, y);
            g.DrawString("P.Unit.", header, Brushes.Black, margenIzq + 280, y);
            g.DrawString("Subtotal", header, Brushes.Black, margenIzq + 380, y);
            y += 20;
            g.DrawLine(Pens.Gray, margenIzq, y, margenIzq + ancho, y);
            y += 10;

            foreach (var item in factura.Items)
            {
                g.DrawString(item.Cantidad.ToString(), normal, Brushes.Black, margenIzq, y);
                g.DrawString(item.Nombre, normal, Brushes.Black, margenIzq + 60, y);
                g.DrawString("$" + item.PrecioUnitario.ToString("0.00"),
                             normal, Brushes.Black, margenIzq + 280, y);
                g.DrawString("$" + item.Subtotal.ToString("0.00"),
                             normal, Brushes.Black, margenIzq + 380, y);
                y += 20;
            }

            y += 5;
            g.DrawLine(Pens.Black, margenIzq, y, margenIzq + ancho, y);
            y += 10;

            string textoTotal = "TOTAL: $" + factura.Total.ToString("0.00");
            SizeF tam = g.MeasureString(textoTotal, total);
            g.DrawString(textoTotal, total, Brushes.DarkOrange,
                         margenIzq + ancho - tam.Width, y);
            y += 40;

            g.DrawString("¡Gracias por su compra!", subtitulo, Brushes.Gray, margenIzq, y);
        }
    }
}
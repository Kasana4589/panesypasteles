using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;
using Entidades;
using Microsoft.Reporting.WinForms;

namespace Pagina_14
{
    public partial class FrmReporteProductos : Form
    {
        private readonly List<Producto> productos;

        public FrmReporteProductos(List<Producto> productos)
        {
            InitializeComponent();
            this.productos = productos;
            Load += FrmReporteProductos_Load;
        }

        private void FrmReporteProductos_Load(object sender, EventArgs e)
        {
            try
            {
                string rutaReporte = Path.Combine(
                    Application.StartupPath, "Reportes", "rptFactura.rdlc");

                reportViewer1.Reset();
                reportViewer1.ProcessingMode = ProcessingMode.Local;

                if (File.Exists(rutaReporte))
                {
                    reportViewer1.LocalReport.ReportPath = rutaReporte;
                }
                else
                {
                    reportViewer1.LocalReport.ReportEmbeddedResource =
                        "Pagina_14.Reportes.rptFactura.rdlc";
                }

                reportViewer1.LocalReport.DataSources.Clear();

                // The RDLC dataset is named dsProducto (singular).
                ReportDataSource origen = new ReportDataSource("dsProducto", productos);
                reportViewer1.LocalReport.DataSources.Add(origen);

                reportViewer1.RefreshReport();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "No fue posible cargar el reporte",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
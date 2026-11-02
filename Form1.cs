using Pagina_14;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace CapaPresentacion   
{
    public partial class FrmVentaProductos : Form
    {
        private FrmPanSimple instanciaPanSimple;
        private FrmPanDulce instanciaPanDulce;
        private FrmPanSalado instanciaPanSalado;
        private FrmPasteles instanciaPasteles;

        public FrmVentaProductos()
        {
            InitializeComponent();
            this.IsMdiContainer = true;
            AplicarEstiloPrincipal();
        }

        private void AplicarEstiloPrincipal()
        {
            Color cafe = Color.FromArgb(107, 62, 46);
            Color crema = Color.FromArgb(255, 246, 236);

            BackColor = crema;
            msProductos.BackColor = cafe;
            msProductos.ForeColor = Color.White;
            msProductos.Font = new Font("Segoe UI Semibold", 10F, FontStyle.Bold);
            msProductos.Padding = new Padding(8, 18, 8, 8);
            msProductos.AutoSize = false;
            msProductos.Width = 168;
            msProductos.Renderer = new ToolStripProfessionalRenderer(new ColoresMenuPanaderia());

            foreach (ToolStripItem item in msProductos.Items)
            {
                item.ForeColor = Color.White;
                item.Padding = new Padding(12, 10, 12, 10);
                item.AutoSize = false;
                item.Width = 150;
                item.Height = 42;
                item.TextAlign = ContentAlignment.MiddleLeft;
            }

            foreach (Control control in Controls)
            {
                if (control is MdiClient clienteMdi)
                    clienteMdi.BackColor = crema;
            }
        }

        private class ColoresMenuPanaderia : ProfessionalColorTable
        {
            public override Color MenuItemSelected => Color.FromArgb(224, 138, 61);
            public override Color MenuItemSelectedGradientBegin => Color.FromArgb(224, 138, 61);
            public override Color MenuItemSelectedGradientEnd => Color.FromArgb(224, 138, 61);
            public override Color MenuItemBorder => Color.FromArgb(224, 138, 61);
            public override Color MenuStripGradientBegin => Color.FromArgb(107, 62, 46);
            public override Color MenuStripGradientEnd => Color.FromArgb(107, 62, 46);
            public override Color ToolStripGradientBegin => Color.FromArgb(107, 62, 46);
            public override Color ToolStripGradientEnd => Color.FromArgb(107, 62, 46);
            public override Color ToolStripGradientMiddle => Color.FromArgb(107, 62, 46);
            public override Color ImageMarginGradientBegin => Color.FromArgb(107, 62, 46);
            public override Color ImageMarginGradientEnd => Color.FromArgb(107, 62, 46);
            public override Color ImageMarginGradientMiddle => Color.FromArgb(107, 62, 46);
        }

        private void BtnPanSimple_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(ref instanciaPanSimple);
        }

        private void panDulceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(ref instanciaPanDulce);
        }

        private void panSaladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(ref instanciaPanSalado);
        }

        private void pastelesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AbrirFormularioHijo(ref instanciaPasteles);
        }

      
        private void AbrirFormularioHijo<T>(ref T instancia) where T : Form, new()
        {
            try
            {
                if (instancia == null || instancia.IsDisposed)
                {
                    instancia = new T();
                    instancia.MdiParent = this;
                    instancia.FormBorderStyle = FormBorderStyle.None;
                    instancia.Dock = DockStyle.Fill;
                    instancia.Show();
                }
                else
                {
                    if (instancia.WindowState == FormWindowState.Minimized)
                        instancia.WindowState = FormWindowState.Normal;

                    instancia.Show();
                    instancia.BringToFront();
                    instancia.Activate();
                }

                CerrarOtrosCatalogos(instancia);
            }
            catch (Exception ex)
            {
                MessageBox.Show(
                    "No se pudo abrir el formulario." + Environment.NewLine +
                    Environment.NewLine +
                    ex.Message +
                    (ex.InnerException != null ? Environment.NewLine + ex.InnerException.Message : ""),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error);
            }
        }

        private void CerrarOtrosCatalogos(Form actual)
        {
            foreach (Form hijo in MdiChildren)
            {
                if (hijo != actual && !hijo.IsDisposed)
                    hijo.Close();
            }
        }

        private void salirsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
namespace Pagina_14
{
    partial class FrmCatalogoProductos
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.pnlCabecera = new System.Windows.Forms.Panel();
            this.lblTituloCategoria = new System.Windows.Forms.Label();
            this.flpAcciones = new System.Windows.Forms.FlowLayoutPanel();
            this.flpProductos = new System.Windows.Forms.FlowLayoutPanel();
            this.pnlCabecera.SuspendLayout();
            this.SuspendLayout();
            // 
            // pnlCabecera
            // 
            this.pnlCabecera.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(107)))), ((int)(((byte)(62)))), ((int)(((byte)(46)))));
            this.pnlCabecera.Controls.Add(this.lblTituloCategoria);
            this.pnlCabecera.Controls.Add(this.flpAcciones);
            this.pnlCabecera.Dock = System.Windows.Forms.DockStyle.Top;
            this.pnlCabecera.Location = new System.Drawing.Point(0, 0);
            this.pnlCabecera.Name = "pnlCabecera";
            this.pnlCabecera.Padding = new System.Windows.Forms.Padding(16, 8, 8, 8);
            this.pnlCabecera.Size = new System.Drawing.Size(980, 68);
            this.pnlCabecera.TabIndex = 1;
            // 
            // lblTituloCategoria
            // 
            this.lblTituloCategoria.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblTituloCategoria.Font = new System.Drawing.Font("Segoe UI Semibold", 16F, System.Drawing.FontStyle.Bold);
            this.lblTituloCategoria.ForeColor = System.Drawing.Color.White;
            this.lblTituloCategoria.Location = new System.Drawing.Point(16, 8);
            this.lblTituloCategoria.Name = "lblTituloCategoria";
            this.lblTituloCategoria.Size = new System.Drawing.Size(426, 52);
            this.lblTituloCategoria.TabIndex = 0;
            this.lblTituloCategoria.Text = "Catálogo";
            this.lblTituloCategoria.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // flpAcciones
            // 
            this.flpAcciones.Dock = System.Windows.Forms.DockStyle.Right;
            this.flpAcciones.Location = new System.Drawing.Point(442, 8);
            this.flpAcciones.Name = "flpAcciones";
            this.flpAcciones.Padding = new System.Windows.Forms.Padding(4, 6, 4, 6);
            this.flpAcciones.Size = new System.Drawing.Size(530, 52);
            this.flpAcciones.TabIndex = 1;
            this.flpAcciones.WrapContents = false;
            // 
            // flpProductos
            // 
            this.flpProductos.AutoScroll = true;
            this.flpProductos.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(236)))));
            this.flpProductos.Dock = System.Windows.Forms.DockStyle.Fill;
            this.flpProductos.Location = new System.Drawing.Point(0, 68);
            this.flpProductos.Name = "flpProductos";
            this.flpProductos.Padding = new System.Windows.Forms.Padding(18, 16, 18, 16);
            this.flpProductos.Size = new System.Drawing.Size(980, 492);
            this.flpProductos.TabIndex = 0;
            this.flpProductos.WrapContents = true;
            // 
            // FrmCatalogoProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(246)))), ((int)(((byte)(236)))));
            this.ClientSize = new System.Drawing.Size(980, 560);
            this.ControlBox = false;
            this.Controls.Add(this.flpProductos);
            this.Controls.Add(this.pnlCabecera);
            this.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.Name = "FrmCatalogoProductos";
            this.Text = "Catálogo";
            this.Load += new System.EventHandler(this.FrmCatalogoProductos_Load);
            this.pnlCabecera.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel pnlCabecera;
        private System.Windows.Forms.Label lblTituloCategoria;
        private System.Windows.Forms.FlowLayoutPanel flpAcciones;
        private System.Windows.Forms.FlowLayoutPanel flpProductos;
    }
}

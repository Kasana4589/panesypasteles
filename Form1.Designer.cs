namespace CapaPresentacion
{
    partial class FrmVentaProductos
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.msProductos = new System.Windows.Forms.MenuStrip();
            this.descuentosToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.BtnPanSimple = new System.Windows.Forms.ToolStripMenuItem();
            this.panDulceToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.panSaladoToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.pastelesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.salirsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.msProductos.SuspendLayout();
            this.SuspendLayout();
            // 
            // msProductos
            // 
            this.msProductos.Dock = System.Windows.Forms.DockStyle.Left;
            this.msProductos.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.descuentosToolStripMenuItem,
            this.BtnPanSimple,
            this.panDulceToolStripMenuItem,
            this.panSaladoToolStripMenuItem,
            this.pastelesToolStripMenuItem,
            this.salirsToolStripMenuItem});
            this.msProductos.Location = new System.Drawing.Point(0, 0);
            this.msProductos.Name = "msProductos";
            this.msProductos.Size = new System.Drawing.Size(168, 489);
            this.msProductos.TabIndex = 0;
            this.msProductos.Text = "menuStrip1";
            // 
            // descuentosToolStripMenuItem
            // 
            this.descuentosToolStripMenuItem.Name = "descuentosToolStripMenuItem";
            this.descuentosToolStripMenuItem.Size = new System.Drawing.Size(106, 19);
            this.descuentosToolStripMenuItem.Text = "Descuentos";
            // 
            // BtnPanSimple
            // 
            this.BtnPanSimple.Name = "BtnPanSimple";
            this.BtnPanSimple.Size = new System.Drawing.Size(106, 19);
            this.BtnPanSimple.Text = "Pan Simple";
            this.BtnPanSimple.Click += new System.EventHandler(this.BtnPanSimple_Click);
            // 
            // panDulceToolStripMenuItem
            // 
            this.panDulceToolStripMenuItem.Name = "panDulceToolStripMenuItem";
            this.panDulceToolStripMenuItem.Size = new System.Drawing.Size(106, 19);
            this.panDulceToolStripMenuItem.Text = "Pan Dulce";
            this.panDulceToolStripMenuItem.Click += new System.EventHandler(this.panDulceToolStripMenuItem_Click);
            // 
            // panSaladoToolStripMenuItem
            // 
            this.panSaladoToolStripMenuItem.Name = "panSaladoToolStripMenuItem";
            this.panSaladoToolStripMenuItem.Size = new System.Drawing.Size(106, 19);
            this.panSaladoToolStripMenuItem.Text = "Pan Salado";
            this.panSaladoToolStripMenuItem.Click += new System.EventHandler(this.panSaladoToolStripMenuItem_Click);
            // 
            // pastelesToolStripMenuItem
            // 
            this.pastelesToolStripMenuItem.AutoSize = false;
            this.pastelesToolStripMenuItem.Name = "pastelesToolStripMenuItem";
            this.pastelesToolStripMenuItem.Size = new System.Drawing.Size(113, 19);
            this.pastelesToolStripMenuItem.Text = "Pasteles";
            this.pastelesToolStripMenuItem.Click += new System.EventHandler(this.pastelesToolStripMenuItem_Click);
            // 
            // salirsToolStripMenuItem
            // 
            this.salirsToolStripMenuItem.Name = "salirsToolStripMenuItem";
            this.salirsToolStripMenuItem.Size = new System.Drawing.Size(106, 19);
            this.salirsToolStripMenuItem.Text = "Salir";
            this.salirsToolStripMenuItem.Click += new System.EventHandler(this.salirsToolStripMenuItem_Click);
            // 
            // FrmVentaProductos
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 14F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(224)))), ((int)(((byte)(192)))));
            this.ClientSize = new System.Drawing.Size(1317, 489);
            this.Controls.Add(this.msProductos);
            this.Font = new System.Drawing.Font("Baskerville Old Face", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(128)))), ((int)(((byte)(64)))), ((int)(((byte)(64)))));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.IsMdiContainer = true;
            this.MainMenuStrip = this.msProductos;
            this.Margin = new System.Windows.Forms.Padding(4, 3, 4, 3);
            this.Name = "FrmVentaProductos";
            this.Text = "Pan Dulce";
            this.msProductos.ResumeLayout(false);
            this.msProductos.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.MenuStrip msProductos;
        private System.Windows.Forms.ToolStripMenuItem descuentosToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem BtnPanSimple;
        private System.Windows.Forms.ToolStripMenuItem panDulceToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem panSaladoToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem pastelesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem salirsToolStripMenuItem;
    }
}


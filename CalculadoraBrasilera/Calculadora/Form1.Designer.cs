namespace Calculadora
{
    partial class Form1
    {
        // Variable de diseño requerida.
        private System.ComponentModel.IContainer components = null;


        // Borrar los recursos que se utilizan.

        /// <param name="disposing">true si necesita descartar recursos administrados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por C#

        private void InitializeComponent()
        {
            this.edVisor = new System.Windows.Forms.TextBox();
            this.flowLayoutPanel1 = new System.Windows.Forms.FlowLayoutPanel();
            this.edResultado = new System.Windows.Forms.TextBox();
            this.lbSequencias = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // edVisor
            // 
            this.edVisor.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.edVisor.Location = new System.Drawing.Point(12, 53);
            this.edVisor.Name = "edVisor";
            this.edVisor.Size = new System.Drawing.Size(324, 35);
            this.edVisor.TabIndex = 1;
            this.edVisor.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // flowLayoutPanel1
            // 
            this.flowLayoutPanel1.Location = new System.Drawing.Point(12, 155);
            this.flowLayoutPanel1.Name = "flowLayoutPanel1";
            this.flowLayoutPanel1.Size = new System.Drawing.Size(324, 405);
            this.flowLayoutPanel1.TabIndex = 2;
            this.flowLayoutPanel1.Paint += new System.Windows.Forms.PaintEventHandler(this.FlowLayoutPanel1_Paint);
            // 
            // edResultado
            // 
            this.edResultado.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.25F);
            this.edResultado.Location = new System.Drawing.Point(12, 12);
            this.edResultado.Name = "edResultado";
            this.edResultado.Size = new System.Drawing.Size(324, 35);
            this.edResultado.TabIndex = 3;
            this.edResultado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // lbSequencias
            // 
            this.lbSequencias.AutoSize = true;
            this.lbSequencias.Font = new System.Drawing.Font("Microsoft Sans Serif", 16.25F);
            this.lbSequencias.Location = new System.Drawing.Point(7, 104);
            this.lbSequencias.Name = "lbSequencias";
            this.lbSequencias.Size = new System.Drawing.Size(84, 26);
            this.lbSequencias.TabIndex = 4;
            this.lbSequencias.Text = "Pósfixa";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(348, 572);
            this.Controls.Add(this.lbSequencias);
            this.Controls.Add(this.edResultado);
            this.Controls.Add(this.flowLayoutPanel1);
            this.Controls.Add(this.edVisor);
            this.Name = "Form1";
            this.Text = "Form1";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox edVisor;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel1;
        private System.Windows.Forms.TextBox edResultado;
        private System.Windows.Forms.Label lbSequencias;
    }
}


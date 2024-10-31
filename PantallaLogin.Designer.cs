namespace SpookyTime
{
    partial class PantallaLogin
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
            this.btnAdministrador = new System.Windows.Forms.Button();
            this.btnCliente = new System.Windows.Forms.Button();
            this.btnBackDoor = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnAdministrador
            // 
            this.btnAdministrador.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAdministrador.AutoSize = true;
            this.btnAdministrador.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAdministrador.Location = new System.Drawing.Point(307, 122);
            this.btnAdministrador.Name = "btnAdministrador";
            this.btnAdministrador.Size = new System.Drawing.Size(201, 60);
            this.btnAdministrador.TabIndex = 0;
            this.btnAdministrador.Text = "ADMINISTRADOR";
            this.btnAdministrador.UseVisualStyleBackColor = false;
            this.btnAdministrador.Click += new System.EventHandler(this.btnAdministrador_Click);
            // 
            // btnCliente
            // 
            this.btnCliente.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnCliente.AutoSize = true;
            this.btnCliente.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnCliente.Location = new System.Drawing.Point(307, 246);
            this.btnCliente.Name = "btnCliente";
            this.btnCliente.Size = new System.Drawing.Size(201, 60);
            this.btnCliente.TabIndex = 1;
            this.btnCliente.Text = "CLIENTE";
            this.btnCliente.UseVisualStyleBackColor = false;
            this.btnCliente.Click += new System.EventHandler(this.btnCliente_Click);
            // 
            // btnBackDoor
            // 
            this.btnBackDoor.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnBackDoor.AutoSize = true;
            this.btnBackDoor.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnBackDoor.Cursor = System.Windows.Forms.Cursors.No;
            this.btnBackDoor.Location = new System.Drawing.Point(713, 415);
            this.btnBackDoor.Name = "btnBackDoor";
            this.btnBackDoor.Size = new System.Drawing.Size(75, 23);
            this.btnBackDoor.TabIndex = 2;
            this.btnBackDoor.Text = "BackDoor";
            this.btnBackDoor.UseVisualStyleBackColor = false;
            this.btnBackDoor.Click += new System.EventHandler(this.btnBackDoor_Click);
            // 
            // PantallaLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.BackgroundImage = global::SpookyTime.Properties.Resources.LunaSPOOKY;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnBackDoor);
            this.Controls.Add(this.btnCliente);
            this.Controls.Add(this.btnAdministrador);
            this.DoubleBuffered = true;
            this.Name = "PantallaLogin";
            this.Text = "PantallaLogin";
            this.Load += new System.EventHandler(this.PantallaLogin_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnAdministrador;
        private System.Windows.Forms.Button btnCliente;
        private System.Windows.Forms.Button btnBackDoor;
    }
}


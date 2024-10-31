namespace SpookyTime
{
    partial class DentroCasa
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
            this.lblCasa = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.clbACara = new System.Windows.Forms.CheckedListBox();
            this.btnAgarra = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtCantidadAgarra = new System.Windows.Forms.TextBox();
            this.btnRegresar = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.SuspendLayout();
            // 
            // lblCasa
            // 
            this.lblCasa.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.lblCasa.AutoSize = true;
            this.lblCasa.Font = new System.Drawing.Font("Microsoft Sans Serif", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblCasa.Location = new System.Drawing.Point(375, 46);
            this.lblCasa.Name = "lblCasa";
            this.lblCasa.Size = new System.Drawing.Size(99, 31);
            this.lblCasa.TabIndex = 0;
            this.lblCasa.Text = "Casa: ";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.groupBox1.Controls.Add(this.clbACara);
            this.groupBox1.Controls.Add(this.btnAgarra);
            this.groupBox1.Controls.Add(this.label2);
            this.groupBox1.Controls.Add(this.txtCantidadAgarra);
            this.groupBox1.Location = new System.Drawing.Point(293, 80);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(304, 367);
            this.groupBox1.TabIndex = 1;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Agarra los dulces:";
            // 
            // clbACara
            // 
            this.clbACara.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.clbACara.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.clbACara.FormattingEnabled = true;
            this.clbACara.Location = new System.Drawing.Point(6, 19);
            this.clbACara.Name = "clbACara";
            this.clbACara.Size = new System.Drawing.Size(292, 184);
            this.clbACara.TabIndex = 4;
            // 
            // btnAgarra
            // 
            this.btnAgarra.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnAgarra.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(255)))), ((int)(((byte)(128)))), ((int)(((byte)(0)))));
            this.btnAgarra.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnAgarra.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnAgarra.Location = new System.Drawing.Point(106, 266);
            this.btnAgarra.Name = "btnAgarra";
            this.btnAgarra.Size = new System.Drawing.Size(75, 21);
            this.btnAgarra.TabIndex = 3;
            this.btnAgarra.Text = "Agarra";
            this.btnAgarra.UseVisualStyleBackColor = false;
            this.btnAgarra.Click += new System.EventHandler(this.btnAgarra_Click_1);
            // 
            // label2
            // 
            this.label2.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(38, 219);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(111, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "Cuantos quieres?";
            // 
            // txtCantidadAgarra
            // 
            this.txtCantidadAgarra.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.txtCantidadAgarra.BackColor = System.Drawing.Color.MediumSlateBlue;
            this.txtCantidadAgarra.Location = new System.Drawing.Point(164, 218);
            this.txtCantidadAgarra.Name = "txtCantidadAgarra";
            this.txtCantidadAgarra.Size = new System.Drawing.Size(100, 20);
            this.txtCantidadAgarra.TabIndex = 1;
            // 
            // btnRegresar
            // 
            this.btnRegresar.Anchor = System.Windows.Forms.AnchorStyles.None;
            this.btnRegresar.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.btnRegresar.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
            this.btnRegresar.ForeColor = System.Drawing.SystemColors.Control;
            this.btnRegresar.Image = global::SpookyTime.Properties.Resources.BACK;
            this.btnRegresar.ImageAlign = System.Drawing.ContentAlignment.MiddleLeft;
            this.btnRegresar.Location = new System.Drawing.Point(12, 12);
            this.btnRegresar.Name = "btnRegresar";
            this.btnRegresar.Size = new System.Drawing.Size(69, 31);
            this.btnRegresar.TabIndex = 6;
            this.btnRegresar.Text = "Regresar";
            this.btnRegresar.UseVisualStyleBackColor = true;
            this.btnRegresar.Click += new System.EventHandler(this.btnRegresar_Click_1);
            // 
            // DentroCasa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateBlue;
            this.BackgroundImage = global::SpookyTime.Properties.Resources.DentroDeCasa;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ClientSize = new System.Drawing.Size(887, 498);
            this.Controls.Add(this.btnRegresar);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.lblCasa);
            this.Name = "DentroCasa";
            this.Text = "DentroCasa";
            this.Load += new System.EventHandler(this.DentroCasa_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblCasa;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btnAgarra;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtCantidadAgarra;
        private System.Windows.Forms.Button btnRegresar;
        private System.Windows.Forms.CheckedListBox clbACara;
    }
}
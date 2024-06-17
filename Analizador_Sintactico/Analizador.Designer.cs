
namespace Analizador_Sintactico
{
    partial class Analizador
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Analizador));
            this.DGV_Sintactico = new System.Windows.Forms.DataGridView();
            this.Column1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.Column2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.BSintactico = new System.Windows.Forms.Button();
            this.BLexico = new System.Windows.Forms.Button();
            this.dgv_Lexico = new System.Windows.Forms.DataGridView();
            this.c1 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c2 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c3 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.c4 = new System.Windows.Forms.DataGridViewTextBoxColumn();
            this.TCodigo = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1 = new System.Windows.Forms.Panel();
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sintactico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Lexico)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // DGV_Sintactico
            // 
            this.DGV_Sintactico.AllowUserToAddRows = false;
            this.DGV_Sintactico.AllowUserToDeleteRows = false;
            this.DGV_Sintactico.BackgroundColor = System.Drawing.Color.White;
            this.DGV_Sintactico.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.DGV_Sintactico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.Column1,
            this.Column2});
            this.DGV_Sintactico.Location = new System.Drawing.Point(566, 402);
            this.DGV_Sintactico.Name = "DGV_Sintactico";
            this.DGV_Sintactico.ReadOnly = true;
            this.DGV_Sintactico.Size = new System.Drawing.Size(603, 245);
            this.DGV_Sintactico.TabIndex = 14;
            // 
            // Column1
            // 
            this.Column1.HeaderText = "ESTRUCTURA";
            this.Column1.MinimumWidth = 100;
            this.Column1.Name = "Column1";
            this.Column1.ReadOnly = true;
            // 
            // Column2
            // 
            this.Column2.HeaderText = "DETALLE";
            this.Column2.MinimumWidth = 460;
            this.Column2.Name = "Column2";
            this.Column2.ReadOnly = true;
            this.Column2.Width = 460;
            // 
            // BSintactico
            // 
            this.BSintactico.BackColor = System.Drawing.Color.Lime;
            this.BSintactico.Enabled = false;
            this.BSintactico.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BSintactico.ForeColor = System.Drawing.Color.White;
            this.BSintactico.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.BSintactico.Location = new System.Drawing.Point(818, 371);
            this.BSintactico.Name = "BSintactico";
            this.BSintactico.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.BSintactico.Size = new System.Drawing.Size(177, 30);
            this.BSintactico.TabIndex = 13;
            this.BSintactico.Text = "Comenzar";
            this.BSintactico.UseVisualStyleBackColor = false;
            this.BSintactico.Click += new System.EventHandler(this.BSintactico_Click);
            // 
            // BLexico
            // 
            this.BLexico.BackColor = System.Drawing.Color.Lime;
            this.BLexico.Font = new System.Drawing.Font("Consolas", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BLexico.ForeColor = System.Drawing.Color.White;
            this.BLexico.Location = new System.Drawing.Point(263, 524);
            this.BLexico.Name = "BLexico";
            this.BLexico.Size = new System.Drawing.Size(192, 29);
            this.BLexico.TabIndex = 12;
            this.BLexico.Text = "Empezar";
            this.BLexico.UseVisualStyleBackColor = false;
            this.BLexico.Click += new System.EventHandler(this.BLexico_Click);
            // 
            // dgv_Lexico
            // 
            this.dgv_Lexico.AllowUserToAddRows = false;
            this.dgv_Lexico.AllowUserToDeleteRows = false;
            this.dgv_Lexico.BackgroundColor = System.Drawing.Color.White;
            this.dgv_Lexico.ColumnHeadersHeight = 20;
            this.dgv_Lexico.Columns.AddRange(new System.Windows.Forms.DataGridViewColumn[] {
            this.c1,
            this.c2,
            this.c3,
            this.c4});
            this.dgv_Lexico.Location = new System.Drawing.Point(60, 559);
            this.dgv_Lexico.Name = "dgv_Lexico";
            this.dgv_Lexico.ReadOnly = true;
            this.dgv_Lexico.RowHeadersWidth = 15;
            this.dgv_Lexico.Size = new System.Drawing.Size(395, 156);
            this.dgv_Lexico.TabIndex = 11;
            // 
            // c1
            // 
            this.c1.HeaderText = "LEXEMA";
            this.c1.MinimumWidth = 50;
            this.c1.Name = "c1";
            this.c1.ReadOnly = true;
            this.c1.Width = 150;
            // 
            // c2
            // 
            this.c2.HeaderText = "TOKEN";
            this.c2.MinimumWidth = 50;
            this.c2.Name = "c2";
            this.c2.ReadOnly = true;
            // 
            // c3
            // 
            this.c3.HeaderText = "LINEA";
            this.c3.MinimumWidth = 50;
            this.c3.Name = "c3";
            this.c3.ReadOnly = true;
            this.c3.Width = 50;
            // 
            // c4
            // 
            this.c4.HeaderText = "COLUMNA";
            this.c4.MinimumWidth = 65;
            this.c4.Name = "c4";
            this.c4.ReadOnly = true;
            this.c4.Width = 65;
            // 
            // TCodigo
            // 
            this.TCodigo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.TCodigo.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TCodigo.ForeColor = System.Drawing.SystemColors.InfoText;
            this.TCodigo.Location = new System.Drawing.Point(51, 263);
            this.TCodigo.Multiline = true;
            this.TCodigo.Name = "TCodigo";
            this.TCodigo.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.TCodigo.Size = new System.Drawing.Size(404, 245);
            this.TCodigo.TabIndex = 10;
            this.TCodigo.TextChanged += new System.EventHandler(this.TCodigo_TextChanged);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.ForeColor = System.Drawing.Color.White;
            this.label4.Location = new System.Drawing.Point(72, 238);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(230, 22);
            this.label4.TabIndex = 18;
            this.label4.Text = "Coloque El Código Aquí";
            // 
            // pictureBox1
            // 
            this.pictureBox1.Image = ((System.Drawing.Image)(resources.GetObject("pictureBox1.Image")));
            this.pictureBox1.Location = new System.Drawing.Point(51, 34);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(1099, 107);
            this.pictureBox1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pictureBox1.TabIndex = 19;
            this.pictureBox1.TabStop = false;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Consolas", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.ForeColor = System.Drawing.Color.White;
            this.label5.Location = new System.Drawing.Point(537, 155);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(234, 19);
            this.label5.TabIndex = 20;
            this.label5.Text = "Kelvin R.Torres 1-16-7283";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.ForeColor = System.Drawing.Color.White;
            this.label2.Location = new System.Drawing.Point(562, 377);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(230, 22);
            this.label2.TabIndex = 21;
            this.label2.Text = " Analizador Sintáctico";
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Consolas", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.Color.White;
            this.label6.Location = new System.Drawing.Point(56, 524);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(180, 22);
            this.label6.TabIndex = 22;
            this.label6.Text = "Analizador Léxico";
            // 
            // panel1
            // 
            this.panel1.BackColor = System.Drawing.Color.White;
            this.panel1.Location = new System.Drawing.Point(51, 216);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1100, 10);
            this.panel1.TabIndex = 23;
            this.panel1.Paint += new System.Windows.Forms.PaintEventHandler(this.panel1_Paint);
            // 
            // Analizador
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.DarkSlateGray;
            this.ClientSize = new System.Drawing.Size(1208, 749);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.DGV_Sintactico);
            this.Controls.Add(this.BSintactico);
            this.Controls.Add(this.BLexico);
            this.Controls.Add(this.dgv_Lexico);
            this.Controls.Add(this.TCodigo);
            this.MaximizeBox = false;
            this.Name = "Analizador";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Analizador Lexico y Sintáctico ";
            this.Load += new System.EventHandler(this.Analizador_Load);
            ((System.ComponentModel.ISupportInitialize)(this.DGV_Sintactico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgv_Lexico)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DataGridView DGV_Sintactico;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column1;
        private System.Windows.Forms.DataGridViewTextBoxColumn Column2;
        private System.Windows.Forms.Button BSintactico;
        private System.Windows.Forms.Button BLexico;
        private System.Windows.Forms.DataGridView dgv_Lexico;
        private System.Windows.Forms.DataGridViewTextBoxColumn c1;
        private System.Windows.Forms.DataGridViewTextBoxColumn c2;
        private System.Windows.Forms.DataGridViewTextBoxColumn c3;
        private System.Windows.Forms.DataGridViewTextBoxColumn c4;
        private System.Windows.Forms.TextBox TCodigo;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Panel panel1;
    }
}


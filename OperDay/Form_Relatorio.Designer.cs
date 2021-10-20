
namespace OperDay
{
    partial class Form_Relatorio
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
            this.label1 = new System.Windows.Forms.Label();
            this.txtDateInit = new System.Windows.Forms.DateTimePicker();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDateFins = new System.Windows.Forms.DateTimePicker();
            this.cmbAcao = new System.Windows.Forms.ComboBox();
            this.cmbConta = new System.Windows.Forms.ComboBox();
            this.label7 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.btnExportar = new System.Windows.Forms.Button();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(51, 15);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(63, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Data Inicial:";
            // 
            // txtDateInit
            // 
            this.txtDateInit.Location = new System.Drawing.Point(56, 30);
            this.txtDateInit.Name = "txtDateInit";
            this.txtDateInit.Size = new System.Drawing.Size(224, 20);
            this.txtDateInit.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(51, 60);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(58, 13);
            this.label2.TabIndex = 21;
            this.label2.Text = "Data Final:";
            // 
            // txtDateFins
            // 
            this.txtDateFins.Location = new System.Drawing.Point(56, 75);
            this.txtDateFins.Name = "txtDateFins";
            this.txtDateFins.Size = new System.Drawing.Size(224, 20);
            this.txtDateFins.TabIndex = 2;
            // 
            // cmbAcao
            // 
            this.cmbAcao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbAcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcao.FormattingEnabled = true;
            this.cmbAcao.Location = new System.Drawing.Point(105, 171);
            this.cmbAcao.Name = "cmbAcao";
            this.cmbAcao.Size = new System.Drawing.Size(121, 21);
            this.cmbAcao.TabIndex = 4;
            this.cmbAcao.Click += new System.EventHandler(this.cmbAcao_Click);
            // 
            // cmbConta
            // 
            this.cmbConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbConta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConta.FormattingEnabled = true;
            this.cmbConta.Location = new System.Drawing.Point(105, 124);
            this.cmbConta.Name = "cmbConta";
            this.cmbConta.Size = new System.Drawing.Size(121, 21);
            this.cmbConta.TabIndex = 3;
            this.cmbConta.Click += new System.EventHandler(this.cmbConta_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(102, 108);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 22;
            this.label7.Text = "Conta:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(102, 155);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 23;
            this.label4.Text = "Ação:";
            // 
            // btnExportar
            // 
            this.btnExportar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExportar.Location = new System.Drawing.Point(85, 220);
            this.btnExportar.Name = "btnExportar";
            this.btnExportar.Size = new System.Drawing.Size(75, 23);
            this.btnExportar.TabIndex = 5;
            this.btnExportar.Text = "Exportar";
            this.btnExportar.UseVisualStyleBackColor = true;
            this.btnExportar.Click += new System.EventHandler(this.btnExportar_Click);
            // 
            // btnLimpar
            // 
            this.btnLimpar.Location = new System.Drawing.Point(166, 220);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 6;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // Form_Relatorio
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(344, 255);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.btnExportar);
            this.Controls.Add(this.cmbAcao);
            this.Controls.Add(this.cmbConta);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtDateFins);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDateInit);
            this.Name = "Form_Relatorio";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Relatório";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DateTimePicker txtDateInit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DateTimePicker txtDateFins;
        private System.Windows.Forms.ComboBox cmbAcao;
        private System.Windows.Forms.ComboBox cmbConta;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button btnExportar;
        private System.Windows.Forms.Button btnLimpar;
    }
}
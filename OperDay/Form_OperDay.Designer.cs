
namespace OperDay
{
    partial class Form_OperDay
    {
        /// <summary>
        /// Variável de designer necessária.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpar os recursos que estão sendo usados.
        /// </summary>
        /// <param name="disposing">true se for necessário descartar os recursos gerenciados; caso contrário, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código gerado pelo Windows Form Designer

        /// <summary>
        /// Método necessário para suporte ao Designer - não modifique 
        /// o conteúdo deste método com o editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtDate = new System.Windows.Forms.DateTimePicker();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtID = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.btnInserir = new System.Windows.Forms.Button();
            this.btnExcluir = new System.Windows.Forms.Button();
            this.btnEditar = new System.Windows.Forms.Button();
            this.btnConsultar = new System.Windows.Forms.Button();
            this.label7 = new System.Windows.Forms.Label();
            this.btnLimpar = new System.Windows.Forms.Button();
            this.btnCadastrarConta = new System.Windows.Forms.Button();
            this.btnCadastrarAcao = new System.Windows.Forms.Button();
            this.cmbConta = new System.Windows.Forms.ComboBox();
            this.cmbAcao = new System.Windows.Forms.ComboBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtValorOp = new System.Windows.Forms.TextBox();
            this.cmbTipoOperacao = new System.Windows.Forms.ComboBox();
            this.btnRelatorio = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // txtDate
            // 
            this.txtDate.Location = new System.Drawing.Point(23, 24);
            this.txtDate.Name = "txtDate";
            this.txtDate.Size = new System.Drawing.Size(224, 20);
            this.txtDate.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(18, 9);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 13);
            this.label1.TabIndex = 20;
            this.label1.Text = "Data da Operação:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(20, 142);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(84, 13);
            this.label3.TabIndex = 23;
            this.label3.Text = "Valor Operação:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(20, 95);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(35, 13);
            this.label4.TabIndex = 22;
            this.label4.Text = "Ação:";
            // 
            // txtID
            // 
            this.txtID.Location = new System.Drawing.Point(444, 22);
            this.txtID.Name = "txtID";
            this.txtID.Size = new System.Drawing.Size(24, 20);
            this.txtID.TabIndex = 12;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(447, 6);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(21, 13);
            this.label5.TabIndex = 25;
            this.label5.Text = "ID:";
            // 
            // btnInserir
            // 
            this.btnInserir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnInserir.Location = new System.Drawing.Point(12, 195);
            this.btnInserir.Name = "btnInserir";
            this.btnInserir.Size = new System.Drawing.Size(75, 23);
            this.btnInserir.TabIndex = 8;
            this.btnInserir.Text = "Inserir";
            this.btnInserir.UseVisualStyleBackColor = true;
            this.btnInserir.Click += new System.EventHandler(this.btnInserir_Click);
            // 
            // btnExcluir
            // 
            this.btnExcluir.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnExcluir.Enabled = false;
            this.btnExcluir.Location = new System.Drawing.Point(93, 195);
            this.btnExcluir.Name = "btnExcluir";
            this.btnExcluir.Size = new System.Drawing.Size(75, 23);
            this.btnExcluir.TabIndex = 9;
            this.btnExcluir.Text = "Excluir";
            this.btnExcluir.UseVisualStyleBackColor = true;
            this.btnExcluir.Click += new System.EventHandler(this.btnExcluir_Click);
            // 
            // btnEditar
            // 
            this.btnEditar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnEditar.Enabled = false;
            this.btnEditar.Location = new System.Drawing.Point(174, 195);
            this.btnEditar.Name = "btnEditar";
            this.btnEditar.Size = new System.Drawing.Size(75, 23);
            this.btnEditar.TabIndex = 10;
            this.btnEditar.Text = "Editar";
            this.btnEditar.UseVisualStyleBackColor = true;
            this.btnEditar.Click += new System.EventHandler(this.btnEditar_Click);
            // 
            // btnConsultar
            // 
            this.btnConsultar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnConsultar.Location = new System.Drawing.Point(418, 48);
            this.btnConsultar.Name = "btnConsultar";
            this.btnConsultar.Size = new System.Drawing.Size(75, 23);
            this.btnConsultar.TabIndex = 13;
            this.btnConsultar.Text = "Consultar";
            this.btnConsultar.UseVisualStyleBackColor = true;
            this.btnConsultar.Click += new System.EventHandler(this.btnConsultar_Click);
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(20, 48);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(38, 13);
            this.label7.TabIndex = 21;
            this.label7.Text = "Conta:";
            // 
            // btnLimpar
            // 
            this.btnLimpar.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnLimpar.Location = new System.Drawing.Point(255, 195);
            this.btnLimpar.Name = "btnLimpar";
            this.btnLimpar.Size = new System.Drawing.Size(75, 23);
            this.btnLimpar.TabIndex = 11;
            this.btnLimpar.Text = "Limpar";
            this.btnLimpar.UseVisualStyleBackColor = true;
            this.btnLimpar.Click += new System.EventHandler(this.btnLimpar_Click);
            // 
            // btnCadastrarConta
            // 
            this.btnCadastrarConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrarConta.Location = new System.Drawing.Point(174, 62);
            this.btnCadastrarConta.Name = "btnCadastrarConta";
            this.btnCadastrarConta.Size = new System.Drawing.Size(97, 23);
            this.btnCadastrarConta.TabIndex = 3;
            this.btnCadastrarConta.Text = "Cadastrar Conta";
            this.btnCadastrarConta.UseVisualStyleBackColor = true;
            this.btnCadastrarConta.Click += new System.EventHandler(this.btnCadastrarConta_Click);
            // 
            // btnCadastrarAcao
            // 
            this.btnCadastrarAcao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCadastrarAcao.Location = new System.Drawing.Point(174, 111);
            this.btnCadastrarAcao.Name = "btnCadastrarAcao";
            this.btnCadastrarAcao.Size = new System.Drawing.Size(99, 23);
            this.btnCadastrarAcao.TabIndex = 5;
            this.btnCadastrarAcao.Text = "Cadastrar Ação";
            this.btnCadastrarAcao.UseVisualStyleBackColor = true;
            this.btnCadastrarAcao.Click += new System.EventHandler(this.btnCadastrarAção_Click);
            // 
            // cmbConta
            // 
            this.cmbConta.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbConta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbConta.FormattingEnabled = true;
            this.cmbConta.ItemHeight = 13;
            this.cmbConta.Location = new System.Drawing.Point(23, 64);
            this.cmbConta.Name = "cmbConta";
            this.cmbConta.Size = new System.Drawing.Size(121, 21);
            this.cmbConta.TabIndex = 2;
            this.cmbConta.Click += new System.EventHandler(this.cmbConta_Click);
            // 
            // cmbAcao
            // 
            this.cmbAcao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbAcao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbAcao.FormattingEnabled = true;
            this.cmbAcao.Location = new System.Drawing.Point(23, 111);
            this.cmbAcao.Name = "cmbAcao";
            this.cmbAcao.Size = new System.Drawing.Size(121, 21);
            this.cmbAcao.TabIndex = 4;
            this.cmbAcao.Click += new System.EventHandler(this.cmbAcao_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(171, 142);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(31, 13);
            this.label2.TabIndex = 24;
            this.label2.Text = "Tipo:";
            // 
            // txtValorOp
            // 
            this.txtValorOp.Location = new System.Drawing.Point(23, 158);
            this.txtValorOp.Name = "txtValorOp";
            this.txtValorOp.Size = new System.Drawing.Size(100, 20);
            this.txtValorOp.TabIndex = 6;
            this.txtValorOp.Leave += new System.EventHandler(this.txtValorOp_Leave);
            // 
            // cmbTipoOperacao
            // 
            this.cmbTipoOperacao.Cursor = System.Windows.Forms.Cursors.Hand;
            this.cmbTipoOperacao.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cmbTipoOperacao.FormattingEnabled = true;
            this.cmbTipoOperacao.Location = new System.Drawing.Point(174, 158);
            this.cmbTipoOperacao.Name = "cmbTipoOperacao";
            this.cmbTipoOperacao.Size = new System.Drawing.Size(121, 21);
            this.cmbTipoOperacao.TabIndex = 7;
            // 
            // btnRelatorio
            // 
            this.btnRelatorio.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnRelatorio.Location = new System.Drawing.Point(421, 193);
            this.btnRelatorio.Name = "btnRelatorio";
            this.btnRelatorio.Size = new System.Drawing.Size(75, 23);
            this.btnRelatorio.TabIndex = 14;
            this.btnRelatorio.Text = "Relatório";
            this.btnRelatorio.UseVisualStyleBackColor = true;
            this.btnRelatorio.Click += new System.EventHandler(this.btnRelatorio_Click);
            // 
            // Form_OperDay
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(508, 228);
            this.Controls.Add(this.btnRelatorio);
            this.Controls.Add(this.txtValorOp);
            this.Controls.Add(this.cmbTipoOperacao);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cmbAcao);
            this.Controls.Add(this.cmbConta);
            this.Controls.Add(this.btnCadastrarAcao);
            this.Controls.Add(this.btnCadastrarConta);
            this.Controls.Add(this.btnLimpar);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.btnConsultar);
            this.Controls.Add(this.btnEditar);
            this.Controls.Add(this.btnExcluir);
            this.Controls.Add(this.btnInserir);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtID);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtDate);
            this.Name = "Form_OperDay";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "OperDay";
            this.Load += new System.EventHandler(this.Form_OperDay_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.DateTimePicker txtDate;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtID;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Button btnInserir;
        private System.Windows.Forms.Button btnExcluir;
        private System.Windows.Forms.Button btnEditar;
        private System.Windows.Forms.Button btnConsultar;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Button btnLimpar;
        private System.Windows.Forms.Button btnCadastrarConta;
        private System.Windows.Forms.Button btnCadastrarAcao;
        private System.Windows.Forms.ComboBox cmbConta;
        private System.Windows.Forms.ComboBox cmbAcao;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtValorOp;
        private System.Windows.Forms.ComboBox cmbTipoOperacao;
        private System.Windows.Forms.Button btnRelatorio;
    }
}


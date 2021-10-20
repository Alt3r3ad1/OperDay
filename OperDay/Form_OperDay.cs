using System;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_OperDay : Form
    {
        public Form_OperDay()
        {
            //EXECUTA AS VERIFICACOES E ATUALIZACOES NECESSARIAS NA INICIALIZACAO DO FORMULARIO
            InitializeComponent();
            cmbConta_Update();
            cmbAcao_Update();
            cmbTipoOperacao_Update();
            txtValorOp_Update();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "INSERIR" E CLICADO
        private void btnInserir_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "VALOR OPERACAO" TEM UM OU MAIS ACONTECIMENTOS DE NUMEROS DE 0-9 ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtValorOp.Text, "[0-9]+"))
            {
                //DEFINE OS VALORES DAS VARIAVEIS NECESSARIAS PARA INSERIR
                Operacao oper = new Operacao();
                oper.dateOperation = Convert.ToDateTime(txtDate.Text);
                oper.vlrOperation = Convert.ToDouble(txtValorOp.Text);
                oper.idConta = Convert.ToInt32(cmbConta.SelectedValue);
                oper.idAcao = Convert.ToInt32(cmbAcao.SelectedValue);
                oper.tpOperation = cmbTipoOperacao.SelectedIndex;
                //EXECUTA FUNÇÃO DE INSERT NO BANCO DE DADOS
                oper.InserirOperacao();
                //EXECUTA A FUNÇÃO DO BOTAO LIMPAR
                btnLimpar_Click(sender, e);
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE NUMEROS DE 0-9
            else
            {
                MessageBox.Show("O campo 'Valor Operação' precisa ser um número decimal válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EXCLUIR" É CLICADO
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EXCLUIR
            Operacao oper = new Operacao();
            oper.idOperation = Convert.ToInt32(txtID.Text);
            //EXECUTA FUNÇÃO DE DELETE NO BANCO DE DADOS
            oper.ExcluirOperacao();
            //EXECUTA A FUNÇÃO DO BOTÃO LIMPAR
            btnLimpar_Click(sender, e);
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "CONSULTAR" É CLICADO
        private void btnConsultar_Click(object sender, EventArgs e)
        {
            //VERIFICAR SE O CAMPO "ID" ESTA VAZIO
            if (txtID.Text != String.Empty)
            {
                try
                {
                    //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA CONSULTAR
                    Operacao oper = new Operacao();
                    oper.idOperation = Convert.ToInt32(txtID.Text);
                    //EXECUTA FUNÇÃO DE SELECT NO BANCO DE DADOS
                    oper.ConsultarOperacao();
                    txtID.Text = Convert.ToString(oper.idOperation);
                    txtDate.Text = Convert.ToString(oper.dateOperation);
                    txtValorOp.Text = Convert.ToString(oper.vlrOperation);
                    cmbConta.SelectedValue = Convert.ToString(oper.idConta);
                    cmbAcao.SelectedValue = Convert.ToString(oper.idAcao);
                    cmbTipoOperacao.SelectedIndex = oper.tpOperation;
                    //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
                    btnEditar.Enabled = true;
                    txtID.ReadOnly = true;
                    btnInserir.Enabled = false;
                    btnExcluir.Enabled = true;
                    //EXECUTA A FUNÇÃO DE RETIRADA DE FOCO DO CAMPO "VALOR OPERACAO"
                    txtValorOp_Leave(sender, e);
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO INCORRETA DE VALORES COMO " "
                catch
                {
                    MessageBox.Show("Verique o número de ID da operação, ou digite o valor '0' para visualizar a última operação inserida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    btnLimpar_Click(sender, e);
                }
            }
            //SE O CAMPO ESTIVER VAZIO
            else
            {
                //CRIA UM NOVO FORMULARIO "FORM_GRIDVIEW"
                var form = new Form_GridView();
                //EXECUTA A FUNÇÃO "FORMGRIDVIEW" PASSANDO O FORMULARIO ABERTO COMO PARAMETRO
                form.FormGridView(this);
                //ABERTURA DO "FORMGRIDVIEW"
                form.ShowDialog();
                //VERIFICACAO DO CAMPO "ID" SER O VALOR "0"
                if (form.idConsulta != 0)
                {
                    //DEFINE A VARIAVEL "IDCONSULTA" DO "FORMGRIDVIEW"
                    txtID.Text = Convert.ToString(form.idConsulta);
                    //EXECUTA A FUNÇÃO DO BOTÃO CONSULTAR
                    btnConsultar_Click(sender, e);
                }
            }
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "LIMPAR" É CLICADO
        public void btnLimpar_Click(object sender, EventArgs e)
        {
            //PASSA O CONTROLES EXISTENTE NO FORMULARIO PARA A FUNCAO "LIMPAROPERACAO"
            Operacao oper = new Operacao();
            oper.LimparOperacao(this.Controls);
            txtValorOp_Update();
            //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
            cmbTipoOperacao.SelectedIndex = 0;
            txtID.ReadOnly = false;
            btnInserir.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;

        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EDITAR" É CLICADO
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "VALOR OPERACAO" TEM UM OU MAIS ACONTECIMENTOS DE NUMEROS DE 0-9 ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtValorOp.Text, "[0-9]+"))
            {
                //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EDITAR
                Operacao oper = new Operacao();
                oper.idOperation = Convert.ToInt32(txtID.Text);
                oper.dateOperation = Convert.ToDateTime(txtDate.Text);
                oper.vlrOperation = Convert.ToDouble(txtValorOp.Text);
                oper.idConta = Convert.ToInt32(cmbConta.SelectedValue);
                oper.idAcao = Convert.ToInt32(cmbAcao.SelectedValue);
                oper.tpOperation = cmbTipoOperacao.SelectedIndex;
                //EXECUTA FUNÇÃO DE UPDATE NO BANCO DE DADOS
                oper.EditarOperacao();
                btnLimpar_Click(sender, e);
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE NUMEROS DE 0-9
            else
            {
                MessageBox.Show("O campo 'Valor Operação' precisa ser um número decimal válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "CADASTRAR CONTA" É CLICADO
        private void btnCadastrarConta_Click(object sender, EventArgs e)
        {
            //CRIA UM NOVO FORMULARIO "FORM_CONTA"
            var form = new Form_Conta();
            //ABERTURA DO FORMULARIO "FORM_CONTA"
            form.ShowDialog();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "CADASTRAR ACAO" É CLICADO
        private void btnCadastrarAção_Click(object sender, EventArgs e)
        {
            //CRIA UM NOVO FORMULARIO "FORM_ACAO"
            var form = new Form_Acao();
            //ABERTURA DO FORMULARIO "FORM_ACAO"
            form.ShowDialog();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "RELATORIO" É CLICADO
        private void btnRelatorio_Click(object sender, EventArgs e)
        {
            //CRIA UM NOVO FORMULARIO "FORM_RELATORIO"
            var form = new Form_Relatorio();
            //ABERTURA DO FORMULARIO "FORM_ACAO"
            form.ShowDialog();
        }

        //REALIZADA A ATUALIZAÇÃO DO COMBOBOX "CONTA"
        private void cmbConta_Update()
        {

            Operacao oper = new Operacao();
            cmbConta.DataSource = oper.PopularComboBoxContaOper();
            cmbConta.ValueMember = "id_Conta";
            cmbConta.DisplayMember = "corr_Conta";
            cmbConta.SelectedIndex = -1;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O COMBOBOX "CONTA" É CLICADO
        public void cmbConta_Click(object sender, EventArgs e)
        {
            cmbConta_Update();
        }

        //REALIZADA A ATUALIZAÇÃO DO COMBOBOX "ACAO"
        private void cmbAcao_Update()
        {

            Operacao oper = new Operacao();
            cmbAcao.DataSource = oper.PopularComboBoxAcaoOper();
            cmbAcao.ValueMember = "id_Action";
            cmbAcao.DisplayMember = "nm_Action";
            cmbAcao.SelectedIndex = -1;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O COMBOBOX "ACAO" É CLICADO
        public void cmbAcao_Click(object sender, EventArgs e)
        {
            cmbAcao_Update();
        }

        //REALIZADA A ATUALIZAÇÃO DO COMBOBOX "TIPO OPERACAO"
        private void cmbTipoOperacao_Update()
        {
            cmbTipoOperacao.Items.Insert(0, "Compra");
            cmbTipoOperacao.Items.Insert(1, "Venda");
            cmbTipoOperacao.SelectedIndex = 0;
        }

        //REALIZADA A ATUALIZAÇÃO DO TEXTBOX "VALOR OPERACAO"
        private void txtValorOp_Update()
        {
            txtValorOp.Text = "0,00";
            //CONVERTE PARA TIPO DE STRING MONETARIA
            txtValorOp.Text = Convert.ToDecimal(txtValorOp.Text).ToString("C");
            //REMOVE O CAMPO A INSERÇÃO DO "R$" DA STRING MONETARIA
            txtValorOp.Text = txtValorOp.Text.Replace("R$ ", "");
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O TEXTBOX "VALOR OPERACAO" É TIRADO DE FOCO
        private void txtValorOp_Leave(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "VALOR OPERACAO" TEM UM OU MAIS ACONTECIMENTOS DE NUMEROS DE 0-9 ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtValorOp.Text, "[0-9]+"))
            {
                //CONVERTE PARA TIPO DE STRING MONETARIA
                txtValorOp.Text = Convert.ToDecimal(txtValorOp.Text).ToString("C");
                //REMOVE O CAMPO A INSERÇÃO DO "R$" DA STRING MONETARIA
                txtValorOp.Text = txtValorOp.Text.Replace("R$ ", "");
            }
            //ESTE "IF" FICARÁ SEM O "ELSE" PARA TRATIVA DE EXCEÇÃO POIS A TRATIVA EM QUESTÃO ESTÃO SENDO REALIZADA NO MOMENTO DE INSERÇÃO 
        }

        private void Form_OperDay_Load(object sender, EventArgs e)
        {

        }
    }
}

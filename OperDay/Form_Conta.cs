using System;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_Conta : Form
    {
        public Form_Conta()
        {
            //EXECUTA AS VERIFICACOES E ATUALIZACOES NECESSARIAS NA INICIALIZACAO DO FORMULARIO
            InitializeComponent();
            cmbStatus_Update();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "INSERIR" E CLICADO
        private void btnInserir_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "CORRETORA" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtCorretora.Text, "[A-z]+"))
            {
                //VERIFICA SE O CAMPO "ATIVO" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
                if (System.Text.RegularExpressions.Regex.IsMatch(txtAtivo.Text, "[A-z]+"))
                {
                    //DEFINE OS VALORES DAS VARIAVEIS NECESSARIAS PARA INSERIR
                    Conta conta = new Conta();
                    conta.corrConta = txtCorretora.Text;
                    conta.stsConta = Convert.ToString(cmbStatus.SelectedIndex);
                    conta.atvConta = txtAtivo.Text;
                    //EXECUTA FUNÇÃO DE INSERT NO BANCO DE DADOS
                    conta.InserirConta();
                    //EXECUTA A FUNÇÃO DO BOTAO LIMPAR
                    btnLimpar_Click(sender, e);
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
                else
                {
                    MessageBox.Show("O campo 'Ativo' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
            else
            {
                MessageBox.Show("O campo 'Corretora' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EXCLUIR" É CLICADO
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EXCLUIR
            Conta conta = new Conta();
            conta.idConta = Convert.ToInt32(txtID.Text);
            //EXECUTA FUNÇÃO DE DELETE NO BANCO DE DADOS
            conta.ExcluirConta();
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
                    Conta conta = new Conta();
                    conta.idConta = Convert.ToInt32(txtID.Text);
                    //EXECUTA FUNÇÃO DE SELECT NO BANCO DE DADOS
                    conta.ConsultarConta();
                    txtID.Text = Convert.ToString(conta.idConta);
                    txtCorretora.Text = conta.corrConta;
                    cmbStatus.SelectedIndex = Convert.ToInt32(conta.stsConta);
                    txtAtivo.Text = conta.atvConta;
                    //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
                    btnEditar.Enabled = true;
                    txtID.ReadOnly = true;
                    btnInserir.Enabled = false;
                    btnExcluir.Enabled = true;
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO INCORRETA DE VALORES COMO " "
                catch
                {
                    MessageBox.Show("Verique o número de ID da conta, ou digite o valor '0' para visualizar a última conta inserida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //PASSA O CONTROLES EXISTENTE NO FORMULARIO PARA A FUNCAO "LIMPAREMPRESA"
            Conta conta = new Conta();
            conta.LimparConta(this.Controls);
            //SELECIONA O INDEX "0" DO COMBOBOX "STATUS"
            cmbStatus.SelectedIndex = 0;
            //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
            txtID.ReadOnly = false;
            btnInserir.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EDITAR" É CLICADO
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "CORRETORA" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtCorretora.Text, "[A-z]+"))
            {
                //VERIFICA SE O CAMPO "ATIVO" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
                if (System.Text.RegularExpressions.Regex.IsMatch(txtAtivo.Text, "[A-z]+"))
                {
                    //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EDITAR
                    Conta conta = new Conta();
                    conta.idConta = Convert.ToInt32(txtID.Text);
                    conta.corrConta = txtCorretora.Text;
                    conta.stsConta = Convert.ToString(cmbStatus.SelectedIndex);
                    conta.atvConta = txtAtivo.Text;
                    //EXECUTA FUNÇÃO DE UPDATE NO BANCO DE DADOS
                    conta.EditarConta();
                    btnLimpar_Click(sender, e);
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
                else
                {
                    MessageBox.Show("O campo 'Ativo' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
            else
            {
                MessageBox.Show("O campo 'Corretora' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //ADICIONA ITEM AO COMBOBOX "STATUS" ALEM DE DEFINIR VALORES E OS NOMES A SEREM EXEBIDOS - SELECAO COMECA COM INDEX "0"
        private void cmbStatus_Update()
        {
            cmbStatus.Items.Insert(0, "Ativa");
            cmbStatus.Items.Insert(1, "Inativa");
            cmbStatus.SelectedIndex = 0;
        }
    }
}

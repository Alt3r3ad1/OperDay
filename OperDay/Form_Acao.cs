using System;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_Acao : Form
    {
        public Form_Acao()
        {
            //EXECUTA AS VERIFICACOES E ATUALIZACOES NECESSARIAS NA INICIALIZACAO DO FORMULARIO
            InitializeComponent();
            cmbEmpresa_Update();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "INSERIR" E CLICADO
        private void btnInserir_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "NOME" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNomeAcao.Text, "[A-z]+"))
            {
                //DEFINE OS VALORES DAS VARIAVEIS NECESSARIAS PARA INSERIR
                Acao action = new Acao();
                action.nmAction = txtNomeAcao.Text;
                action.idEmpresa = Convert.ToInt32(cmbEmpresa.SelectedValue);
                //EXECUTA FUNÇÃO DE INSERT NO BANCO DE DADOS
                action.InserirAcao();
                //EXECUTA A FUNÇÃO DO BOTAO LIMPAR
                btnLimpar_Click(sender, e);
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
            else
            {
                MessageBox.Show("O campo 'Nome' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EXCLUIR" É CLICADO
        private void btnExcluir_Click(object sender, EventArgs e)
        {
            //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EXCLUIR
            Acao action = new Acao();
            action.idAction = Convert.ToInt32(txtID.Text);
            //EXECUTA FUNÇÃO DE DELETE NO BANCO DE DADOS
            action.ExcluirAcao();
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
                    Acao action = new Acao();
                    action.idAction = Convert.ToInt32(txtID.Text);
                    //EXECUTA FUNÇÃO DE SELECT NO BANCO DE DADOS
                    action.ConsultarAcao();
                    txtID.Text = Convert.ToString(action.idAction);
                    txtNomeAcao.Text = Convert.ToString(action.nmAction);
                    cmbEmpresa.SelectedValue = Convert.ToString(action.idEmpresa);
                    //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
                    btnEditar.Enabled = true;
                    btnInserir.Enabled = false;
                    btnExcluir.Enabled = true;
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO INCORRETA DE VALORES COMO " "
                catch
                {
                    MessageBox.Show("Verique o número de ID da ação, ou digite o valor '0' para visualizar a última ação inserida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                    btnLimpar_Click(sender, e);
                }
                txtID.ReadOnly = true;
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
            Acao action = new Acao();
            action.LimparAcao(this.Controls);
            //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
            txtID.ReadOnly = false;
            btnInserir.Enabled = true;
            btnExcluir.Enabled = false;
            btnEditar.Enabled = false;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "EDITAR" É CLICADO
        private void btnEditar_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "VALOR OPERACAO" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNomeAcao.Text, "[A-z]+"))
            {
                //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EDITAR
                Acao action = new Acao();
                action.idAction = Convert.ToInt32(txtID.Text);
                action.nmAction = txtNomeAcao.Text;
                action.idEmpresa = Convert.ToInt32(cmbEmpresa.SelectedValue);
                //EXECUTA FUNÇÃO DE UPDATE NO BANCO DE DADOS
                action.EditarAcao();
                btnLimpar_Click(sender, e);
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
            else
            {
                MessageBox.Show("O campo 'Nome' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        //REALIZADA A ATUALIZAÇÃO DO COMBOBOX "EMPRESA"
        private void cmbEmpresa_Update()
        {

            Acao action = new Acao();
            cmbEmpresa.DataSource = action.PopularComboBoxEmpresa();
            cmbEmpresa.ValueMember = "id_Empresa";
            cmbEmpresa.DisplayMember = "nm_Empresa";
            cmbEmpresa.SelectedIndex = -1;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O COMBOBOX "EMPRESA" É CLICADO
        public void cmbEmpresa_Click(object sender, EventArgs e)
        {
            cmbEmpresa_Update();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "CADASTRAR EMPRESA" É CLICADO
        private void btnCadastrarEmpresa_Click(object sender, EventArgs e)
        {
            //CRIA UM NOVO FORMULARIO "FORM_EMPRESA"
            var form = new Form_Empresa();
            //ABERTURA DO FORMULARIO "FORM_EMPRESA"
            form.ShowDialog();
        }
    }
}

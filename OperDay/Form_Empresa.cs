using System;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_Empresa : Form
    {
        public Form_Empresa()
        {
            InitializeComponent();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "INSERIR" E CLICADO
        private void btnInserir_Click(object sender, EventArgs e)
        {
            //VERIFICA SE O CAMPO "NOME" TEM UM OU MAIS ACONTECIMENTOS DE CARACTERES DE A-z ATRAVES DE UM REGEX
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNomeEmpresa.Text, "[A-z]+"))
            {
                //DEFINE OS VALORES DAS VARIAVEIS NECESSARIAS PARA INSERIR
                Empresa emp = new Empresa();
                emp.nmEmpresa = txtNomeEmpresa.Text;
                emp.cnpjEmpresa = txtCNPJ.Text.Replace(".", String.Empty);
                //EXECUTA FUNÇÃO DE INSERT NO BANCO DE DADOS
                emp.InserirEmpresa();
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
            Empresa emp = new Empresa();
            emp.idEmpresa = Convert.ToInt32(txtID.Text);
            //EXECUTA FUNÇÃO DE DELETE NO BANCO DE DADOS
            emp.ExcluirEmpresa();
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
                    Empresa emp = new Empresa();
                    emp.idEmpresa = Convert.ToInt32(txtID.Text);
                    //EXECUTA FUNÇÃO DE SELECT NO BANCO DE DADOS
                    emp.ConsultarEmpresa();
                    txtID.Text = Convert.ToString(emp.idEmpresa);
                    txtNomeEmpresa.Text = Convert.ToString(emp.nmEmpresa);
                    txtCNPJ.Text = emp.cnpjEmpresa;
                    //DESATIVA BOTÕES QUE NÃO PODERAM SER UTILIZADOS E HABILITAR BOTÃO QUE PODERAM SER UTILIZADOS
                    btnEditar.Enabled = true;
                    txtID.ReadOnly = true;
                    btnInserir.Enabled = false;
                    btnExcluir.Enabled = true;
                }
                //TRATA A EXCEÇÃO QUANTO A INSERÇÃO INCORRETA DE VALORES COMO " "
                catch
                {
                    MessageBox.Show("Verique o número de ID da empresa, ou digite o valor '0' para visualizar a última empresa inserida", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
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
            Empresa emp = new Empresa();
            emp.LimparEmpresa(this.Controls);
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
            if (System.Text.RegularExpressions.Regex.IsMatch(txtNomeEmpresa.Text, "[A-z]+"))
            {
                //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EDITAR
                Empresa emp = new Empresa();
                emp.idEmpresa = Convert.ToInt32(txtID.Text);
                emp.nmEmpresa = txtNomeEmpresa.Text;
                emp.cnpjEmpresa = txtCNPJ.Text;
                //EXECUTA FUNÇÃO DE UPDATE NO BANCO DE DADOS
                emp.EditarEmpresa();
                btnLimpar_Click(sender, e);
            }
            //TRATA A EXCEÇÃO QUANTO A INSERÇÃO DE CARACTERES DE A-z
            else
            {
                MessageBox.Show("O campo 'Nome' precisa ser um nome válido", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

    }
}

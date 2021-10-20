using System;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_GridView : Form
    {
        //DEFINE AS VARIAVEIS NECESSARIAS
        public int idConsulta { get; set; }
        public string formChamador { get; set; }

        public Form_GridView()
        {
            InitializeComponent();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O FORMULARIO E CARREGADO
        private void GridView_Load(object sender, EventArgs e)
        {
            //CADA "IF" DEFINE QUAL TIPO FUNCAO SERA EXECUTADA PARA POPULACAO DO GRIDVIEW "DATAGRIDVIEW"
            if (formChamador == "OperDay")
            {
                GridView grid = new GridView();
                dataGridView.DataSource = grid.PopularGridViewOperation();
            }
            else if (formChamador == "Conta")
            {
                GridView grid = new GridView();
                dataGridView.DataSource = grid.PopularGridViewConta();
            }
            else if (formChamador == "Ação")
            {
                GridView grid = new GridView();
                dataGridView.DataSource = grid.PopularGridViewAcao();
            }
            else if (formChamador == "Empresa")
            {
                GridView grid = new GridView();
                dataGridView.DataSource = grid.PopularGridViewEmpresa();
            }
            else
            {
                MessageBox.Show("Impossível gerar a consulta!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }

        }

        //REALIZA A VISUALIZACAO, CONVERSAO E ATRIBUICAO DO VALOR DA PRIMEIRA CECULA DA LINHA SELECIONADA PARA A VARIAVEL "IDCONSULTA"
        private void ConsultarPeloGrid()
        {
            idConsulta = Convert.ToInt32(dataGridView.CurrentRow.Cells[0].Value.ToString());
            this.Close();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO A LINHA RECEBE O EVENTO DE DOIS CLIQUES
        private void DoubleClickGridView(object sender, DataGridViewCellEventArgs e)
        {
            ConsultarPeloGrid();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO UMA LINHA ESTA SELECIONADA E O BOTAO "ENTER" E PRESSIONADO
        private void KeyPressGridView(object sender, KeyPressEventArgs e)
        {
            ConsultarPeloGrid();
        }

        //IDENTIFICADA QUAL E O "NOME" DO FORMULARIO CHAMADOR DO GRIDVIEW
        public void FormGridView(Form FormChamador)
        {
            //CONVERTE O "NOME" DO FORMULARIO PARA STRING E ATRIBUI A VARIAVEL "FORMCHAMADOR"
            formChamador = Convert.ToString(FormChamador);
            //REALIZADA A ATRIBUICAO DA SEQUENCIA DE CARACTERES PARA A VARIAVEL "FORMCHAMADOR" LOGO APOS O ULTIMO " " DO NOME DO FORMULARIO CHAMADOR
            formChamador = formChamador.Substring(formChamador.LastIndexOf(@" ") + 1);
        }
    }
}
using System;
using System.IO;
using System.Windows.Forms;

namespace OperDay
{
    public partial class Form_Relatorio : Form
    {
        public Form_Relatorio()
        {
            InitializeComponent();
        }

        //REALIZADA A ATUALIZAÇÃO DO COMBOBOX "CONTA"
        private void cmbConta_Update()
        {

            Relatorio relatorio = new Relatorio();
            cmbConta.DataSource = relatorio.PopularComboBoxContaRelat();
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

            Relatorio relatorio = new Relatorio();
            cmbAcao.DataSource = relatorio.PopularComboBoxAcaoRelat();
            cmbAcao.ValueMember = "id_Action";
            cmbAcao.DisplayMember = "nm_Action";
            cmbAcao.SelectedIndex = -1;
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O COMBOBOX "ACAO" É CLICADO
        public void cmbAcao_Click(object sender, EventArgs e)
        {
            cmbAcao_Update();
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTAO "LIMPAR" É CLICADO
        private void btnLimpar_Click(object sender, EventArgs e)
        {
            //PASSA O CONTROLES EXISTENTE NO FORMULARIO PARA A FUNCAO "LIMPAROPERACAO"
            Relatorio relatorio = new Relatorio();
            relatorio.LimparRelatorio(this.Controls);
        }

        //DEFINE A FUNÇÃO A SER EXECUTADA QUANDO O BOTÃO "EXPORTAR" É CLICADO
        private void btnExportar_Click(object sender, EventArgs e)
        {
            try
            {
                //DEFINE OS VALORES DAS VARIÁVEIS NECESSÁRIAS PARA EXPORTAR
                Relatorio relatorio = new Relatorio();
                relatorio.dateOperationInit = Convert.ToDateTime(txtDateInit.Text);
                relatorio.dateOperationFins = Convert.ToDateTime(txtDateFins.Text);
                relatorio.idConta = Convert.ToInt32(cmbConta.SelectedValue);
                relatorio.idAcao = Convert.ToInt32(cmbAcao.SelectedValue);
                //EXECUTA A FUNÇÃO PARA CONSULTA EM BANCO DE DADOS
                relatorio.ConsultarOperacoes(relatorio.idConta, relatorio.idAcao);
                //DEFINIÇÃO DE PROPRIEDADES DO SAVEFILEDIALOG
                SaveFileDialog savefiledialog = new SaveFileDialog();
                savefiledialog.InitialDirectory = "C:\\";
                savefiledialog.Filter = "Excel (*.csv) | *.csv";
                savefiledialog.FileName = "OperDay_" + DateTime.Now.ToString("ddMMyyyy") + "_" + cmbConta.Text + "_" + cmbAcao.Text;
                savefiledialog.RestoreDirectory = true;
                //EXECUTA A FUNÇÃO PARA POPULAÇÃO DE UM STRING NA CLASSE RELATORIO
                relatorio.ExportarExcel(relatorio.data);
                //ABERTURA DO SAVEFILEDIALOG COM IDENTIFICAÇÃO DAS AÇÕES "OK" E "CANCEL" DO USUARIO
                DialogResult resultado = savefiledialog.ShowDialog();
                //REALIZA A POPULAÇÃO DE UM STREAMWITER COM A STRING REFERENTE A CONSULTA NO BANCO DE DADOS - CASO O BOTÃO "SALVAR" SEJA PRESSIONADO
                if (resultado == DialogResult.OK)
                {

                    FileStream fs = new FileStream(savefiledialog.FileName, FileMode.Create);
                    StreamWriter writer = new StreamWriter(fs);
                    writer.Write(relatorio.table);
                    writer.Close();
                    MessageBox.Show("Planilha gerada com sucesso!\n\n" + savefiledialog.FileName, "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    btnLimpar_Click(sender, e);
                }
                //MOSTRA MENSAGEM CASO A OPERAÇÃO SEJA ENCERRADA DENTRO DO SAVEFILEDIALOG
                else if (resultado == DialogResult.Cancel)
                {
                    MessageBox.Show("Operação cancelada", "Cancelado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            //TRATA ERROS NO MOMENTO DO SALVAMENTO DO ARQUIVO - GERALMENTE O ARQUIVO A SER SUBSTITUIDO ESTA EM USO
            catch
            {
                MessageBox.Show("Falha ao gerar planilha, o arquivo pode estar em uso.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }
    }
}

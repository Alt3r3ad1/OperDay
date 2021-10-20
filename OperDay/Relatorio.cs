using System;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Windows.Forms;

namespace OperDay
{
    class Relatorio
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        public DateTime dateOperationInit { get; set; }
        public DateTime dateOperationFins { get; set; }
        public int idConta { get; set; }
        public int idAcao { get; set; }
        public DataTable data { get; set; }
        public string table { get; set; }

        //DEFINICAO DOS METODOS DA CLASSE

        //REALIZA A CONSULTA DAS OPERACOES NO BANCO DE DADOS
        public void ConsultarOperacoes(int idConta, int idAcao)
        {
            //CADA "IF" REPRESENTA AS INFORMACOES QUE FORAM DISPOSTAS NO FORMULARIO "FORM_RELATORIO" A FIM DE REALIZAR O FILTRO NA OBTENCAO DOS RESULTADOS
            if (idConta != 0 && idAcao != 0)
            {
                string command = "sp_GerarRelatorioOperacao" +
                " @dateOperationInit " +
                ", @dateOperationFins " +
                ", @idConta " +
                ", @idAcao ";
                SqlCommand sql = new SqlCommand();
                sql.Parameters.AddWithValue("@dateOperationInit", dateOperationInit);
                sql.Parameters.AddWithValue("@dateOperationFins", dateOperationFins);
                sql.Parameters.AddWithValue("@idConta", idConta);
                sql.Parameters.AddWithValue("@idAcao", idAcao);
                sql.CommandText = command;
                Conexao con = new Conexao();
                con.ExecuteCommand("conn", sql);
                if (con.CompleteCommand == false)
                {
                    MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    data = con.DataSet();
                }
            }
            else if (idConta != 0)
            {
                string command = "sp_GerarRelatorioOperacaoConta" +
                " @dateOperationInit " +
                ", @dateOperationFins " +
                ", @idConta ";
                SqlCommand sql = new SqlCommand();
                sql.Parameters.AddWithValue("@dateOperationInit", dateOperationInit);
                sql.Parameters.AddWithValue("@dateOperationFins", dateOperationFins);
                sql.Parameters.AddWithValue("@idConta", idConta);
                sql.CommandText = command;
                Conexao con = new Conexao();
                con.ExecuteCommand("conn", sql);
                if (con.CompleteCommand == false)
                {
                    MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    data = con.DataSet();
                }


            }
            else if (idAcao != 0)
            {
                string command = "sp_GerarRelatorioOperacaoAcao" +
                " @dateOperationInit " +
                ", @dateOperationFins " +
                ", @idAcao ";
                SqlCommand sql = new SqlCommand();
                sql.Parameters.AddWithValue("@dateOperationInit", dateOperationInit);
                sql.Parameters.AddWithValue("@dateOperationFins", dateOperationFins);
                sql.Parameters.AddWithValue("@idAcao", idAcao);
                sql.CommandText = command;
                Conexao con = new Conexao();
                con.ExecuteCommand("conn", sql);
                if (con.CompleteCommand == false)
                {
                    MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    data = con.DataSet();
                }
            }
            else if (idConta == 0 && idAcao == 0)
            {
                string command = "sp_GerarRelatorioOperacaoAll" +
                " @dateOperationInit " +
                ", @dateOperationFins ";
                SqlCommand sql = new SqlCommand();
                sql.Parameters.AddWithValue("@dateOperationInit", dateOperationInit);
                sql.Parameters.AddWithValue("@dateOperationFins", dateOperationFins);
                sql.CommandText = command;
                Conexao con = new Conexao();
                con.ExecuteCommand("conn", sql);
                if (con.CompleteCommand == false)
                {
                    MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
                }
                else
                {
                    data = con.DataSet();
                }
            }
            //TRATATIVA CASO OCORRA A SOBRA DE ALGUM LIXO NOS COMBOBOX'S
            else
            {
                MessageBox.Show("Falha ao consultar dados, verifique as informações inseridas.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O COMBOBOX "CONTA"
        public DataTable PopularComboBoxContaRelat()
        {
            string command = "SELECT id_Conta, corr_Conta FROM Conta ORDER BY corr_Conta";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            data = con.DataSet();
            return data;
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O COMBOBOX "ACAO"
        public DataTable PopularComboBoxAcaoRelat()
        {
            string command = "SELECT id_Action, nm_Action FROM Acao ORDER BY nm_Action";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            data = con.DataSet();
            return data;
        }

        //REALIZA A GERACAO DE UMA STRING ONDE AS INFORMACOES DISPOSTAS NA VARIAVEL "DATATABLE" SERAO CONVERTIDAS PARA EXPORTACAO POSTERIORMENTE
        public void ExportarExcel(DataTable datatable)
        {
            StringBuilder sb = new StringBuilder();

            for (int contadorColuna = 0;
                 contadorColuna < datatable.Columns.Count; contadorColuna++)
            {
                sb.Append(datatable.Columns[contadorColuna].ColumnName);
                if (contadorColuna != datatable.Columns.Count - 1)
                {
                    sb.Append(",");
                }
                else
                {
                    sb.AppendLine();
                }
            }
            for (int contadorLinha = 0;
                 contadorLinha < datatable.Rows.Count; contadorLinha++)
            {
                for (int contadorColuna = 0;
                     contadorColuna < datatable.Columns.Count; contadorColuna++)
                {
                    if (contadorColuna == 7)
                    {
                        if (contadorLinha == (datatable.Rows.Count - 1))
                        {
                            sb.Append(datatable.Rows[contadorLinha][contadorColuna].ToString().Replace(",", "."));
                        }
                    }
                    else
                    {
                        sb.Append(datatable.Rows[contadorLinha][contadorColuna].ToString().Replace(",", "."));
                    }
                    if (contadorColuna != datatable.Columns.Count - 1)
                    {
                        sb.Append(",");
                    }

                }
                if (contadorLinha != datatable.Rows.Count - 1)
                {
                    sb.AppendLine();
                }
            }
            table = sb.ToString();
        }

        //REALIZA A LIMPEZA DOS DADOS EXISTENTES NO FORMULARIO "FORM_RELATORIO"
        public void LimparRelatorio(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is ComboBox)
                {
                    ((ComboBox)(ctrl)).SelectedIndex = -1;
                }
                if (ctrl is DateTimePicker)
                {
                    ((DateTimePicker)(ctrl)).Value = DateTime.Now;
                }
            }
        }

    }
}



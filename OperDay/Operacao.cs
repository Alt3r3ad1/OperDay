using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OperDay
{

    class Operacao
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        public int idOperation { get; set; }
        public DateTime dateOperation { get; set; }
        public double vlrOperation { get; set; }
        public int idConta { get; set; }
        public int idAcao { get; set; }
        public int tpOperation { get; set; }
        public DataTable data { get; set; }

        //DEFINICAO DOS METODOS DA CLASSE

        //REALIZA A INSERÇÃO DOS DADOS DE UMA OPERACAO NO BANCO DE DADOS
        public void InserirOperacao()
        {
            string command = "sp_InserirOperacao" +
            " @dateOperation " +
            ", @vlrOperation " +
            ", @idConta " +
            ", @idAcao " +
            ", @tpOperation";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@dateOperation", dateOperation);
            sql.Parameters.AddWithValue("@vlrOperation", vlrOperation);
            sql.Parameters.AddWithValue("@idConta", idConta);
            sql.Parameters.AddWithValue("@idAcao", idAcao);
            sql.Parameters.AddWithValue("@tpOperation", tpOperation);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == false)
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else
            {
                MessageBox.Show("Inserção realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //REALIZA A EXCLUSÃO DOS DADOS DE UMA OPERACAO NO BANCO DE DADOS
        public void ExcluirOperacao()
        {
            string command = "sp_ExcluirOperacao" +
            " @idOperation";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idOperation", idOperation);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == false)
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else
            {
                MessageBox.Show("Exclusão realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A CONSULTA DOS DADOS DE UMA OPERACAO NO BANCO DE DADOS
        public void ConsultarOperacao()
        {
            string command = "sp_ConsultarOperacao" +
            " @idOperation";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idOperation", idOperation);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == true)
            {
                while (con.RecordSet.Read())
                {
                    idOperation = Convert.ToInt32(con.RecordSet[0]);
                    dateOperation = Convert.ToDateTime(con.RecordSet[1]);
                    vlrOperation = Convert.ToDouble(con.RecordSet[2]);
                    idConta = Convert.ToInt32(con.RecordSet[3]);
                    idAcao = Convert.ToInt32(con.RecordSet[4]);
                    tpOperation = Convert.ToInt32(con.RecordSet[5]);
                }
            }
            else
            {
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A EDICAO DOS DADOS DE UMA OPERACAO NO BANCO DE DADOS
        public void EditarOperacao()
        {
            string command = "sp_EditarOperacao" +
            " @idOperation " +
            ", @dateOperation " +
            ", @vlrOperation " +
            ", @idConta " +
            ", @idAcao" +
            ", @tpOperation";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idOperation", idOperation);
            sql.Parameters.AddWithValue("@dateOperation", dateOperation);
            sql.Parameters.AddWithValue("@vlrOperation", vlrOperation);
            sql.Parameters.AddWithValue("@idConta", idConta);
            sql.Parameters.AddWithValue("@idAcao", idAcao);
            sql.Parameters.AddWithValue("@tpOperation", tpOperation);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == false)
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else
            {
                MessageBox.Show("Edição realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //REALIZA A LIMPEZA DOS DADOS EXISTENTES NO FORMULARIO "FORM_OPERDAY"
        public void LimparOperacao(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = String.Empty;
                }
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

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O COMBOBOX "CONTA"
        public DataTable PopularComboBoxContaOper()
        {
            string command = "SELECT id_Conta, corr_Conta FROM Conta WHERE sts_Conta = 0 ORDER BY corr_Conta";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            data = con.DataSet();
            return data;
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O COMBOBOX "ACAO"
        public DataTable PopularComboBoxAcaoOper()
        {
            string command = "SELECT id_Action, nm_Action FROM Acao ORDER BY nm_Action";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            data = con.DataSet();
            return data;
        }
    }
}

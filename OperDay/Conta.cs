using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OperDay
{
    class Conta
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        public int idConta { get; set; }
        public string corrConta { get; set; }
        public string stsConta { get; set; }
        public string atvConta { get; set; }

        //DEFINICAO DOS METODOS DA CLASSE

        //REALIZA A INSERÇÃO DOS DADOS DE UMA CONTA NO BANCO DE DADOS
        public void InserirConta()
        {
            string command = "sp_InserirConta" +
            " @corrConta " +
            ", @stsConta " +
            ", @atvConta ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@corrConta", corrConta);
            sql.Parameters.AddWithValue("@stsConta", stsConta);
            sql.Parameters.AddWithValue("@atvConta", atvConta);
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

        //REALIZA A EXCLUSAO DOS DADOS DE UMA CONTA NO BANCO DE DADOS
        public void ExcluirConta()
        {
            string command = "sp_ExcluirConta" +
            " @idConta ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idConta", idConta);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == false)
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            else
            {
                MessageBox.Show("Exclusão realizada com sucesso!", "Sucesso", MessageBoxButtons.OK, MessageBoxIcon.Information);

            }
        }

        //REALIZA A CONSULTA DOS DADOS DE UMA CONTA NO BANCO DE DADOS
        public void ConsultarConta()
        {
            string command = "sp_ConsultarConta" +
            " @idConta";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idConta", idConta);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == true)
            {
                while (con.RecordSet.Read())
                {
                    idConta = Convert.ToInt32(con.RecordSet[0]);
                    corrConta = Convert.ToString(con.RecordSet[1]);
                    stsConta = Convert.ToString(con.RecordSet[2]);
                    atvConta = Convert.ToString(con.RecordSet[3]);
                }
            }
            else
            {
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A EDICAO DOS DADOS DE UMA CONTA NO BANCO DE DADOS
        public void EditarConta()
        {
            string command = "sp_EditarConta" +
            " @idconta " +
            ", @corrConta " +
            ", @stsConta " +
            ", @atvConta ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idConta", idConta);
            sql.Parameters.AddWithValue("@corrConta", corrConta);
            sql.Parameters.AddWithValue("@stsConta", stsConta);
            sql.Parameters.AddWithValue("@atvConta", atvConta);
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

        //REALIZA A LIMPEZA DOS DADOS EXISTENTES NO FORMULARIO "FORM_CONTA"
        public void LimparConta(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = String.Empty;
                }
            }
        }

    }
}

using System;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OperDay
{
    class Empresa
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        public int idEmpresa { get; set; }
        public string nmEmpresa { get; set; }
        public string cnpjEmpresa { get; set; }

        //DEFINICAO DOS METODOS DA CLASSE

        //REALIZA A INSERCAO DOS DADOS DE UMA EMPRESA NO BANCO DE DADOS
        public void InserirEmpresa()
        {
            string command = "sp_InserirEmpresa" +
            " @nmEmpresa " +
            ", @cnpjEmpresa ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@nmEmpresa", nmEmpresa);
            sql.Parameters.AddWithValue("@cnpjEmpresa", cnpjEmpresa);
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

        //REALIZA A EXCLUSAO DOS DADOS DE UMA EMPRESA NO BANCO DE DADOS
        public void ExcluirEmpresa()
        {
            string command = "sp_ExcluirEmpresa" +
            " @idEmpresa ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idEmpresa", idEmpresa);
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

        //REALIZA A CONSULTA DOS DADOS DE UMA EMPRESA NO BANCO DE DADOS
        public void ConsultarEmpresa()
        {
            string command = "sp_ConsultarEmpresa" +
            " @idEmpresa";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idEmpresa", idEmpresa);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == true)
            {
                while (con.RecordSet.Read())
                {
                    idEmpresa = Convert.ToInt32(con.RecordSet[0]);
                    nmEmpresa = Convert.ToString(con.RecordSet[1]);
                    cnpjEmpresa = Convert.ToString(con.RecordSet[2]);
                }
            }
            else
            {
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A EDICAO DOS DADOS DE UMA EMPRESA NO BANCO DE DADOS
        public void EditarEmpresa()
        {
            string command = "sp_EditarEmpresa" +
            " @idEmpresa " +
            ", @nmEmpresa " +
            ", @cnpjEmpresa ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idEmpresa", idEmpresa);
            sql.Parameters.AddWithValue("@nmEmpresa", nmEmpresa);
            sql.Parameters.AddWithValue("@cnpjEmpresa", cnpjEmpresa);
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

        //REALIZA A LIMPEZA DOS DADOS EXISTENTES NO FORMULARIO "FORM_EMPRESA"
        public void LimparEmpresa(Control.ControlCollection controles)
        {
            //Faz um laço para todos os controles passados no parâmetro
            foreach (Control ctrl in controles)
            {
                //Se o contorle for um TextBox...
                if (ctrl is TextBox)
                {
                    ((TextBox)(ctrl)).Text = String.Empty;
                }
                if (ctrl is MaskedTextBox)
                {
                    ((MaskedTextBox)(ctrl)).Text = String.Empty;
                }
            }
        }

        

    }
}

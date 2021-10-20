using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace OperDay
{
    class Acao
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        public int idAction { get; set; }
        public string nmAction { get; set; }
        public int idEmpresa { get; set; }
        public DataTable data { get; set; }

        //DEFINICAO DOS METODOS DA CLASSE

        //REALIZA A INSERÇÃO DOS DADOS DE UMA ACAO NO BANCO DE DADOS
        public void InserirAcao()
        {
            string command = "sp_InserirAcao" +
            " @nmAction " +
            ", @idEmpresa ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@nmAction", nmAction);
            sql.Parameters.AddWithValue("@idEmpresa", idEmpresa);
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

        //REALIZA A EXCLUSAO DOS DADOS DE UMA ACAO NO BANCO DE DADOS
        public void ExcluirAcao()
        {
            string command = "sp_ExcluirAcao" +
            " @idAction ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idAction", idAction);
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

        //REALIZA A CONSULTA DOS DADOS DE UMA ACAO NO BANCO DE DADOS
        public void ConsultarAcao()
        {
            string command = "sp_ConsultarAcao" +
            " @idAction";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idAction", idAction);
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            if (con.CompleteCommand == true)
            {
                while (con.RecordSet.Read())
                {
                    idAction = Convert.ToInt32(con.RecordSet[0]);
                    nmAction = Convert.ToString(con.RecordSet[1]);
                    idEmpresa = Convert.ToInt32(con.RecordSet[2]);
                }
            }
            else
            {
                MessageBox.Show(con.ErrorDescription, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error, MessageBoxDefaultButton.Button1);
            }
        }

        //REALIZA A EDICAO DOS DADOS DE UMA ACAO NO BANCO DE DADOS
        public void EditarAcao()
        {
            string command = "sp_EditarAcao" +
            " @idAction " +
            ", @nmAction " +
            ", @idEmpresa ";
            SqlCommand sql = new SqlCommand();
            sql.Parameters.AddWithValue("@idAction", idAction);
            sql.Parameters.AddWithValue("@nmAction", nmAction);
            sql.Parameters.AddWithValue("@idEmpresa", idEmpresa);
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

        //REALIZA A LIMPEZA DOS DADOS EXISTENTES NO FORMULARIO "FORM_ACAO"
        public void LimparAcao(Control.ControlCollection controles)
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
            }
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O COMBOBOX "EMPRESA"
        public DataTable PopularComboBoxEmpresa()
        {
            string command = "SELECT id_Empresa, nm_Empresa FROM Empresa ORDER BY nm_Empresa";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            data = con.DataSet();
            return data;
        }
    }
}

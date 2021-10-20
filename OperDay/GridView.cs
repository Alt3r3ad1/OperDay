using System.Data;
using System.Data.SqlClient;

namespace OperDay
{
    class GridView
    {
        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O GRIDVIEW DO FORMULARIO "OPERDAY"
        public DataTable PopularGridViewOperation()
        {

            string command = "EXEC sp_GridOperacao";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            return con.DataSet();
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O GRIDVIEW DO FORMULARIO "CONTA"
        public DataTable PopularGridViewConta()
        {

            string command = "EXEC sp_GridConta";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            return con.DataSet();
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O GRIDVIEW DO FORMULARIO "ACAO"
        public DataTable PopularGridViewAcao()
        {
            string command = "EXEC sp_GridAcao";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            return con.DataSet();
        }

        //REALIZA A CONSULTA NO BANCO DE DADOS PARA POPULAR O GRIDVIEW DO FORMULARIO "EMPRESA"
        public DataTable PopularGridViewEmpresa()
        {
            string command = "EXEC sp_GridEmpresa";
            SqlCommand sql = new SqlCommand();
            sql.CommandText = command;
            Conexao con = new Conexao();
            con.ExecuteCommand("conn", sql);
            return con.DataSet();
        }
    }
}

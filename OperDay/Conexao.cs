using System;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace OperDay
{
    class Conexao
    {
        //DEFINICAO DOS ATRIBUTOS DA CLASSE
        private bool _State;                // Indica ULTIMO Status da conexão
        private string _ErrorMessage;      // Indica ULTIMO Mensagem de Erro da conexao
        private int _ErrorNumber;           // Indica ULTIMO Numero de Erro da conexao
        private bool _CompleteCommand;   // Indica ULTIMO comando foi executado com sucesso
        private SqlDataReader _rsData;      // Usado para retornar colecao de registros

        //DEFINICAO DOS METODOS DA CLASSE

        public void ExecuteCommand(string myConnectionName, SqlCommand myQueryString)
        {
            // Metodo responsavel por executar uma instrução SQL
            // Recebe como parametro o nome de uma chave do arquivo web.config que contem string de conexão e
            // a instrução SQL a ser executada pode ser um SELECT ou uma procedure (preferencialmente)
            // Utililizo Tray..Catch para tratamento de erro
            try
            {
                // Definir valores padrões das variaveis
                _ErrorMessage = "";
                _State = false;

                // Se nao for passado nenhuma chave de conexão do arquivo web.config
                // vou setar uma chave padrao de conexão do arquivo Web.Config
                if (myConnectionName.Length == 0)
                { // Aqui estou indicando o nome da chave que contem a string de conexão no arquivo web.config
                    myConnectionName = "conn";
                }

                string myConnectionString = ConfigurationManager.ConnectionStrings[myConnectionName].ConnectionString.ToString();

                // Se não for informado comando T-SQL retorno error
                if (myQueryString != null) // Se realmente foi passado um comando a ser executado
                {
                    // Inicio uma conexão com o banco de dados
                    SqlConnection myConnection = new SqlConnection(myConnectionString);
                    // Abro a conexão
                    myConnection.Open();

                    myQueryString.Connection = myConnection;

                    // Executo um comando com ExecuteReader, pois este retorna dados a um SqlDataReader
                    _rsData = myQueryString.ExecuteReader();
                    //STATUS DA OPERACAO
                    _State = true;
                    if (_rsData == null)
                    {
                        _CompleteCommand = false;
                    }
                    else
                    {
                        _CompleteCommand = true;
                    }
                }
            }
            catch (Exception ex)
            {
                // Em caso de erro seto as variaveis abaixo
                _ErrorMessage = ex.Message.ToString();
                _State = false;
                _ErrorNumber = ex.GetHashCode();
            }
        }

        public SqlDataReader RecordSet
        {
            // Metodo para ler os registros
            get { return _rsData; }
        }
        public int ErrorNumber
        {
            // Metodo para ler ultimo código de error
            get { return _ErrorNumber; }
        }
        public string ErrorDescription
        {
            // Metodo para ler ultima descrição de error
            get { return _ErrorMessage; }
        }
        public bool ConnectionState
        {
            // Metodo para ler ultimo status da conexao
            get { return _State; }
        }
        public bool CompleteCommand
        {
            // Metodo para ler status do ultimo comando executado.
            get { return _CompleteCommand; }
        }

        //METODO PARA TRANSFORMAR UMA VARIAVEL "SQLDATAREADER" EM "DATATABLE"
        public DataTable DataSet()
        {
            DataTable data = new DataTable();
            data.Load(_rsData);
            return data;
        }
    }
}

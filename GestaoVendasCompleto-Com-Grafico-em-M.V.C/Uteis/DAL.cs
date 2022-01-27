using System;
// Add
using System.Data;
using System.Data.SqlClient;

namespace SistemaVendas.Uteis
{
    // Data Access Layer (DAL - Camada de Acesso à Dados) //
    public class DAL
    {
        private static readonly String Server = "HOME"; // Servidor SQL...
        private static readonly String Database = "sistema_vendas"; // Banco de Dados no SQL...
        private static readonly String User = "sa"; // Usuário SQL...
        private static readonly String Password = "Paradoxo22"; // Senha SQL...

        // String de Conexão SQL
        private static readonly String Connectionstring = $"Server={Server};Database={Database};Uid={User};Pwd={Password};";

        private static SqlConnection Connection;

        //Criação do Construtor 
        public DAL()
        {
            Connection = new SqlConnection(Connectionstring);
            Connection.Open();
        }

        // Espera um parâmetro do tipo string
        // contendo um conteudo SQL do tipo SELECT
        public DataTable RetDatatable(String sql)
        {
            DataTable data = new DataTable();
            SqlCommand Command = new SqlCommand(sql, Connection);
            SqlDataAdapter da = new SqlDataAdapter(Command);
            da.Fill(data);
            return data;
        }

        // Polimorfismo para evitar problemas com Racker na Injeção de Dependência.
        public DataTable RetDatatable(SqlCommand Command)
        {
            DataTable data = new DataTable();
            Command.Connection = Connection;
            SqlDataAdapter da = new SqlDataAdapter(Command);
            da.Fill(data);
            return data;
        }

        // Método que permite executar comandos de CRUD
        public void ExecutarComandoSql(String sql)
        {
            SqlCommand Command = new SqlCommand(sql, Connection);
            Command.ExecuteNonQuery();
        }
    }
}
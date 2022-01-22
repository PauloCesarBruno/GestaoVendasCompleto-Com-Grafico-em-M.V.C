using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Importaçãoes
using SistemaVendas.Uteis;
using System.Data;
using System.ComponentModel.DataAnnotations;

namespace SistemaVendas.Models
{
    #region Propriedades

    public class ClienteModel
    {
        public string  Id { get; set; }

        [Required (ErrorMessage = "Informe o Nome do Cliente")]
        public string Nome { get; set; }

        [Required(ErrorMessage = "Informe o CPF ou CNPJ do Cliente")]
        public string CPF_CNPJ { get; set; }

        [Required(ErrorMessage = "Informe o E-MAIL do Cliente")]
        [DataType(DataType.EmailAddress)] // Para Validar um Formato Válido de Email.
        [EmailAddress(ErrorMessage = "O Email informado não é Valido !!!")]
        public string Email { get; set; }

        public string Senha { get; set; }

        #endregion

        #region Metodos

        // Método para carregar 
        public List<ClienteModel> ListarTodosClientes()
        {
            List<ClienteModel> lista = new List<ClienteModel>();
            ClienteModel item;
            DAL objDal = new DAL();
            string sql = "SELECT id, nome, cpf_cnpj, email, senha FROM cliente Order by nome asc";
            DataTable dt = objDal.RetDatatable(sql);

            for (int i = 0; i < dt.Rows.Count ; i++)
            {
                item = new ClienteModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    CPF_CNPJ = dt.Rows[i]["cpf_cnpj"].ToString(),
                    Email = dt.Rows[i]["email"].ToString(),
                    Senha = dt.Rows[i]["senha"].ToString()
                };
                lista.Add(item);
            }

            return lista;
        }

        // Método para carregar as informações para Edição
        public ClienteModel RetornarCliente(int? Id)
        {           
            ClienteModel item;
            DAL objDal = new DAL();
            string sql = $"SELECT id, nome, cpf_cnpj, email, senha FROM cliente where Id = '{Id}' Order by nome asc";
            DataTable dt = objDal.RetDatatable(sql);
            item = new ClienteModel
               {
                   Id = dt.Rows[0]["Id"].ToString(),
                   Nome = dt.Rows[0]["nome"].ToString(),
                   CPF_CNPJ = dt.Rows[0]["cpf_cnpj"].ToString(),
                   Email = dt.Rows[0]["email"].ToString(),
                   Senha = dt.Rows[0]["senha"].ToString()
               };            

            return item;
        }

        // Método para Inserir ou Alterar (INSERT OU UPDATE)
        public void Gravar()
        {
            try
            {
                DAL objDAL = new DAL();
                string sql = string.Empty;

                if (Id != null)
                {
                    //Abaixo Usando "Interpolação de String" - Novo recurso do C#.
                    sql = $"UPDATE CLIENTE SET nome ='{Nome}', cpf_cnpj ='{CPF_CNPJ}', Email = '{Email}' where Id = '{Id}' ";
                }
                else
                {
                    //Abaixo Usando "Interpolação de String" - Novo recurso do C#.
                    sql = $"INSERT INTO CLIENTE (nome, cpf_cnpj, email, senha) VALUES('{Nome}','{CPF_CNPJ}','{Email}','123456')";
                }
                objDAL.ExecutarComandoSql(sql);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Método para Excluir Registro
        public void Excluir(int Id)
        {
            DAL objDAL = new DAL();
            string sql = $"DELETE FROM CLIENTE WHERE ID = '{Id}'";
            objDAL.ExecutarComandoSql(sql);
        }
    }
}
#endregion
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
// Importar System.data para usar o datatable
using System.Data;
// Importado para instanciar a Classe DAL.
using SistemaVendas.Uteis;
// Importar Using Abaixo para validar com [REQUIRED] por exemplo
using System.ComponentModel.DataAnnotations;
using System.Data.SqlClient;

namespace SistemaVendas.Models
{
    public class LoginModel
    {
        public String Id { get; set; }
        public String Nome { get; set; }


        // Validações e mensagens para o Campo EMAIL.
        [Required(ErrorMessage = "Informe o Email do Usuário !!!")]
        [DataType(DataType.EmailAddress)] // Para Validar um Formato Válido de Email.
        [EmailAddress(ErrorMessage = "O Email informado não é Valido !!!")]
        public String Email { get; set; }


        // Validações e mensagens para o Campo SENHA.
        [Required(ErrorMessage = "Informe a Senha do Usuário !!!")]
        public String Senha { get; set; }

        public Boolean ValidarLogin()
        {
            String sql = "SELECT ID, NOME FROM VENDEDOR WHERE EMAIL = @email AND SENHA = @senha";
            SqlCommand Command = new SqlCommand();
            Command.CommandText = sql;
            Command.Parameters.AddWithValue("@email", Email);
            Command.Parameters.AddWithValue("@senha", Senha);
            //instancar Classe DAL
            DAL objDAL = new DAL();

            DataTable dt = objDAL.RetDatatable(Command);
            if (dt.Rows.Count == 1)
            {
                Id = dt.Rows[0]["ID"].ToString();
                Nome = dt.Rows[0]["NOME"].ToString();
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
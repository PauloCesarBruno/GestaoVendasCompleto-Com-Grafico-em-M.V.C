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

    public class ProdutoModel
    {
        public string  Id { get; set; }
               
        public string Nome { get; set; }
                
        public string Descricao { get; set; }
                    
        [Range(1, 99999.99)] // Valor Mínimo ao Máximo
        [DataType(DataType.Currency)]
        [Display(Name = "Preço Unitário")]
        public decimal? Preco_Unitario { get; set; } // (?) Permite nulo para tratamento
                        
        public string Quantidade_Estoque { get; set; } // (?) Permite nulo para tratamento
                      
        public string  Unidade_Medida { get; set; }
                 
        public string Link_Foto { get; set; }

        #endregion

        #region Metodos

         // =====================================================================================
        // Rotina para filtrar produto pelo Código
        public List<ProdutoModel> ListagemDeProdutos(int Cod)
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            DAL objDal = new DAL();
            string sql = $"SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM Produto Where Id = '{Cod}' Order by nome asc";
            DataTable dt = objDal.RetDatatable(sql);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["Descricao"].ToString(),
                    Preco_Unitario = decimal.Parse(dt.Rows[i]["preco_unitario"].ToString()),
                    Quantidade_Estoque = dt.Rows[i]["quantidade_estoque"].ToString(),
                    Unidade_Medida = dt.Rows[i]["unidade_medida"].ToString(),
                    Link_Foto = dt.Rows[i]["link_foto"].ToString()

                };
                lista.Add(item);
            }

            return lista;
        }
        // Fim da Rotina de filtrar produto por Código
        // =====================================================================================


        // Método para carregar 
        public List<ProdutoModel> ListarTodosProdutos()
        {
            List<ProdutoModel> lista = new List<ProdutoModel>();
            ProdutoModel item;
            DAL objDal = new DAL();
            string sql = "SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM Produto Order by nome asc";
            DataTable dt = objDal.RetDatatable(sql);

            for (int i = 0; i < dt.Rows.Count ; i++)
            {
                item = new ProdutoModel
                {
                    Id = dt.Rows[i]["Id"].ToString(),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Descricao = dt.Rows[i]["Descricao"].ToString(),
                    Preco_Unitario = decimal.Parse (dt.Rows[i]["preco_unitario"].ToString()),
                    Quantidade_Estoque = dt.Rows[i]["quantidade_estoque"].ToString(),
                    Unidade_Medida = dt.Rows[i]["unidade_medida"].ToString(),
                    Link_Foto = dt.Rows[i]["link_foto"].ToString()

                };
                lista.Add(item);
            }

            return lista;
        }

        // Método para carregar as informações para Edição
        public ProdutoModel RetornarProduto(int? Id)
        {           
            ProdutoModel item;
            DAL objDal = new DAL();
            string sql = $"SELECT id, nome, descricao, preco_unitario, quantidade_estoque, unidade_medida, link_foto FROM Produto where Id = '{Id}' Order by nome asc";
            DataTable dt = objDal.RetDatatable(sql);
            item = new ProdutoModel
               {
                   Id = dt.Rows[0]["Id"].ToString(),
                   Nome = dt.Rows[0]["nome"].ToString(),
                   Descricao = dt.Rows[0]["Descricao"].ToString(),
                   Preco_Unitario = decimal.Parse(dt.Rows[0]["preco_unitario"].ToString()),
                   Quantidade_Estoque = dt.Rows[0]["quantidade_estoque"].ToString(),
                   Unidade_Medida = dt.Rows[0]["unidade_medida"].ToString(),
                   Link_Foto = dt.Rows[0]["link_foto"].ToString()
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
                    sql = $"UPDATE PRODUTO SET " +
                    $"NOME='{Nome}', " +
                    $"DESCRICAO='{Descricao}', " +
                    $"preco_unitario={Preco_Unitario.ToString ().Replace(",",".")}, " + // Replace-tratamento Decimal
                    $"quantidade_estoque='{Quantidade_Estoque}', " +
                    $"unidade_medida='{Unidade_Medida}', " +
                    $"link_foto='{Link_Foto}' " +
                    $"where Id = '{Id}'";
                }
                else
                {
                    //Abaixo Usando "Interpolação de String" - Novo recurso do C#.
                    sql = "INSERT INTO PRODUTO (nome,descricao,preco_unitario,quantidade_estoque,unidade_medida,link_foto ) " +
                    $"VALUES('{Nome}', " +
                    $"'{Descricao}', " +
                    $"'{Preco_Unitario.ToString().Replace(",", ".")}', " +
                    $"'{Quantidade_Estoque}', " +
                    $"'{Unidade_Medida}', " +
                    $"'{Link_Foto}')";
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
            string sql = $"DELETE FROM PRODUTO WHERE ID = '{Id}'";
            objDAL.ExecutarComandoSql(sql);
        }
    }
}
#endregion
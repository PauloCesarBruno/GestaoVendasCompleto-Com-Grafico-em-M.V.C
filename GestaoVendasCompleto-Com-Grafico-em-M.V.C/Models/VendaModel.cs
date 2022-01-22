using Newtonsoft.Json; // Importado
using SistemaVendas.Uteis; // Importado
using System;
using System.Collections.Generic;
using System.Data; // Importado
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class VendaModel
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Cliente_Id { get; set; }
        public string Vendedor_Id { get; set; }
        public double Total { get; set; }
        public string ListaProdutos { get; set; }

       
        // Para atender o Filtro do Relatório
        public List<VendaModel> ListagemVendas(string DataDe, string DataAte)
        {
            return RetornarListagemVendas(DataDe, DataAte);
        }
        
        // Listagem Geral
        public List <VendaModel> ListagemVendas()
        {
            return RetornarListagemVendas("1900/01/01", "2200/01/01");
        }

        private List<VendaModel> RetornarListagemVendas(string  DataDe, string DataAte)
        {
            List<VendaModel> lista = new List<VendaModel>();
            VendaModel item;
            DAL objDal = new DAL();
            string sql = "select v1.id, v1.data, v1.total, v2.nome as vendedor, c.nome as cliente from " +
                         "venda v1 inner Join vendedor v2 on v1.vendedor_id = v2.id inner join cliente c " +
                         "on v1.cliente_id = c.id " +
                         $"Where v1.data >='{DataDe}' and v1.data <= '{DataAte}'" +
                         "order by data, total";
            DataTable dt = objDal.RetDatatable(sql);
           

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new VendaModel
                {
                    Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                    Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                    Total = double.Parse(dt.Rows[i]["total"].ToString()),
                    Cliente_Id = dt.Rows[i]["cliente"].ToString(),
                    Vendedor_Id = dt.Rows[i]["vendedor"].ToString()

                };
                lista.Add(item);
            }
            return lista;
        }

        public List<ClienteModel> RetornarListaClientes()
        {
            return new ClienteModel().ListarTodosClientes();
        }

        public List<VendedorModel> RetornarListaVendedores()
        {
            return new VendedorModel().ListarTodosVendedores();
        }

        public List<ProdutoModel> RetornarListaProdutos()
        {
            return new ProdutoModel().ListarTodosProdutos();
        }

        public void Inserir()
        {
            DAL objDAL = new DAL();

            string dataVenda = DateTime.Now.Date.ToString("yyyy/MM/dd");

            string sql = "INSERT INTO VENDA(data, total, vendedor_id, cliente_id) " +
                $"VALUES('{dataVenda}', {Total.ToString().Replace(",", ".")},{Vendedor_Id},{Cliente_Id})";
                objDAL.ExecutarComandoSql(sql);

            // Recuperar o ID da Venda
            sql = $"select id from venda where data='{dataVenda}' and vendedor_id={Vendedor_Id} and cliente_id={Cliente_Id} order by id desc";
            DataTable dt = objDAL.RetDatatable(sql);
            string id_venda = dt.Rows[0]["id"].ToString();

            // Deserializar JSON da lista de produtos selecionados e gravá-los na tabela itens_venda.
            List<ItemVendaModel> lista_produtos = JsonConvert.DeserializeObject<List<ItemVendaModel>>(ListaProdutos);
            for (int i = 0; i < lista_produtos.Count; i++)
            {
                sql = "insert into itens_venda(venda_id, produto_id, qtde_produto, preco_produto) " +
                    $"values({id_venda},{lista_produtos[i].CodigoProduto.ToString()}," +
                    $"{lista_produtos[i].QtdeProduto.ToString()}," +
                    $"{lista_produtos[i].PrecoUnitario.ToString().Replace(",", ".")})";
                objDAL.ExecutarComandoSql(sql);

                // Baixar Produto no Estoque
                sql = " update produto " +
                    $" set quantidade_estoque = (quantidade_estoque - " + int.Parse(lista_produtos[i].QtdeProduto.ToString()) + ") " +
                    $" where id = {lista_produtos[i].CodigoProduto.ToString()}";
                objDAL.ExecutarComandoSql(sql);
            }
        }
    }
}

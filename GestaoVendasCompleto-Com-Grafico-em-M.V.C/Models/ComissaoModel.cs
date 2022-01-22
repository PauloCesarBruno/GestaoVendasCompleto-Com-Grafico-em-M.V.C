using SistemaVendas.Uteis; // Importado
using System;
using System.Collections.Generic;
using System.Data; // Importado
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models
{
    public class ComissaoModel
    {
        public int Id { get; set; }
        public string Data { get; set; }
        public string Nome { get; set; }
        public double Valor { get; set; }

        public decimal Comissao { get; set; }
                

        // Para atender o Filtro do Relatório
        public List<ComissaoModel> ListagemComissao(string DataDe, string DataAte)
        {
            return RetornarListagemComissao(DataDe, DataAte);
        }

        // Listagem Geral
        public List<ComissaoModel> ListagemComissao()
        {
            return RetornarListagemComissao("1900/01/01", "2200/01/01");
        }

        //COMISSÃO DE VENDEDORES
        public List<ComissaoModel> RetornarListagemComissao(string DataDe, string DataAte)
        {
            List<ComissaoModel> lista = new List<ComissaoModel>();
            ComissaoModel item;
            DAL objDal = new DAL();
            string sql ="select v1.id, v1.data,  v2.nome, v1.total as valor  from " +
                   "venda v1 inner Join vendedor v2 on v1.vendedor_id = v2.id " +
                   $"Where v1.data >='{DataDe}' and v1.data <= '{DataAte}'" +
                   "order by nome, data";

            DataTable dt = objDal.RetDatatable(sql);          
            

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new ComissaoModel
                {
                    Id = int.Parse(dt.Rows[i]["Id"].ToString()),
                    Data = DateTime.Parse(dt.Rows[i]["data"].ToString()).ToString("dd/MM/yyyy"),
                    Nome = dt.Rows[i]["nome"].ToString(),
                    Valor = double.Parse(dt.Rows[i]["valor"].ToString()),
                    // Abaixo calcula 5% de comissão do vendedor.
                    Comissao = decimal.Parse(dt.Rows[i]["valor"].ToString()) / 100 * 5, 
                };
                lista.Add(item);
            }
            return lista;
        }
        // fIM DA COMISSÃO DE VENDEDORES
    }
}
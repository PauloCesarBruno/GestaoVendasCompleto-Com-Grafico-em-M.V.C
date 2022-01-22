using SistemaVendas.Uteis;
using System;
using System.Collections.Generic;
using System.Data; // Importado
using System.Linq;
using System.Threading.Tasks;

namespace SistemaVendas.Models // Importado
{
    // Para Atender os Filtros de Perírodo
    public class RelatorioModel
    {
        public DateTime DataDe { get; set; }
        public DateTime DataAte { get; set; }
    }

    public class GraficoProduto
    {
        public double QtdeVendido { get; set; }
        public int CodigoProduto { get; set; }
        public string DescricaoProduto { get; set; }


        public List<GraficoProduto> RetornarGrafico()
        {
            DAL objDAL = new DAL();
            string sql = "Select sum(qtde_produto) as qtde, p.nome as produto from itens_venda i inner join produto p on i.produto_id = p.id group by nome;";
            DataTable dt = objDAL.RetDatatable(sql);

            List<GraficoProduto> lista = new List<GraficoProduto>();
            GraficoProduto item;

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                item = new GraficoProduto();
                item.QtdeVendido = double.Parse(dt.Rows[i]["qtde"].ToString());
                item.DescricaoProduto = dt.Rows[i]["produto"].ToString();
                lista.Add(item);
            }
            return lista;
        }
    }
}

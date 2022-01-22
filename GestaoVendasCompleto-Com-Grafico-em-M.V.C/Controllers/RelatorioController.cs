using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models; // Importado

namespace SistemaVendas.Controllers
{
    public class RelatorioController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Vendas()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Vendas(RelatorioModel relatorio)
        {

            if (relatorio.DataDe.Year == 0001)
            {
                ViewBag.ListaVendas = new VendaModel().ListagemVendas();
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy/MM/dd");
                string DataAte = relatorio.DataAte.ToString("yyyy/MM/dd");
                ViewBag.ListaVendas = new VendaModel().ListagemVendas(DataDe, DataAte);
            }

            return View();
        }


        [HttpPost]
        public IActionResult Comissao(RelatorioModel relatorio)
        {
            if (relatorio.DataDe.Year == 0001)
            {
                Comissao(); // Chamada do metodo Comissão Abaixo
            }
            else
            {
                string DataDe = relatorio.DataDe.ToString("yyyy/MM/dd");
                string DataAte = relatorio.DataAte.ToString("yyyy/MM/dd");
                ViewBag.ListaComissao = new ComissaoModel().ListagemComissao(DataDe, DataAte);
            }
            return View();
        }

        public IActionResult Grafico()
        {
            List<GraficoProduto> lista = new GraficoProduto().RetornarGrafico();
            string valores = "";
            string labels = "";
            string cores = "";


            var random = new Random();
            // Percorre a Lista de itens para compor o Grafico
            for (int i = 0; i < lista.Count; i++)
            {
                valores += lista[i].QtdeVendido.ToString() + ",";
                labels += "'" + lista[i].DescricaoProduto.ToString() + "',";

                // Escolher Aleatoriamente as Cores para Compor as Partes do Grafico tipo Torta
                cores += "'" + String.Format("#{0:X6}", random.Next(0X1000000)) + "',";
            }

            ViewBag.Valores = valores;
            ViewBag.Labels = labels;
            ViewBag.Cores = cores;

            return View();
        }

        public IActionResult Comissao()
        {
            ViewBag.ListaComissao = new ComissaoModel().ListagemComissao();
            return View();
        }
    }
}
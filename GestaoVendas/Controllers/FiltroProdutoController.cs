using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace GestaoVendasSQLServer.Controllers
{
    public class FiltroProdutoController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filtro()
        {   // Renderiza e traz todos os Produtos
            ViewBag.ListaProdutosCod = new ProdutoModel().ListarTodosProdutos();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ProdutoModel produto)
        {
            try
            {
                // Filtra o Produto pelo Código
                String Cod = produto.Id.ToString();
                ViewBag.ListaProdutosCod = new ProdutoModel().ListagemDeProdutos(int.Parse(Cod));
            }
            catch
            {
                //
            }
            return View();
        }
    }
}

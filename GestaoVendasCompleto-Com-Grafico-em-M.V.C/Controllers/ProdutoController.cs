using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models; // Importado

namespace SistemaVendas.Controllers
{
    public class ProdutoController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaProdutos = new ProdutoModel().ListarTodosProdutos();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id !=null)
            {
                // Carregar o registro do produto numa ViewBag
                ViewBag.Produto = new ProdutoModel().RetornarProduto(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ProdutoModel produto)
        {
            if (ModelState.IsValid)
            {
                produto.Gravar();
                return RedirectToAction("Index");
            }
            return View();
        }

        // Excluir Registro
        public IActionResult Excluir(int Id)
        {
            ViewData["IdExcluir"] = Id;
            return View();
        }
              
        public IActionResult ExcluirProduto(int Id)
        {
            {
                // Tratamento para que se o produto não estiver vinculado a uma venda Exclui
                // Se Caso não, Não Exclui... Redireciona para View (Tratamento).
                try
                {
                    new ProdutoModel().Excluir(Id);
                    return View();
                }
                catch (Exception)
                {
                    return View("Tratamento");
                }
            }
        }
    }
}
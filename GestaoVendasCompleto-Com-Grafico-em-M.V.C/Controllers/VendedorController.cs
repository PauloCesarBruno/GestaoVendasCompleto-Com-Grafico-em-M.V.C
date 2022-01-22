using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models; // Importado

namespace SistemaVendas.Controllers
{
    public class VendedorController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaVendedores = new VendedorModel().ListarTodosVendedores();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id !=null)
            {
                // Carregar o registro do vendedor numa ViewBag
                ViewBag.Vendedor = new VendedorModel().RetornarVendedor(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(VendedorModel vendedor)
        {
            if (ModelState.IsValid)
            {
                vendedor.Gravar();
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

        public IActionResult ExcluirVendedor(int Id)
        {
            {
                // Tratamento para que se o Vendedor não estiver vinculado a uma venda Exclui
                // Se Caso não, Não Exclui... Redireciona para View (Tratamento).
                try
                {
                    new VendedorModel().Excluir(Id);
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
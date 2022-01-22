using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Importado
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models; // Importado

namespace SistemaVendas.Controllers
{
    public class VendaController : Controller
    {
        private IHttpContextAccessor httpContext;

        // Recebe o Contexto HTTP via Injeção de Dependência
        public VendaController(IHttpContextAccessor HttpContextAccessor)
        {
            httpContext = HttpContextAccessor;
        }

        [HttpGet]
        public IActionResult Index()
        {
            ViewBag.ListaVendas = new VendaModel().ListagemVendas();
            return View();
        }

        [HttpGet]
        public IActionResult Registrar()
        {
            CarregarDados();
            return View();
        }

        [HttpPost]
        public IActionResult Registrar(VendaModel venda)
        {
            // Captura o Id do Vendedor Logado no Sistema para direcionar a venda ao mesmo.
            venda.Vendedor_Id = httpContext.HttpContext.Session.GetString("IdUsuarioLogado");
            if (ModelState.IsValid)
            {
                venda.Inserir();
                CarregarDados();
                return RedirectToAction("Index");
            }
            else
            {
                // View Criada apenas para tratar Reg. de Venda.
                return View ("Tratamento"); 
            }
        }           
        
        private void CarregarDados()
        {
            ViewBag.ListaClientes = new VendaModel().RetornarListaClientes();
            ViewBag.ListaVendedores = new VendaModel().RetornarListaVendedores();
            ViewBag.ListaProdutos = new VendaModel().RetornarListaProdutos();
        }
    }
}


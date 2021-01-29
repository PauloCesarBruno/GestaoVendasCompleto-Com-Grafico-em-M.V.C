using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace GestaoVendasSQLServer.Controllers
{
    public class FiltroVendedorController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filtro()
        {   // Renderiza e traz todos os Vendedores
            ViewBag.ListaVendedor = new VendedorModel().ListarTodosVendedores();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(VendedorModel vendedor)
        {
            try
            {
                // Filtra o Vendedor pelo Código
                String Cod = vendedor.Id.ToString();
                ViewBag.ListaVendedor = new VendedorModel().ListagemVendedores(int.Parse(Cod));
            }
            catch
            {
                //
            }
            return View();
        }
    }
}
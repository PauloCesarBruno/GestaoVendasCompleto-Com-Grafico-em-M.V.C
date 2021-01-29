using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;

namespace GestaoVendasSQLServer.Controllers
{
    public class FiltroClienteController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Filtro()
        {   // Renderiza e traz todos os clientes
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
            return View();
        }

        [HttpPost]
        public IActionResult Filtro(ClienteModel cliente)
        {
            try
            {
                // Filtra o Cliente pelo CPF
                String cpf = cliente.CPF_CNPJ.ToString();
                ViewBag.ListaClientes = new ClienteModel().ListagemClientes(cpf);
            }
            catch
            {
                //
            }
            return View();
        }
    }
}

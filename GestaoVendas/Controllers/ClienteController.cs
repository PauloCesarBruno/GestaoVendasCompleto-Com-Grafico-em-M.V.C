using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models; // Importado

namespace SistemaVendas.Controllers
{
    public class ClienteController : Controller
    {
        public IActionResult Index()
        {
            ViewBag.ListaClientes = new ClienteModel().ListarTodosClientes();
            return View();
        }

        [HttpGet]
        public IActionResult Cadastro(int? Id)
        {
            if (Id != null)
            {
                // Carregar o registro do cliente numa ViewBag
                ViewBag.Cliente = new ClienteModel().RetornarCliente(Id);
            }
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(ClienteModel cliente)
        {
            if (ModelState.IsValid)
            {
                cliente.Gravar();
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

        public IActionResult ExcluirCliente(int Id)
        {
            // Tratamento para que se o Vendedor não estiver vinculado a uma venda Exclui
            // Se Caso não, Não Exclui... Redireciona para View (Tratamento).
            try
            {
                new ClienteModel().Excluir(Id);
                return View();
            }
            catch (Exception)
            {
                return View("Tratamento");
            }
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
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http; // Importado
using Microsoft.AspNetCore.Mvc;
using SistemaVendas.Models;


namespace SistemaVendas.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Menu()
        {
            return View();
        }
               
        [HttpGet]
        public IActionResult Login(int? Id) // Rotina para realizar o Logout
        {
            if (Id != null)
            {
                if (Id==0)
                {
                    HttpContext.Session.SetString("IdUsuarioLogado", string.Empty);
                    HttpContext.Session.SetString("NomeUsuarioLogado", string .Empty);
                }
            }
            // Fim da rotina para realizar o Logout

            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginModel login) // Instanciar a Classe e dar um nome
        {
            if (ModelState.IsValid) 
            {
                // Cria objeto booleano (loginOK) e verifica validação
                // No metodo "ValidarLogin();" que esta na classe LoginModel.
                bool  loginOK = login.ValidarLogin();

                if(loginOK)
                {
                  HttpContext.Session.SetString("IdUsuarioLogado", login.Id);
                  HttpContext.Session.SetString("NomeUsuarioLogado", login.Nome);
                  return RedirectToAction("Menu","Home");
                }
                else
                {
                    //Variável temporária
                    TempData["ErrorLogin"] = "Email e/ou Senha Incorretos, Verifique e tente novamente !!!";
                }
            }

            return View();
        }

        public IActionResult SolicitacaoSenha()
        {          
            return View();
        }

        public IActionResult LembrarSenha()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}

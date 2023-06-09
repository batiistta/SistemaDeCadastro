using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;


namespace SistemaDeCadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class RestritoController : Controller
    {

        public IActionResult Index()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

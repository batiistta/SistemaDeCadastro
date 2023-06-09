using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using SistemaDeCadastro.Models;
using Newtonsoft.Json;

namespace SistemaDeCadastro.ViewsComponents
{
    public class Menu : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            string sessaoUsuario = HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null; 
            Usuario  usuario = JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);    
            return View(usuario);
        }
    }
}
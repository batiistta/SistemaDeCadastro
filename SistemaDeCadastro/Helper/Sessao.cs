using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Models;
using Newtonsoft.Json;

namespace SistemaDeCadastro.Helper
{
    public class Sessao : ISessao
    {
        private readonly IHttpContextAccessor _httpContext;

        public Sessao(IHttpContextAccessor httpContext)
        {
            _httpContext = httpContext;
        }

        public void CriarSessaoUsuario(Usuario usuario)
        {
            string valor = JsonConvert.SerializeObject(usuario);
            _httpContext?.HttpContext?.Session.SetString("sessaoUsuarioLogado", valor);
        }

        public Usuario BuscarSessaoUsuario()
        {
            string sessaoUsuario = _httpContext.HttpContext.Session.GetString("sessaoUsuarioLogado");
            if (string.IsNullOrEmpty(sessaoUsuario)) return null;
            return JsonConvert.DeserializeObject<Usuario>(sessaoUsuario);

        }        

        public void RemoverSessaoUsuario()
        {
            _httpContext.HttpContext.Session.Remove("sessaoUsuarioLogado");
        }
    }
}

using SistemaDeCadastro.Helper;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;


namespace SistemaDeCadastro.Controllers
{
    public class LoginController : Controller

    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;
        private readonly ISessao _sessao;


        public LoginController(IUsuarioRepositorio usuarioRepositorio, ISessao sessao)
        {
            _usuarioRepositorio = usuarioRepositorio;
            _sessao = sessao;

        }

        public IActionResult Index()
        {
            //Se usuario estiver logado redirecione para home.
            if (_sessao.BuscarSessaoUsuario() != null) return RedirectToAction("Index", "Home");


            return View();
        }

        [HttpPost]
        public IActionResult Entrar(Login login)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuario = _usuarioRepositorio.BuscarLogin(login.Email);

                    if (usuario != null) ;
                    {
                        if (usuario.SenhaValida(login.Senha))
                        {
                            _sessao.CriarSessaoUsuario(usuario);
                            return RedirectToAction("Index", "Home");
                        }

                        TempData["MensagemErro"] = $"Senha inválida. Verifique e tente novamente";
                    }
                    TempData["MensagemErro"] = $"Login e/ou senha inválido(s). Verifique as informações e tente novamente";
                }

                return View("Index");
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao realizar o login, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        public IActionResult Sair()
        {
            _sessao.RemoverSessaoUsuario();
            return RedirectToAction("Index", "Login");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

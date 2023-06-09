using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SistemaDeCadastro.Repositorio;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Helper;
using SistemaDeCadastro.Filters;
using SistemaDeCadastro.Data;

namespace SistemaDeCadastro.Controllers
{
    public class UsuarioController : Controller
    {
        private readonly IUsuarioRepositorio _usuarioRepositorio;

        private readonly SistemaDeCadastroContext _context;

        private readonly ISessao _sessao;

        //variavel _usuario repositorio recebe o que esa sendo injetado pelo construtor.
        public UsuarioController(IUsuarioRepositorio usuarioRepositorio, SistemaDeCadastroContext context, ISessao sessao
        )
        {
            _usuarioRepositorio = usuarioRepositorio;
            _context = context;
            _sessao = sessao;

        }
        [PaginaParaUsuarioLogado]
        public IActionResult Perfil()
        {
            Usuario usuario = _sessao.BuscarSessaoUsuario();
            usuario = _usuarioRepositorio.ListarID(usuario.Id);
            return View(usuario);
        }

        [PaginaParaAdminLogado]
        public IActionResult Index()
        {
            List<Usuario> usuarios = _usuarioRepositorio.BuscarTodosUsuarios();
            return View(usuarios);
        }

        public IActionResult Criar()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Criar(Usuario usuario)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    usuario = _usuarioRepositorio.Adicionar(usuario);
                    TempData["MensagemSucesso"] = "Usuário cadastrado com sucesso";
                    return RedirectToAction("Index", "Login");
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao cadastrar o usuário, detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }

        }
        [PaginaParaUsuarioLogado]
        public IActionResult Editar(int id)
        {
            Usuario usuario = _usuarioRepositorio.ListarID(id);
            return View(usuario);
        }


        [PaginaParaUsuarioLogado]
        [HttpPost]
        public IActionResult Editar(UsuarioSemSenha usuarioSemSenha)
        {
            try
            {
                Usuario usuario = null;
                Usuario usuariosessao = _sessao.BuscarSessaoUsuario();

                if (ModelState.IsValid)
                {
                    if (usuariosessao.Perfil == Enums.PerfilEnum.Admin)
                    {
                        usuario = new Usuario()
                        {
                            Id = usuarioSemSenha.Id,
                            Nome = usuarioSemSenha.Nome,
                            Email = usuarioSemSenha.Email,
                            ConfirmeEmail = usuarioSemSenha.ConfirmeEmail,
                            Perfil = usuarioSemSenha.Perfil
                        };

                        usuario = _usuarioRepositorio.Atualizar(usuario);
                        TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                        return RedirectToAction("Index", usuario);
                    }

                    usuario = new Usuario()
                    {
                        Id = usuarioSemSenha.Id,
                        Nome = usuarioSemSenha.Nome,
                        Email = usuarioSemSenha.Email,
                        ConfirmeEmail = usuarioSemSenha.ConfirmeEmail,
                        Perfil = usuarioSemSenha.Perfil
                    };

                    usuario = _usuarioRepositorio.Atualizar(usuario);
                    TempData["MensagemSucesso"] = "Usuário atualizado com sucesso";
                    return RedirectToAction("Perfil", usuario);
                }
                return View(usuario);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao atualizar o cadastro do usuário, detalhe do erro: {erro.Message}";
                return View("Perfil");
            }
        }
        [PaginaParaAdminLogado]
        public IActionResult ApagarConfirmacao(int id)
        {
            Usuario usuario = _usuarioRepositorio.ListarID(id);
            return View(usuario);
        }

        [PaginaParaAdminLogado]
        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _usuarioRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Usuário apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro ao apagar o usuário";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao apagar o usuário, detalhe do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

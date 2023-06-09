using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Helper;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;
using Microsoft.AspNetCore.Mvc;
using SistemaDeCadastro.Filters;
using Microsoft.AspNetCore.Http;
using SistemaDeCadastro.Data;

namespace SistemaDeCadastro.Controllers
{
    [PaginaParaUsuarioLogado]
    public class ContatoController : Controller
    {
        private readonly IContatoRepositorio _contatoRepositorio;

        private readonly SistemaDeCadastroContext _context;
        private readonly ISessao _sessao;
        public ContatoController(IContatoRepositorio contatoRepositorio, ISessao sessao)
        {
            _contatoRepositorio = contatoRepositorio;
            _sessao = sessao;
        }

        public IActionResult Index(int pg = 1)
        {

            Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();
            List<Contato> contatos = new List<Contato>();

            try
            {
                if (usuarioLogado.Perfil == Enums.PerfilEnum.Admin)
                {
                    contatos = _contatoRepositorio.BuscarTodosContatosPerfilAdm();                    
                    
                }else if (usuarioLogado.Perfil == Enums.PerfilEnum.Padrao)
                {
                    contatos = _contatoRepositorio.BuscarTodosContatos(usuarioLogado.Id);                    
                }

                // começo da paginação

                //Define quantos itens serão exibidos por página
                const int pageSize = 4;
                if (pg < 1)
                    pg = 1;
                int recsCount = contatos.Count();

                var paginacao = new Paginacao(recsCount, pg, pageSize);

                int recSkip = (pg - 1) * pageSize;

                var data = contatos.Skip(recSkip).Take(paginacao.PageSize).ToList();

                this.ViewBag.Paginacao = paginacao;

                //return View(contatosTodos);

                return View(data);
                //fim da paginação
            }
            catch (Exception ex)
            {
                TempData["MensagemErro"] = ($"Ocorreu um erro ao buscar os contatos: {ex.Message}");
                return RedirectToAction("Index");
            }

        }

        public IActionResult Criar()
        {
            return View();
        }
        //Criando um novo contato

        [HttpPost]
        public IActionResult Criar(Contato contato)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    Usuario usuariologado = _sessao.BuscarSessaoUsuario();
                    contato.FkUsuario = usuariologado.Id;

                    if (usuariologado.Perfil == Enums.PerfilEnum.Admin)
                    {
                        return View("CriarContatoVerificacaoParaAdms");
                    }
                    else
                    {
                        _contatoRepositorio.Adicionar(contato);
                        TempData["MensagemSucesso"] = "Contato cadastrado com sucesso";
                        return RedirectToAction("Index");
                    }
                }
                return View(contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao cadastrar o contato, detalhe do erro: {erro.Message} ";
                return RedirectToAction("Index");
            }

        }
        [PaginaParaUsuarioLogado]
        public IActionResult VerificarPerfil()
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();

            if (usuarioLogado.Perfil == Enums.PerfilEnum.Admin)
            {
                return View("CriarContatoVerificacaoParaAdms");
            }

            return View("Criar");
        }      

        
        public IActionResult Editar(int id)
        {
            Contato contato = _contatoRepositorio.ListarID(id);
            return View(contato);
        }

        public IActionResult ApagarConfirmacao(int id)
        {
            Contato contato = _contatoRepositorio.ListarID(id);
            return View(contato);
        }

        public IActionResult Apagar(int id)
        {
            try
            {
                bool apagado = _contatoRepositorio.Apagar(id);
                if (apagado)
                {
                    TempData["MensagemSucesso"] = "Contato apagado com sucesso";
                }
                else
                {
                    TempData["MensagemErro"] = "Ocorreu um erro ao apagar o contato";
                }

                return RedirectToAction(nameof(Index));
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao apagar o contato, detalhe do erro: {erro.Message}";
                return RedirectToAction(nameof(Index));
            }
        }


        [HttpPost]
        public IActionResult Alterar(Contato contato)
        {
            Usuario usuarioLogado = _sessao.BuscarSessaoUsuario();

            try
            {
                if (ModelState.IsValid)
                {
                    _contatoRepositorio.Atualizar(contato);
                    TempData["MensagemSucesso"] = "Contato atualizado com sucesso";
                    return RedirectToAction("Index");
                }
                return View("Editar", contato);
            }
            catch (System.Exception erro)
            {
                TempData["MensagemErro"] = $"Ocorreu um erro ao atualizar o cadastro do contato, detalhe do erro: {erro.Message}";
                return RedirectToAction("Index");
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}

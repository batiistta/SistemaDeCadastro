using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {

        private readonly SistemaDeCadastroContext _context;

        public UsuarioRepositorio(SistemaDeCadastroContext bancoContext)
        {
            _context = bancoContext;
        }

        public Usuario ListarID(int id)
        {
            return _context.usuarios.FirstOrDefault(x => x.Id == id);
        }


        public List<Usuario> BuscarTodosUsuarios()
        {
            return _context.usuarios.ToList();
        }



        public Usuario Adicionar(Usuario usuario)
        {
            usuario.DataCadastro = DateTime.Now;
            usuario.SetSenhaHash();
            _context.Add(usuario);
            //salvando alterações
            _context.SaveChanges();
            return usuario;
        }

        public Usuario Atualizar(Usuario usuario)
        {
            Usuario usuarioDB = ListarID(usuario.Id);
            if (usuarioDB == null) throw new System.Exception("Erro na atualização do usuário");
            usuarioDB.Nome = usuario.Nome;
            usuarioDB.Email = usuario.Email;
            usuarioDB.ConfirmeEmail = usuario.ConfirmeEmail;
            usuarioDB.Perfil = usuario.Perfil;
            usuarioDB.DataAtualizacao = DateTime.Now;

            _context.usuarios.Update(usuarioDB);
            _context.SaveChanges();

            return usuarioDB;
        }

        public bool Apagar(int id)
        {
            Usuario usuarioDB = ListarID(id);

            if (usuarioDB == null) throw new System.Exception("Erro ao deletar o usuário");
            _context.usuarios.Remove(usuarioDB);
            
            _context.SaveChanges();
            return true;
        }

        public Usuario BuscarLogin(string login)
        {
            return _context.usuarios.FirstOrDefault(x => x.Email.ToUpper() == login.ToUpper());
        }


    }
}
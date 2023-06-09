using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Data;
using SistemaDeCadastro.Models;
using SistemaDeCadastro.Repositorio;

namespace SistemaDeCadastro.Repositorio
{
    public class ContatoRepositorio : IContatoRepositorio
    {

        private readonly SistemaDeCadastroContext _context;

        public ContatoRepositorio(SistemaDeCadastroContext bancoContext)
        {
            _context = bancoContext;
        }

        public Contato ListarID(int id)
        {
            return _context.contatos.FirstOrDefault(x => x.Id ==id);
        }


        public List<Contato> BuscarTodosContatos(int usuarioId)
        {
                        
            return _context.contatos.Where(x => x.FkUsuario == usuarioId).ToList();
        }
      

        List<Contato> IContatoRepositorio.BuscarTodosContatosPerfilAdm()
        {
            return _context.contatos.ToList();
        }
      

        public Contato Adicionar(Contato contato)
        {
            
            _context.Add(contato);

            //salvando alterações
            _context.SaveChanges();
            return contato;
        }

        public Contato Atualizar(Contato contato)
        {
            Contato contatoDB = ListarID(contato.Id);
            if (contatoDB == null) throw new System.Exception("Erro na atualização do contato");
            contatoDB.Nome = contato.Nome;
            contatoDB.Email = contato.Email;
            contatoDB.Celular = contato.Celular;    

            _context.contatos.Update(contatoDB);
            _context.SaveChanges();
            return contatoDB;            
        }

        public bool Apagar(int id)
        {
            Contato contatoDB = ListarID(id);
            if (contatoDB == null) throw new System.Exception("Erro ao deletar o contato");
            _context.contatos.Remove(contatoDB);
            _context.SaveChanges(); 
            return true;
        }

        
    }
}
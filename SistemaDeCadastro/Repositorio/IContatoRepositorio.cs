using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public interface IContatoRepositorio
    {
        Contato ListarID(int id);
        List<Contato> BuscarTodosContatos(int usuarioId);        
        List<Contato> BuscarTodosContatosPerfilAdm();
        Contato Adicionar(Contato contato);
        Contato Atualizar(Contato contato);
        bool Apagar(int id);       
        
    }
}
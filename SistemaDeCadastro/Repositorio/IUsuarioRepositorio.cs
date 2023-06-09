using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Repositorio
{
    public interface IUsuarioRepositorio
    {
        Usuario ListarID(int id);
        List<Usuario> BuscarTodosUsuarios();
        Usuario Adicionar(Usuario usuario);
        Usuario Atualizar(Usuario usuario);
        bool Apagar(int id);
        Usuario BuscarLogin(string login);
        
    }
}
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Enums;
using SistemaDeCadastro.Helper;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Models
{
    public class Usuario
    {
     
        [Key]
        [Required]
        public int Id { get; set; }                

        [Required]
        [Display(Name = "Nome")]
        public string Nome { get; set; } 

        public PerfilEnum? Perfil { get; set; }
 
        [Required]
        [Display(Name = "E-mail")]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }
        

        [Required]
        [Display(Name = "Confirme seu E-mail")]
        [DataType(DataType.EmailAddress)]
        [Compare(nameof(Email), ErrorMessage = "Os e-mails devem ser iguais.")]
        public string ConfirmeEmail { get; set; }

        [Required]
        [DataType(DataType.Password)]        
        [Display(Name = "Senha")]
        public string Senha { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Senha), ErrorMessage = "As senhas devem ser iguais.")]
        [Display(Name = "Confirme Senha")]
        public string ConfirmeSenha { get; set; }

        public DateTime DataCadastro {get; set;}
        public DateTime? DataAtualizacao { get; set; }

        public virtual List<Contato>? Contatos { get; set; }

        public bool SenhaValida(string senha)
        {
            return Senha == senha.GerarHash();
        }

        public void SetSenhaHash()
        {
            Senha = Senha.GerarHash();
        }
    }
}
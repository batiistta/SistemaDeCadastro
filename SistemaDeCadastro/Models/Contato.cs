using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Enums;

namespace SistemaDeCadastro.Models
{
    public class Contato
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Digite o nome do contato")]
        [Display(Name = "Nome Completo")]
        public string Nome { get; set; }       

        [Required(ErrorMessage = "Digite o e-mail do contato")]
        [EmailAddress(ErrorMessage = "O e-mail informado não é valido!")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Digite o telefone corretamente")]
        [DataType(DataType.PhoneNumber)]        
        [Display(Name = "Telefone")]
        public string Celular { get; set; }

        [ForeignKey("Usuario")]
        [Required]
        public int FkUsuario { get; set; }           
        
        public virtual Usuario? Usuario { get; set; }

    }
}
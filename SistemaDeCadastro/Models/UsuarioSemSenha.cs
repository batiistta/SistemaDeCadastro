using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using SistemaDeCadastro.Enums;
using SistemaDeCadastro.Models;

namespace SistemaDeCadastro.Models
{
    public class UsuarioSemSenha
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
        
    }
}
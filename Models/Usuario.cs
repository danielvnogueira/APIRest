using System;
using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models
{
    public class Usuario
    {
        public Guid Id { get; set; }
        
        // Create View Model - System.ComponentModel.DataAnnotations
        [Required]
        public string Nome { get; set; }
        
        [Required]
        public string Email { get; set; }
        
        [Required]
        public string Senha { get; set; }
    
    }
}
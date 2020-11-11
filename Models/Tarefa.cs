using System;
using System.ComponentModel.DataAnnotations;

namespace TarefasBackEnd.Models
{
    public class Tarefa
    {
        public Guid Id { get; set; }

        public Guid UsuarioId { get; set; }

        [Required]
        public string Nome { get; set; } // Required 
        
        public bool Status { get; set; } = false;


    }
}
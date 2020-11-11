using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using TarefasBackEnd.Models;

namespace TarefasBackEnd.Repositories
{
    public interface ITarefaRepository
    {
        List<Tarefa> Read(Guid id);

        void Create(Tarefa tarefa);

        void Delete(Guid id);

        void Update(Guid id, Tarefa tarefa);
    }

    public class TarefaRepository : ITarefaRepository
    {
        //injecting dependences
        private readonly DataContext _context; 

        public TarefaRepository(DataContext context) // Create the constructor
        {
            _context = context;
        }
        // ---
        

        //CRUD
        public void Create(Tarefa tarefa) 
        {
            tarefa.Id = Guid.NewGuid(); // Create number id for task - define key id
             _context.Add(tarefa); //Create a new task
             _context.SaveChanges(); // Saving the created task
        }
        
        public void Delete(Guid id) 
        {
            var tarefa = _context.Tarefas.Find(id); // Get the task id to delete
            _context.Entry(tarefa).State = EntityState.Deleted; // Status ready to delete
            _context.SaveChanges(); // Saving the changes (Delete)
        }

        public List<Tarefa> Read(Guid id) 
        {
            return _context.Tarefas.Where(tarefa => tarefa.UsuarioId == id).ToList(); //Return only tasks of id
        }

        public void Update(Guid id, Tarefa tarefa)
        {
            var _tarefa = _context.Tarefas.Find(id);

            _tarefa.Nome = tarefa.Nome;
            _tarefa.Status = tarefa.Status;

            _context.Entry(_tarefa).State = EntityState.Modified; // Status ready to modify all state of the task
            _context.SaveChanges(); // Saving the changes 
        }
    }
}
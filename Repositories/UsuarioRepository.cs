using System;
using System.Linq;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefaBackEnd.Repositories //implementation of the interface to authentication users
{
    public interface IUsuarioRepository  //Interface
    {
        Usuario Read(string email, string senha);

        void Create(Usuario usuario);
    }

    public class UsuarioRepository : IUsuarioRepository //implementation of two absctracts methods - Read and Create
    {
        private readonly DataContext _context; // dependency injection that users in the context

        public UsuarioRepository(DataContext context) //Creating constructor that receveid DataContext
        {
            _context = context;
        } 

        public void Create(Usuario usuario) // Register new de user
        {
            usuario.Id = Guid.NewGuid(); //Setting new id user to guid
            _context.Usuarios.Add(usuario); //add new user in the global var
            _context.SaveChanges(); //after add new user, then save changes.
        }

        public Usuario Read(string email, string senha)
        {
            return _context.Usuarios.SingleOrDefault( //Return tha table users using singleofdefault method. Using System.Ling
                usuario => usuario.Email == email && usuario.Senha == senha // compare any users objects 
                // if email or senha is null return the exception with users not exist
            ); 
                
        }
    }
}
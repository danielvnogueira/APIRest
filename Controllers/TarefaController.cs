using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    //[Authorize] // Auth with my jwt token Bear -- My class need to be auth
    [ApiController]    
    [Route("tarefa")]
    public class TarefaController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous] // I commented this permisses to  anonymous users use the Method Get
        public IActionResult Get([FromServices]ITarefaRepository repository)
        {
            var id = new Guid(User.Identity.Name); // Return only data of user 
            var tarefas = repository.Read(id); //GEt task of id user
            return Ok(tarefas); //list task 
        }

        [HttpPost]
        [AllowAnonymous] // I commented this permisses to  anonymous users use the Method Get
        public IActionResult Create([FromBody]Tarefa model, [FromServices]ITarefaRepository repository)
        {
            if(!ModelState.IsValid) //Validation the task requirements
                return BadRequest(); //if validation is false then return message 
            
            model.UsuarioId = new Guid(User.Identity.Name); // Generate Guid to user

            repository.Create(model); //if validation is true then will create

            return Ok(); 

        }

        [AllowAnonymous] // I commented this permisses to  anonymous users use the Method Get
        [HttpPut("{id}")]
        public IActionResult Update(string id, [FromBody]Tarefa model, [FromServices]ITarefaRepository repository)
        {
            if(!ModelState.IsValid) //Validation the task requirements
                return BadRequest(); //if validation is false then return message 

            repository.Update(new Guid(id), model); //if validation is true then will receive data id model to update

            return Ok(); 

        }


        [AllowAnonymous] // I commented this permisses to  anonymous users use the Method Get        
        [HttpDelete("{id}")]
        public IActionResult Delete(string id, [FromServices]ITarefaRepository repository)
        {
            repository.Delete(new Guid(id)); //if validation is true then will receive data id model to delete
            return Ok(); 
        }

    }
}
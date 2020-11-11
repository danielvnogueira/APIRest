using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using TarefaBackEnd.Repositories;
using TarefasBackEnd.Models;
using TarefasBackEnd.Models.ViewModels;

namespace TarefasBackEnd.Controllers
{
    [ApiController] // Import way
    [Route("usuario")] //http://localhost:5000/usuario
    public class UsuarioController : ControllerBase
    {
        [HttpPost] 
        [Route("")] //Route to Create user with method post
        public IActionResult Create([FromBody]Usuario model, [FromServices]IUsuarioRepository repository) //method Usuario from Body e injectable Intarface UsuarioRepository from Services 
        {
            if (!ModelState.IsValid)//Do Validation
                return BadRequest(); //error 4xx

            repository.Create(model); //if Validation is Ok

            return Ok(); //return message 2xx ok
        }

        [HttpPost] // Route to login
        [Route("login")]
        public IActionResult Login([FromBody]UsuarioLogin model, [FromServices]IUsuarioRepository repository) // use my IACtionResult that received of body my user
        {
            if (!ModelState.IsValid)//Do Validation Email anda Senha
                return BadRequest(); //error 4xx

            Usuario usuario = repository.Read(model.Email, model.Senha); //Use this information in repository to consult
            
            if(usuario == null)
                return Unauthorized(); //access denied 4xx Unauthorized

            usuario.Senha = ""; // Dont show password in output

            return Ok(new {
                usuario = usuario,
                token = GenerateToken(usuario)
            }); //return message 2xx ok
        }

        private string GenerateToken(Usuario usuario) //Created method with token
        {
            var tokenHandler = new JwtSecurityTokenHandler(); //System.IdentityModel.Tokens.Jwt

            // Key cryptograph and encoding to bytes value key 
            var key = Encoding.ASCII.GetBytes("TheBigTokenCryptographyToDificultTheDecryptography");

            var descriptor = new SecurityTokenDescriptor //Microsoft.IdentityModel.Tokens
            {
                Subject = new ClaimsIdentity(new Claim[] { //System.Security.Claims -- This is my informatios that I want to save to my user
                    new Claim(ClaimTypes.Name, usuario.Id.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(30), //Token Validate Time to Expire in 30 minutes
                SigningCredentials = new SigningCredentials(
                    new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature) // Key to Security Criptografy and Algorithm
            } ;

            var token = tokenHandler.CreateToken(descriptor);
            return tokenHandler.WriteToken(token);
        }
    }
    
}
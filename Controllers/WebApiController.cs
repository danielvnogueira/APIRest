using System;
using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TarefasBackEnd.Models;
using TarefasBackEnd.Repositories;

namespace TarefasBackEnd.Controllers
{
    [Authorize] // Auth with my jwt token Bear -- My class need to be auth
    [ApiController]    
    [Route("")]
    public class WebApiController : ControllerBase
    {
        [HttpGet]
        [AllowAnonymous] // I commented this permisses to  anonymous users use the Method Get
        public IActionResult Index()
        {   
            return new ContentResult {
                ContentType = "text/html",
                StatusCode = (int)HttpStatusCode.OK,
                Content = "<html><body><B>Web API Trustly - CRUD</B></body></html>"
            };
        }
    }
}
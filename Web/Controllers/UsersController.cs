using Core.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers;
[ApiController]
[Route("api/v1/[controller]")]
public class UsersController:BaseController
{
    [HttpPost("")]
    public IActionResult CreateUser([FromBody] CreateUser createUser)
    {
        
        return Ok(new { Message = "User created successfully" });
    }
    
}
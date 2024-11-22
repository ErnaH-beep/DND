using DNDProject.Application.Interfaces;
using DNDProject.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DNDProject.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public ActionResult<IEnumerable<User>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost(Name = "SaveUser")]
        public async Task<ActionResult> SaveUserAsync(User user)
        {
            await _userService.SaveUserAsync(user);
            return CreatedAtAction("SaveUser", user);
        }

        
    }
}

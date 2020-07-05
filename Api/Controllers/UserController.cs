using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BL.Dtos;
using BL.Services.BaseRepository;
using Microsoft.AspNetCore.Mvc;
using Models.Entities;

namespace Api.Controllers
{
    public class UserController : BaseController<User>
    {
        public UserController(IBaseRepository<User> baseRepository) 
            : base (baseRepository)
        { 
        }

        [HttpGet("validateKey/{key}")]
        public IActionResult validateKey(string key)
        {
            var exist = _baseRepository.GetContext().Subject.Any(x => x.SecretKey == key);
            return Ok(exist);
        }

        [HttpPost("Validate")]
        public async Task<IActionResult> Validate([FromBody] UserDto user)
        {
            var userDb = _baseRepository.GetContext().Users
                                                         .FirstOrDefault(x => x.Name.ToLower().Contains(user.UserName.ToLower()) && 
                                                                              x.Password.ToLower().Contains(user.Password.ToLower()));
            return Ok(userDb);
        }

        [HttpPost("CreateIfNotExist")]
        public async Task<IActionResult> CreateIfNotExist([FromBody] UserDto user)
        {
            var userCreated = _baseRepository.Create(new User
            {
                Name = user.UserName,
                Password = user.Password
            });
            return Ok(userCreated);
        }
    }
}

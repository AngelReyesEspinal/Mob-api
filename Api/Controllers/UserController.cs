using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    }
}

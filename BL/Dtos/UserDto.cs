using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos
{
    public class UserDto : BaseDto.BaseDto
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
}

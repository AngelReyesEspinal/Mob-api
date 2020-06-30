using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos
{
    public class SubjectDto : BaseDto.BaseDto
    {
        public string Name { get; set; }
        public string Document { get; set; }
        public string SecretKey { get; set; }
        public int UserId { get; set; }
    }
}

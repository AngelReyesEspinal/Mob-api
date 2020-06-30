using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos
{
    public class EvaluationDto : BaseDto.BaseDto
    {
        public string Name { get; set; }
        public int SubjectId { get; set; }
    }
}

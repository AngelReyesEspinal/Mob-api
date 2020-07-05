using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos
{
    public class EvaluationDto : BaseDto.BaseDto
    {
        public string Name { get; set; }
        public bool ShowGifs { get; set; }
        public int QuestionQuantity { get; set; }
        public List<QuestionDto> QuestionsFrontEnd { get; set; }
    }
}

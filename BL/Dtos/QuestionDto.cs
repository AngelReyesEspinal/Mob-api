using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos
{
    public class QuestionDto : BaseDto.BaseDto
    {
        public string Name { get; set; }
        public string Wildcard { get; set; }
        public string FileName { get; set; }
        public string Img { get; set; }
        public int UserId { get; set; }
        public int EvaluationId { get; set; }
        public List<QuestionAnswerOptionDto> QuestionAnswerOptions { get; set; }
    }

    public class QuestionAnswerOptionDto : BaseDto.BaseDto
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
    }
}

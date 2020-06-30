using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class QuestionAnswerOption : Base.Base
    {
        public string Title { get; set; }
        public bool IsCorrect { get; set; }
        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}

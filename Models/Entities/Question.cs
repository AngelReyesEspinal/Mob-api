using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Question : Base.Base
    {
        public string Name { get; set; }
        public string Wildcard { get; set; }
        public int EvaluationId { get; set; }
        public int UserId { get; set; }
        public int? DocumentId { get; set; }
        public virtual User User { get; set; }
        public virtual Document Document { get; set; }
        public virtual Evaluation Evaluation { get; set; }
        public virtual ICollection<QuestionAnswerOption> QuestionAnswerOptions { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Evaluation : Base.Base
    {
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public virtual Subject Subject { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}

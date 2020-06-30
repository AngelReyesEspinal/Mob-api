using Models.Entities.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Subject : Base.Base
    {
        public string Name { get; set; }
        public int DocumentId { get; set; }
        public string SecretKey { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public virtual Document Document { get; set; }
        public virtual ICollection<Evaluation> Evaluations { get; set; }
    }
}

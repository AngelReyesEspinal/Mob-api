using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos.BaseDto
{
    public class BaseDto : IBaseDto
    {
        public virtual int? Id { get; set; }
        public virtual bool Deleted { get; set; }
    }
}

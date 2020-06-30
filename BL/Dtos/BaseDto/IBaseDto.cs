using System;
using System.Collections.Generic;
using System.Text;

namespace BL.Dtos.BaseDto
{
    public interface IBaseDto
    {
        int? Id { get; set; }
        bool Deleted { get; set; }
    }
}

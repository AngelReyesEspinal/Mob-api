using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities.Base
{
    public interface IBase
    {
        int Id { get; set; }
        bool Deleted { get; set; }
    }
}

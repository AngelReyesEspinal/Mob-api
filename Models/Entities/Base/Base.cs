using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities.Base
{
    public class Base : IBase
    {
        public int Id { get; set; }
        public bool Deleted { get; set; }
    }
}

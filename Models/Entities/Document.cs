using System;
using System.Collections.Generic;
using System.Text;

namespace Models.Entities
{
    public class Document : Base.Base
    {
        public string OriginalName { get; set; }
        public string FileName { get; set; }
        public string Path { get; set; }
    }
}

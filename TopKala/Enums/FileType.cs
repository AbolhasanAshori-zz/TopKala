using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TopKala.Enums
{
    public class FileType
    {
        public string Value { get; private set; }
        public FileType(string value) { Value = value; }
        public static FileType Profile { get => new FileType("img/svg"); }
    }
}
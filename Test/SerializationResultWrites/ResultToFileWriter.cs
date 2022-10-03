using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace Test.SerializationResultWrites
{
    public class ResultToFileWriter : IWriter
    {
        public string Path { get; set; }
        public ResultToFileWriter(string path)
        {
            Path = path;
        }
        public void Write(string value)
        {
            using(StreamWriter writer = new StreamWriter(Path))
            {
                writer.Write(value);
            }
        }
    }
}

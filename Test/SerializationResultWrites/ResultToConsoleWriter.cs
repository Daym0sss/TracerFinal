using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Test.Interfaces;

namespace Test.SerializationResultWrites
{
    public class ResultToConsoleWriter : IWriter
    {
        public void Write(string value)
        {
            Console.Write(value);
        }
    }
}

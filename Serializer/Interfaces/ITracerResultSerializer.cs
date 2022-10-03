using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Interfaces
{
    public interface ITracerResultSerializer<T> where T : class
    {
        string SerializeResult(T obj);
    }
}

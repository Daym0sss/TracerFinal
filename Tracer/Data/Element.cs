using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    public class Element<T> where T : class
    {
        public T Data { get; set; }
        public Element(T data)
        {   
            Data = data;
            Children = new List<Element<T>>();
        }

        internal Element<T>? Parent { get; set; }
        public List<Element<T>> Children { get; set; }
    }
}

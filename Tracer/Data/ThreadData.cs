using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    [Serializable]
    public class ThreadData
    {
        public int ThreadId { get; set; }
        public long EllapsedTime { get; set; }
        public List<Element<MethodData>> ExecutedMethods { get; set; }
        internal long PreviousTime { get; set; } 
        internal Element<MethodData>? CurrentMethod { get; set; }

        public ThreadData()
        {
            ExecutedMethods = new List<Element<MethodData>>();
        }
    }
}

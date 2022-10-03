using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serializer.Dto
{
    public class ThreadDataView
    {
        public int ThreadId { get; set; }
        public long EllapsedTime { get; set; }
        public List<MethodDataView>? Methods { get; set; }
    }
}

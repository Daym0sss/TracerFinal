using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tracer;

namespace Serializer.Dto
{
    public static class MethodInfoTransformer
    {
        public static List<ThreadDataView> Transform(this List<ThreadData> previousList)
        {
            var result = new List<ThreadDataView>();
            foreach (var item in previousList)
            {
                var tempMethods = new List<MethodDataView>();

                PushToList(item.ExecutedMethods, tempMethods);

                result.Add(new ThreadDataView()
                {
                    ThreadId = item.ThreadId,
                    EllapsedTime = item.EllapsedTime,
                    Methods = tempMethods
                });
            }
            return result;
        }

        private static void PushToList(List<Element<MethodData>> previousList, List<MethodDataView> result)
        {
            foreach(var item in previousList)
            {
                var temp = new MethodDataView()
                {
                    ClassName = item.Data.ClassName,
                    MethodName = item.Data.MethodName,
                    EllapsedTime = item.Data.EllapsedTime,
                    Methods = new List<MethodDataView>()
                };
                if(item.Children != null && item.Children.Count>0)
                {
                    PushToList(item.Children, temp.Methods);
                }
                result.Add(temp);
            }
        }
    }
}

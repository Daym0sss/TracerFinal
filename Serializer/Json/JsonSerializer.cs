using System.Text.Json;
using Tracer;
using Serializer.Dto;
using Serializer.Interfaces;
namespace Serializer.Json
{
    public class JsonSerializer: ITracerResultSerializer<List<ThreadData>>
    {
        public string SerializeResult(List<ThreadData> threads)
        {
            return System.Text.Json.JsonSerializer.Serialize(threads.Transform(),
               typeof(List<ThreadDataView>),
               new JsonSerializerOptions { WriteIndented = true });
        }
    }
}
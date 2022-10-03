
namespace Tracer
{
    [Serializable]
    public class MethodData
    {

        public string? ClassName { get; internal set; }

        public string? MethodName { get; internal set; }

        public long EllapsedTime { get; internal set; }

    }
}

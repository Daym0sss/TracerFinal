
namespace Tracer
{
    public interface ITracer    
    {
        void StartTrace();
        void StopTrace();
        List<ThreadData> GetTracerResult();

    }
}   

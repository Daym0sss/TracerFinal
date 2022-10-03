using System;
using System.Collections.Generic;
using Tracer;
using Xunit;

namespace Tests
{
    public class UnitTest1
    {
        [Fact]
        public void CheckOneMethodResult()
        {
            
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            
            tracer.StartTrace();
            tracer.StopTrace();
            result = tracer.GetTracerResult();
          
            Assert.True(result.Count ==1 && result[0].ExecutedMethods.Count == 1);
        }
        [Fact]
        public void CheckTwoMethodResultWithoutOverload()
        {
            
            
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            
            tracer.StartTrace();
            NoLoadMethod(tracer);
            tracer.StopTrace();
            result = tracer.GetTracerResult();

           
            Assert.True(result[0].ExecutedMethods[0].Children[0].Data.MethodName.Length>0);
        }
        
        public void NoLoadMethod(ITracer tracer)
        {
            tracer.StartTrace();
            int x = 1;
            x += 3;
            tracer.StopTrace();
        }

        [Fact]
        public void CheckOneMethodResultWithOverload()
        {
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            tracer.StartTrace();
            LoadMethod(tracer);
            tracer.StopTrace();
            result = tracer.GetTracerResult();

            Assert.True(result[0].ExecutedMethods[0].Data.EllapsedTime > 0);
        }

        public void LoadMethod(ITracer tracer)
        {
            tracer.StartTrace();
            long[] array = new long[1000];
            Random random = new Random();
            for(int i = 0; i < array.Length; i++)
            {
                array[i] = random.NextInt64();
            }
            Array.Sort(array);
            tracer.StopTrace();
        }

        [Fact]

        public void CheckTwoMethodResultWithOverload()
        {
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            tracer.StartTrace();
            SomeMethod(tracer);
            tracer.StopTrace();
            result = tracer.GetTracerResult();

            Assert.True(result[0].ExecutedMethods[0].Children[0].Children[0].Data.EllapsedTime>0);
        }

        public void SomeMethod(ITracer tracer)
        {
            tracer.StartTrace();
            LoadMethod(tracer);
            tracer.StopTrace();
        }
    }
}
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
            //Arrange
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            //Act
            tracer.StartTrace();
            tracer.StopTrace();
            result = tracer.GetTracerResult();
            //Assert
            Assert.True(result.Count ==1 && result[0].ExecutedMethods.Count == 1);
        }
        [Fact]
        public void CheckTwoMethodResult()
        {
            
            //Arrange
            var tracer = new Tracer.Tracer();
            List<ThreadData> result;
            //Act
            tracer.StartTrace();
            SomeMethod(tracer);
            tracer.StopTrace();
            result = tracer.GetTracerResult();

            //Assert
            Assert.NotNull(result[0].ExecutedMethods[0].Children[0].Children);
        }
        public void SomeMethod(ITracer tracer)
        {
            tracer.StartTrace();
            int x = 1;
            x += 3;
            tracer.StopTrace();
        }

    }
}
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tracer
{
    public class Tracer : ITracer
    {
        private static Stopwatch _timeMeasurer = new Stopwatch();
        private object _locker = new object();


        private ConcurrentDictionary<int, ThreadData> _threadsTable;
        private List<ThreadData> _threads;

        static Tracer()
        {
            _timeMeasurer.Start();
        }

        public Tracer()
        {
            _threads = new List<ThreadData>();
            _threadsTable = new ConcurrentDictionary<int, ThreadData>();
        }

        public List<ThreadData> GetTracerResult()
        {
            return _threads;
        }

        public void StartTrace()
        {
            ThreadData currentThread;
            {
                var currentThreadId = Thread.CurrentThread.ManagedThreadId;
                lock (_locker)
                {
                    if(_threadsTable.ContainsKey(currentThreadId))
                    {
                        currentThread = _threadsTable[currentThreadId];
                    }
                    else
                    {
                        currentThread = new ThreadData();
                        currentThread.ThreadId = currentThreadId;
                        _threadsTable.TryAdd(currentThreadId, currentThread);
                        _threads.Add(currentThread);
                        currentThread.PreviousTime = _timeMeasurer.ElapsedMilliseconds;
                        currentThread.EllapsedTime = 0;
                    }
                }
                

               
                StackTrace stackTrace = new StackTrace();
                var frame = stackTrace.GetFrame(1);
                var method = frame.GetMethod();
                
                
                if(currentThread.CurrentMethod == null)
                {
                    
                    currentThread.CurrentMethod = new Element<MethodData>(new MethodData() { 
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name
                    });

                    currentThread.CurrentMethod.Parent = null;
                    currentThread.ExecutedMethods.Add(currentThread.CurrentMethod);
                    currentThread.CurrentMethod.Data.EllapsedTime = _timeMeasurer.ElapsedMilliseconds;
                }
                else
                {
                   
                    var temp = new Element<MethodData>(new MethodData()
                    {
                        ClassName = method.DeclaringType.Name,
                        MethodName = method.Name,
                        EllapsedTime = _timeMeasurer.ElapsedMilliseconds
                    });

                    currentThread.CurrentMethod.Children.Add(temp);
                    temp.Parent = currentThread.CurrentMethod;
                    currentThread.CurrentMethod = temp;
                }

            }
        }

        public void StopTrace()
        {
            ThreadData currentThread;

            var currentThreadId = Thread.CurrentThread.ManagedThreadId;
            if (_threadsTable.ContainsKey(currentThreadId))
            {
                currentThread = _threadsTable[currentThreadId];
            }
            else
            {
                throw new Exception("Thread has not been initialized yet");
            }

            var tempTime = _timeMeasurer.ElapsedMilliseconds;
            currentThread.CurrentMethod.Data.EllapsedTime = tempTime - currentThread.CurrentMethod.Data.EllapsedTime;
            currentThread.CurrentMethod = currentThread.CurrentMethod.Parent;

            currentThread.EllapsedTime = tempTime - currentThread.PreviousTime + currentThread.EllapsedTime;
            currentThread.PreviousTime = tempTime;
        }
    }
}
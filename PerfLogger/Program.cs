using System;
using System.Diagnostics;
using System.Linq;
using System.Threading;

namespace PerfLogger
{
    class PerfLogger:IDisposable
    {
        private Action<int> _disposeAction;
        private Stopwatch _stopwatch;

        public PerfLogger(Action<int> action)
        {
            _disposeAction = action;
            _stopwatch=new Stopwatch();
            _stopwatch.Start();
        }

        public static PerfLogger Measure(Action<int> action)
        {
           return new PerfLogger(action);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _disposeAction(_stopwatch.Elapsed.Milliseconds);
        }
    }

	class Program
	{
		static void Main()
		{
			
			var sum = 0.0;
			using (PerfLogger.Measure(t => Console.WriteLine("for: {0}", t)))
				for (var i = 0; i < 100000000; i++) sum += i;
			using (PerfLogger.Measure(t => Console.WriteLine("linq: {0}", t)))
				sum -= Enumerable.Range(0, 100000000).Sum(i => (double)i);
			Console.WriteLine(sum);
			
		}
	}
}

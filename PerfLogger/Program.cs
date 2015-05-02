using System;
using System.Diagnostics;
using System.Linq;

namespace PerfLogger
{
    class PerfLogger:IDisposable
    {
        private readonly Action<TimeSpan> _disposeAction;
        private  readonly Stopwatch _stopwatch;

        public PerfLogger(Action<TimeSpan> action)
        {
            _disposeAction = action;
            _stopwatch=new Stopwatch();
            _stopwatch.Start();
        }

        public static PerfLogger Measure(Action<TimeSpan> action)
        {
           return new PerfLogger(action);
        }

        public void Dispose()
        {
            _stopwatch.Stop();
            _disposeAction(_stopwatch.Elapsed);
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

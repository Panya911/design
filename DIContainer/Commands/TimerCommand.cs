using System;
using System.IO;
using System.Threading;

namespace DIContainer.Commands
{
    public class TimerCommand : BaseCommand
    {
        private readonly CommandLineArgs arguments;

        public TimerCommand(CommandLineArgs arguments, TextWriter writer)
            :base(writer)
        {
            this.arguments = arguments;
        }

        public override void Execute()
        {
            var timeout = TimeSpan.FromMilliseconds(arguments.GetInt(0));
            Writer.WriteLine("Waiting for " + timeout);
            Thread.Sleep(timeout);
            Writer.WriteLine("Done!");
        }
    }
}
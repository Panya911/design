using System;
using System.IO;

namespace DIContainer.Commands
{
    public class PrintTimeCommand : BaseCommand
    {
        public PrintTimeCommand(TextWriter writer)
            : base(writer)
        {
            
        }
        
        public override void Execute()
        {
            Writer.WriteLine(DateTime.Now);
        }
    }
}
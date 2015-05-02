using System;
using System.IO;

namespace DIContainer.Commands
{
    class HelpComand : BaseCommand
    {
        private readonly Lazy<ICommand[]> _commands;

        public HelpComand(Lazy<ICommand[]> commands,TextWriter writer)
            :base(writer)
        {
            _commands = commands;
        }

        public override void Execute()
        {
            foreach (var command in _commands.Value)
                Writer.WriteLine(command.Name);
        }
    }
}

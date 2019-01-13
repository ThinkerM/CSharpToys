using System.IO;
using NezarkaCommonLibrary.Extensions;
using NezarkaController.CommandsLayer;
using NezarkaModel;

namespace NezarkaController
{
    public class NezarkaService
    {
        private readonly TextReader inputSource;
        private readonly TextWriter outputTarget;

        public NezarkaService(TextReader inputSource, TextWriter outputTarget)
        {
            this.inputSource = inputSource;
            this.outputTarget = outputTarget;
        }

        public void RunService()
        {
            var dataLoader = new StoreBuilder();
            IStore store = dataLoader.LoadFrom(inputSource);
            if (store == null)
            {
                OutputDataError();
                return;
            }

            var commandProcessor = new CommandProcessor();
            var parser = new CommandParser(store);
            string currentLine;
            while (!(currentLine = inputSource.ReadLine()).IsNullOrEmpty())
            {
                ICommand command = parser.Parse(currentLine);
                commandProcessor.ProcessCommand(command, outputTarget);
            }
        }

        private void OutputDataError() 
            => outputTarget.WriteLine("Data error.");
    }
}

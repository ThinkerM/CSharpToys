using System;
using System.IO;
using System.Linq;

namespace NezarkaLauncher
{
    internal static class Launcher
    {
        private static void Main(string[] args)
        {
            using (var output = GetOutput(args))
            {
                var service = new NezarkaController.NezarkaService(Console.In, output);
                service.RunService();
            }
        }

        private static TextWriter GetOutput(string[] args)
        {
            if (args.Any())
            {
                string outputFilePath = args[0];
                return new StreamWriter(outputFilePath, false);
            }
            return Console.Out;
        }
    }
}

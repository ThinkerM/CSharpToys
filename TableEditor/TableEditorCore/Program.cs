using System;
using System.IO;
using TableEditorCore.Parsing;
using TableEditorCore.Services;
using TableEditorCore.TableComponents;

namespace TableEditorCore
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            #region Input check
            if (args.Length != 2)
            {
                Console.WriteLine("Argument Error");
                return;
            }

            FileInfo inputFile, outputFile;
            try
            {
                inputFile = FileLoader.LoadFile(args[0]);
                outputFile = FileLoader.GetOrCreateFile(args[1]);
            }
            catch (IOException)
            {
                Console.WriteLine("File Error");
                return;
            }
            #endregion

            using (var inputReader = inputFile.OpenText())
            using (var outputWriter = outputFile.CreateText())
            {
                var sheetParser = new SheetParser(
                    new CellParser(
                        new BinaryFormulaParser(
                            new NonRegexCoordinatesParser())));

                string mainSheetName = inputFile.Name.RemoveEndIfExists(ComponentsConstants.SHEET_EXTENSION);
                ISheet mainSheet = sheetParser.Parse(inputReader, mainSheetName);

                var sheetRepository = new SheetRepository(sheetParser);
                sheetRepository.Add(mainSheet);
                var cellFinder = new CellFinder(sheetRepository);
                var cellEvaluator = new CellEvaluator2(cellFinder);

                mainSheet.CalculateContentData(cellEvaluator);
                mainSheet.DescribeSelf(outputWriter);
            }  
        }
    }
}

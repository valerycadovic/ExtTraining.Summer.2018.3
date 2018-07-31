using System.Configuration;
using System.Reflection;
using System;

namespace No7.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("No7.Solution.Console.trades.txt");

            // Логер поправить
            TradeImporter<Solution.TradeRecord> importer = new TradeImporter<Solution.TradeRecord>(
                new DatabaseTradeSaver(ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString), 
                new FileReader<Solution.TradeRecord>(
                    new TradeParser(new ConsoleLoger()), tradeStream));

            importer.Import();

            System.Console.ReadKey();
        }
    }
}
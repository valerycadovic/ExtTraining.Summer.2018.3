using System.Configuration;
using System.Reflection;

namespace No7.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            var tradeStream = Assembly.GetExecutingAssembly().GetManifestResourceStream("No7.Solution.Console.trades.txt");

            // Логер поправить
            // Класс TradeImporter позволяет читать и записывать данные и не зависит от их моделей способа хранения
            TradeImporter<Solution.TradeRecord> importer = new TradeImporter<Solution.TradeRecord>(
                new DatabaseTradeSaver(ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString), 
                new FileReader<Solution.TradeRecord>(
                    new TradeParser(new ConsoleLoger()), tradeStream));

            // Читает из заданного хранилище в другое заданное хранилище
            importer.Import();

            System.Console.ReadKey();
        }
    }
}
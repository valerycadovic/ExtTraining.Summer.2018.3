using System.Configuration;
using System.IO;
using System.Reflection;
using DataExportLib;

namespace No7.Solution.Console
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Stream tradeStream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("No7.Solution.Console.trades.txt"))
            {
                string connectionString = ConfigurationManager.ConnectionStrings["TradeData"].ConnectionString;

                TradeImportService service = TradeImportService.Instance;

                service.Import(
                    new TradeFileReader(tradeStream),
                    new TradeRecordParser(new[] {new TradeRecordValidator()}),
                    new DatabaseTradeSaver(connectionString),
                    new NLogLogger());
            }

            System.Console.ReadKey();
        }
    }
}
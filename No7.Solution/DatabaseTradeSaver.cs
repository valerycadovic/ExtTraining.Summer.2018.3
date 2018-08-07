namespace No7.Solution
{
    using System.Collections.Generic;
    using System.Data.SqlClient;
    using DataExportLib;

    public class DatabaseTradeSaver : IDataSaver<TradeRecord>
    {
        private readonly string connectionString;

        public DatabaseTradeSaver(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public void Save(IEnumerable<TradeRecord> data)
        {
            using (var connection = new SqlConnection(connectionString))
            {
                connection.Open();
                using (var transaction = connection.BeginTransaction())
                {
                    foreach (var trade in data)
                    {
                        var command = connection.CreateCommand();
                        command.Transaction = transaction;
                        command.CommandType = System.Data.CommandType.StoredProcedure;
                        command.CommandText = "dbo.Insert_Trade";
                        command.Parameters.AddWithValue("@sourceCurrency", trade.SourceCurrency);
                        command.Parameters.AddWithValue("@destinationCurrency", trade.DestinationCurrency);
                        command.Parameters.AddWithValue("@lots", trade.Lots);
                        command.Parameters.AddWithValue("@price", trade.Price);

                        command.ExecuteNonQuery();
                    }

                    transaction.Commit();
                }
                connection.Close();
            }
        }
    }
}

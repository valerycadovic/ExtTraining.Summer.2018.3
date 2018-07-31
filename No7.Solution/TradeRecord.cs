namespace No7.Solution
{
    /// <summary>
    /// Требуемая модель
    /// </summary>
    public class TradeRecord
    {
        public string DestinationCurrency { get; set; }

        public float Lots { get; set; }

        public decimal Price { get; set; }

        public string SourceCurrency { get; set; }
    }
}

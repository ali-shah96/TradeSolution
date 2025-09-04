namespace Trading.Api.Dto
{
    public class TradeDto
    {
        public int Id { get; set; }
        public string Symbol { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public DateTime ExecutedAt { get; set; }
    }
}

using System.ComponentModel.DataAnnotations.Schema;

namespace Trading.Api.Models
{
    public class Trade
    {
        public int Id { get; set; }
        public string Symbol { get; set; } = string.Empty;
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,4)")]
        public decimal Price { get; set; }
        public DateTime ExecutedAt { get; set; } = DateTime.UtcNow;
    }
}

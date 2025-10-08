namespace FCG.Application.Requests
{
    public class ApplyDiscountRequest
    {
        public int GameId { get; set; }
        public double DiscountPercentage { get; set; }
    }
}

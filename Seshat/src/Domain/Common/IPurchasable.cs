namespace Seshat.Domain.Common
{
    public interface IPurchasable
    {
        public decimal? PurchasePrice { get; set; }
        public string? PurchaseUrl { get; set; }
    }
}
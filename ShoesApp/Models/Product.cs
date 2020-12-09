using System;

namespace ShoesApp.Models
{
	public class Product
	{
        public int Id { get; set; }
        public string Brand { get; set; }
        public string Name { get; set; }
		public string? ProductCode { get; set; }
		public string? Color { get; set; }
		public string Size { get; set; }
		public bool? Box { get; set; }
		public string? Source { get; set; }
		public string DateOfPurchase { get; set; }
		public string? SaleDate { get; set; }
        public double PurchasePrice { get; set; }
		public double? SellingPrice { get; set; }
		public double? ShippingPrice { get; set; }
		public double? PriceWithoutShipping { get; set; }
		public double? Profit { get; set; }
        public bool IsSold { get; set; }
    }
}
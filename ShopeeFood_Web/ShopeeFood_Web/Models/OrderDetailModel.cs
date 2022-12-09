namespace ShopeeFood_Web.Models
{
    public class OrderDetailModel
    {
        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public int OrderQuantity { get; set; }

        public double Price { get; set; }

        public double Total_price { get; set; }
    }
}

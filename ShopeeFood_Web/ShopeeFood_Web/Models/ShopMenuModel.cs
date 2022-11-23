namespace ShopeeFood_Web.Models
{
    public class ShopMenuModel
    {
        public int ShopId { get; set; }

        public int ProductId { get; set; }

        public int ProductTypeId { get; set; }

        public string ProductName { get; set; }

        public double Cost { get; set; }

        public string Discount { get; set; }

        public double Price { get; set; }

        public string ProductImage { get; set; }

        public string ProductDescription { get; set; }
    }
}

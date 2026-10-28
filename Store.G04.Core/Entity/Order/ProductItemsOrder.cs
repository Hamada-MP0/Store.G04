
namespace Store.G04.Core.Entity.Order
{
    public class ProductItemsOrder
    {
        public ProductItemsOrder(
            int productId,
            string productName,
            string pictureURL)
        {
            ProductId = productId;
            ProductName = productName;
            PictureURL = pictureURL;
        }

        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string PictureURL { get; set; }
    }
}

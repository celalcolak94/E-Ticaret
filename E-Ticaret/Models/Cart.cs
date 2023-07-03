using E_Ticaret.Data.Entities;

namespace E_Ticaret.Models
{
    public class Cart
    {
        public List<CartLine> CartLines { get; set; }

        public Cart()
        {
            CartLines = new List<CartLine>();
        }

        public void AddProduct(Product product, int quantity)
        {
            var line = CartLines.FirstOrDefault(x => x.Product.Id == product.Id);

            if (line == null)
            {
                CartLines.Add(new CartLine() { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void DeleteProduct(Product product)
        {
            CartLines.RemoveAll(x => x.Product.Id == product.Id);
        }

        public decimal Total()
        {
            return CartLines.Sum(x => x.Product.Price * x.Quantity);
        }

        public void Clear()
        {
            CartLines.Clear();
        }

    }

    public class CartLine
    {
        public Product Product { get; set; }
        public int Quantity { get; set; }
    }

}

using System.Collections.Generic;
using System.Linq;

namespace Shop.Models
{
    public class Cart
    {
        public  List<CartLine> CartLines = new List<CartLine>();

        public void AddToCart(Product product, int quantity)
        {
            //CartLine line = CartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            CartLine line = CartLines.FirstOrDefault(x => x.Product.Id == product.Id);
            if (line == null)
            {
                CartLines.Add(new CartLine
                {
                    Count = quantity,
                    Product = product
                });
            }
            else
            {
                line.Count += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            CartLines.RemoveAll(x => x.Product.Id == product.Id);
        }

        public decimal ComputeTotalValue()
        {
            return CartLines.Sum(x => x.Count * x.Product.Price);
        }

        public void Clear()
        {
            CartLines.Clear();
        }

        public IEnumerable<CartLine> Lines
        {
            get { return CartLines; }
        }
    }

    public class CartLine
    {
        public virtual Product Product { get; set; }
        public int Count { get; set; }
    }
}
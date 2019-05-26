using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;


namespace OnlineShop.Domain.Entities
{
    public class Cart
    {
        [Key]
        public int CartId { get; set; }
        private List<CartLine> lineCollection = new List<CartLine>();

        public void AddItem(Product product, int quantity)
        {
            CartLine line = lineCollection.FirstOrDefault(p => p.Product.ProductId == product.ProductId);

            if (line == null)
            {
                lineCollection.Add(new CartLine { Product = product, Quantity = quantity });
            }
            else
            {
                line.Quantity += quantity;
            }
        }

        public void RemoveLine(Product product)
        {
            lineCollection.RemoveAll(l => l.Product.ProductId == product.ProductId);
        }

        public decimal ComputeTotalValue()
        {
            return lineCollection.Sum(e => e.Product.Price * e.Quantity);
        }

        public void Clear()
        {
            lineCollection.Clear();
        }

        public IEnumerable<CartLine> Lines { get {return lineCollection;} }
    }

    public class CartLine
    {
        [Key]
        public int CartLineId { get; set; }
        public int OrderId { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        
    }
}

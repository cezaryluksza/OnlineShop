using OnlineShop.Domain.Entities;

namespace OnlineShop.Domain.Abstract
{
    public interface IOrderProcessor
    {
        void ProcessOrder(Cart cart, ShippingDetails shippingDetails, string userId);
    }
}

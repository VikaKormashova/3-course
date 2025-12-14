using System;

namespace ShopStock
{
    public static class ProductExtensions
    {
        public static void ApplyDefaultDiscount(this IProduct product, int percent)
        {
            if (percent < 0 || percent > 100)
                throw new ArgumentException("Скидка должна быть от 0 до 100%");
            
            int discountAmount = product.Price * percent / 100;
            product.Price = Math.Max(0, product.Price - discountAmount);
        }
    }
}
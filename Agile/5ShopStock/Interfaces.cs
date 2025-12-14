using System;

namespace ShopStock
{
    public interface IProduct
    {
        string Name { get; set; }
        int Price { get; set; }
        void ApplyDiscount(int percent);
    }

    public interface IStockItem
    {
        int Stock { get; set; }
        bool Reserve(int qty);
    }
}
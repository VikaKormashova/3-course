using System;

namespace ShopStock
{
    public class WarehouseItem : IProduct, IStockItem
    {
        private string _name = "";
        private int _price;
        private int _stock;
        private int _minPrice;

        public string Name
        {
            get => _name;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название товара не может быть пустым");
                _name = value;
            }
        }

        public int Price
        {
            get => _price;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Цена не может быть отрицательной");
                _price = value;
            }
        }

        public int Stock
        {
            get => _stock;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Запас не может быть отрицательным");
                _stock = value;
            }
        }

        public int MinPrice
        {
            get => _minPrice;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Минимальная цена не может быть отрицательной");
                _minPrice = value;
            }
        }

        public WarehouseItem(string name, int price, int stock, int minPrice)
        {
            Name = name;
            Price = price;
            Stock = stock;
            MinPrice = minPrice;
        }

        public void ApplyDiscount(int percent)
        {
            int originalPrice = Price;
            this.ApplyDefaultDiscount(percent);
            
            if (Price < MinPrice)
                Price = MinPrice;
        }

        public bool Reserve(int qty)
        {
            if (qty <= 0)
                throw new ArgumentException("Количество должно быть положительным");
            
            if (Stock >= qty)
            {
                Stock -= qty;
                return true;
            }
            return false;
        }
    }
}
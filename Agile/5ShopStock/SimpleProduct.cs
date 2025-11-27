using System;

namespace ShopStock
{
    public class SimpleProduct : IProduct
    {
        private string _name = "";
        private int _price;

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

        public SimpleProduct(string name, int price)
        {
            Name = name;
            Price = price;
        }

        public void ApplyDiscount(int percent)
        {
            this.ApplyDefaultDiscount(percent);
        }
    }
}
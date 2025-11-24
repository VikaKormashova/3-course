using System;
using System.Linq;

namespace PaymentSystem
{
    public class CardGateway : PaymentGateway
    {
        private string _maskedPan = "";

        public string MaskedPan
        {
            get { return _maskedPan; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Маска карты не может быть пустой");
                _maskedPan = value;
            }
        }

        public CardGateway(string providerName, bool sandbox = false) 
            : base(providerName, sandbox)
        {
        }

        public void SetMaskedPan(string pan)
        {
            if (string.IsNullOrWhiteSpace(pan) || pan.Length != 16 || !pan.All(char.IsDigit))
                throw new ArgumentException("Неверный номер карты. Должно быть 16 цифр");

            string masked = $"**** **** **** {pan.Substring(12)}";
            MaskedPan = masked;
            Console.WriteLine($"Маска карты установлена: {MaskedPan}");
        }

        public override string Process(decimal amount)
        {
            Amount = amount;

            if (string.IsNullOrEmpty(MaskedPan))
                throw new InvalidOperationException("Не установлена маска карты");

            return $"Обработано {Amount} через карту {MaskedPan}";
        }

        public string Process(string cardType, decimal amount)
        {
            Amount = amount;
            return $"Обработано {Amount} через {cardType} карту {MaskedPan}";
        }
    }
}
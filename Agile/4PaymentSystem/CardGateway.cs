using System;

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
            if (string.IsNullOrWhiteSpace(pan))
                throw new ArgumentException("Номер карты не может быть пустым");
            
            string masked = $"**** **** **** {pan.Substring(pan.Length - 4)}";
            MaskedPan = masked;
            Console.WriteLine($"Маска карты установлена: {MaskedPan}");
        }

        public override string Process(decimal amount)
        {
            if (string.IsNullOrEmpty(MaskedPan))
                throw new InvalidOperationException("Не установлена маска карты");

            return $"Processed {amount} via card {MaskedPan}";
        }
    }
}
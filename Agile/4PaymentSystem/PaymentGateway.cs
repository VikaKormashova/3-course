using System;

namespace PaymentSystem
{
    public class PaymentGateway
    {
        private string _providerName = "";
        private bool _sandbox;
        private decimal _amount;

        public string ProviderName
        {
            get { return _providerName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Название провайдера не может быть пустым");
                _providerName = value;
            }
        }

        public bool Sandbox
        {
            get { return _sandbox; }
            set { _sandbox = value; }
        }

        public decimal Amount
        {
            get { return _amount; }
            protected set
            {
                if (value <= 0)
                    throw new ArgumentException("Сумма должна быть положительной");
                _amount = value;
            }
        }

        public PaymentGateway(string providerName, bool sandbox = false)
        {
            ProviderName = providerName;
            Sandbox = sandbox;
        }

        public void EnableSandbox(bool on)
        {
            Sandbox = on;
            Console.WriteLine($"Sandbox режим {(on ? "включен" : "выключен")} для {ProviderName}");
        }

        public string Info()
        {
            return $"Провайдер: {ProviderName}, Режим: {(Sandbox ? "Тестовый" : "Рабочий")}";
        }

        public virtual string Process(decimal amount)
        {
            Amount = amount;
            return $"Processed {Amount} via base";
        }

        public string Process()
        {
            return Process(100.00m);
        }

        public string Process(string description)
        {
            Amount = 50.00m;
            return $"{Process(Amount)} - {description}";
        }

        public string Process(decimal amount, string currency)
        {
            Amount = amount;
            return $"Processed {Amount} {currency} via base";
        }
    }
}
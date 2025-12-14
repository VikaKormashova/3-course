using System;

namespace PaymentSystem
{
    public class PaymentGateway
    {
        private string _providerName = "";
        private bool _sandbox;

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
            return $"Processed {amount} via base";
        }
    }
}
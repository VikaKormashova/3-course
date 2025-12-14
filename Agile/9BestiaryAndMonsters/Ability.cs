using System;

namespace BestiarySystem
{
    public class Ability
    {
        public string Code { get; }
        public int Power { get; }
        public int CooldownSeconds { get; }

        public Ability(string code, int power, int cooldownSeconds = 0)
        {
            if (string.IsNullOrWhiteSpace(code))
                throw new ArgumentException(
                    "Код способности не может быть пустым", 
                    nameof(code));
            
            if (cooldownSeconds < 0)
                throw new ArgumentOutOfRangeException(nameof(cooldownSeconds), 
                    "Перезарядка не может быть отрицательной");

            Code = code;
            Power = power;
            CooldownSeconds = cooldownSeconds;
        }

        public override string ToString()
        {
            return $"{Code} (Сила: {Power}, Перезарядка: {CooldownSeconds}с)";
        }
    }
}
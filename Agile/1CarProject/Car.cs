using System;

namespace CarProject
{
    public class Car
    {
        public string Brand { get; set; }    
        public string Model { get; set; }     
        public int Mileage { get; set; }     

        public Car(string brand, string model, int mileage = 0)
        {
            Brand = brand;
            Model = model;
            Mileage = mileage;
        }

        public void Drive(int distance)
        {
            if (distance > 0)
            {
                Mileage += distance;
                Console.WriteLine($"{Brand} {Model} проехал(а) {distance} км.");
            }
            else
            {
                Console.WriteLine("Ошибка: расстояние должно быть положительным!");
            }
        }

        public override string ToString()
        {
            return $"Автомобиль: {Brand} {Model}, Пробег: {Mileage} км";
        }
    }
}
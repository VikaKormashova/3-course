using ElectronicDevices;

var smartphone = new Smartphone("Apple", 4000, "2532x1170 пикселей");
var laptop = new Laptop("Dell", 8000, "3.2 GHz 6-ядерный процессор");

Console.WriteLine("ИНФОРМАЦИЯ О СМАРТФОНЕ:");
smartphone.ShowDeviceInfo();

Console.WriteLine("ИНФОРМАЦИЯ О НОУТБУКЕ:");
laptop.ShowDeviceInfo();
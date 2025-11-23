namespace ElectronicDevices;

public class Smartphone : Device
{
    public string ScreenResolution { get; set; }

    public Smartphone(string brand, int batteryCapacity, string screenResolution) 
        : base(brand, batteryCapacity)
    {
        ScreenResolution = screenResolution;
    }

    public override void ShowDeviceInfo()
    {
        base.ShowDeviceInfo();
        Console.WriteLine($"Разрешение экрана: {ScreenResolution}");
        Console.WriteLine($"Тип устройства: Смартфон");
        Console.WriteLine();
    }
}
namespace ElectronicDevices;

public class Device
{
    public string Brand { get; set; }
    public int BatteryCapacity { get; set; }

    public Device(string brand, int batteryCapacity)
    {
        Brand = brand;
        BatteryCapacity = batteryCapacity;
    }

    public virtual void ShowDeviceInfo()
    {
        Console.WriteLine("=== ИНФОРМАЦИЯ О УСТРОЙСТВЕ ===");
        Console.WriteLine($"Бренд: {Brand}");
        Console.WriteLine($"Емкость аккумулятора: {BatteryCapacity} mAh");
    }
}
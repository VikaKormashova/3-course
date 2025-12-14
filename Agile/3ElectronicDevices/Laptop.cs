namespace ElectronicDevices;

public class Laptop : Device
{
    public string ProcessorPerformance { get; set; }

    public Laptop(string brand, int batteryCapacity, string processorPerformance) 
        : base(brand, batteryCapacity)
    {
        ProcessorPerformance = processorPerformance;
    }

    public override void ShowDeviceInfo()
    {
        base.ShowDeviceInfo();
        Console.WriteLine($"Производительность процессора: {ProcessorPerformance}");
        Console.WriteLine($"Тип устройства: Ноутбук");
        Console.WriteLine();
    }
}
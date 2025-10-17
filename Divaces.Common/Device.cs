using System;
using System.Collections.Generic;


namespace Devices.Common
{
    public class Device
    {
        public Device(string manufacturer, string model)
        {
            Id = Guid.NewGuid();
            Manufacturer = manufacturer;
            Model = model;
        }

        public Guid Id { get; set; }
        public string Manufacturer { get; set; }
        public string Model { get; set; }

        public virtual void DisplayInfo()
        {
            Console.WriteLine($"ID: {Id}");
            Console.WriteLine($"Виробник: {Manufacturer}");
            Console.WriteLine($"Модель: {Model}");
        }
    }

    public class MobileDevice : Device
    {
        public static string Category = "Portable Electronics";

        static MobileDevice()
        {
            Console.WriteLine("Створено екземпляр класу MobileDevice.");
        }

        public double ScreenSizeInches { get; set; }
        public string OperatingSystem { get; set; }

        public MobileDevice(string manufacturer, string model, double screenSize, string os)
            : base(manufacturer, model)
        {
            ScreenSizeInches = screenSize;
            OperatingSystem = os;
        }

        public delegate void PowerHandler(string message);
        public event PowerHandler OnPowerOn;

        public void PowerOn()
        {
            OnPowerOn?.Invoke("MobileDevice увімкнено!");
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Розмір екрану: {ScreenSizeInches} дюймів");
            Console.WriteLine($"Операційна система: {OperatingSystem}");
        }
    }

    public class Smartphone : MobileDevice
    {
        public int CameraMegapixels { get; set; }
        public bool HasDualSIM { get; set; }

        public Smartphone(string manufacturer, string model, double screenSize, string os, int cameraMp, bool dualSim)
            : base(manufacturer, model, screenSize, os)
        {
            CameraMegapixels = cameraMp;
            HasDualSIM = dualSim;
        }

        public static Smartphone CreateNew()
        {
            var random = new Random();
            string[] manufacturers = { "Apple", "Samsung", "Google", "Xiaomi", "OnePlus" };
            string manufacturer = manufacturers[random.Next(manufacturers.Length)];

            // Випадкові дані
            double screenSize = random.Next(60, 70) / 10.0; // 6.0 - 6.9 дюймів
            string os = manufacturer == "Apple" ? "iOS" : "Android";
            int cameraMp = random.Next(12, 200); // 12 МП до 200 МП

            return new Smartphone(
                manufacturer,
                $"S-Model-{random.Next(1000, 9999)}", // Випадкова модель
                screenSize,
                os,
                cameraMp,
                random.Next(0, 2) == 1 // True/False
            );
        }

        public static string GetDeviceType()
        {
            return "Смартфон";
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Камера: {CameraMegapixels} Мп");
            Console.WriteLine($"Наявність Dual-SIM: {(HasDualSIM ? "Так" : "Ні")}");
        }


    }

    public class Laptop : MobileDevice
    {
        public string Processor { get; set; }
        public int RAM_GB { get; set; }

        public Laptop(string manufacturer, string model, double screenSize, string os, string processor, int ram)
            : base(manufacturer, model, screenSize, os)
        {
            Processor = processor;
            RAM_GB = ram;
        }

        public static Laptop CreateNew()
        {
            var random = new Random();
            string[] manufacturers = { "Dell", "HP", "Asus", "Lenovo", "Apple" };
            string manufacturer = manufacturers[random.Next(manufacturers.Length)];

            double screenSize = random.Next(130, 175) / 10.0; 
            string os = manufacturer == "Apple" ? "macOS" : "Windows";
            string[] processors = { "Intel Core i5", "Intel Core i7", "AMD Ryzen 5", "Apple M2" };
            int[] ramOptions = { 8, 16, 32, 64 };
            int ram = ramOptions[random.Next(ramOptions.Length)];

            return new Laptop(
                manufacturer,
                $"L-Model-{random.Next(1000, 9999)}", 
                screenSize,
                os,
                processors[random.Next(processors.Length)],
                ram
            );
        }

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Процесор: {Processor}");
            Console.WriteLine($"Оперативна пам'ять: {RAM_GB} ГБ");
        }
    }
}

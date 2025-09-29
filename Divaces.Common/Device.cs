using System;
using System.Collections.Generic;

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

        public override void DisplayInfo()
        {
            base.DisplayInfo();
            Console.WriteLine($"Процесор: {Processor}");
            Console.WriteLine($"Оперативна пам'ять: {RAM_GB} ГБ");
        }
    }
}

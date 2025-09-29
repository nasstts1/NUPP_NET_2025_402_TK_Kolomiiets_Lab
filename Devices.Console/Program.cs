using Devices.Common;
using System;
using System.Linq;

public class Program
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8; 
        Console.WriteLine("Демонстрація роботи CRUD-сервісу з моделлю 'Девайси'");
        Console.WriteLine("-------------------------------------------------");

        var smartphoneService = new CrudService<Smartphone>();

        var iphone = new Smartphone("Apple", "iPhone 15 Pro", 6.1, "iOS", 48, false);
        var galaxy = new Smartphone("Samsung", "Galaxy S23 Ultra", 6.8, "Android", 200, true);

        smartphoneService.Create(iphone);
        smartphoneService.Create(galaxy);


        Console.WriteLine("1. Об'єкти створено та додано до сервісу:");
        smartphoneService.ReadAll().Print();


        Console.WriteLine("2. Читання елемента за ID (iPhone):");
        var foundPhone = smartphoneService.Read(iphone.Id);
        if (foundPhone != null)
        {
            foundPhone.DisplayInfo();
        }
        else
        {
            Console.WriteLine("Смартфон не знайдено.");
        }
        Console.WriteLine("-------------------------------------------------");


        Console.WriteLine("3. Оновлення елемента:");
        var newIphone = new Smartphone("Apple", "iPhone 16 Pro", 6.1, "iOS", 48, false);
        newIphone.Id = iphone.Id; 
        smartphoneService.Update(newIphone);
        Console.WriteLine("Оновлений список після зміни:");
        smartphoneService.ReadAll().Print();


        Console.WriteLine("4. Видалення елемента (Galaxy):");
        smartphoneService.Remove(galaxy);
        Console.WriteLine("Список після видалення:");
        smartphoneService.ReadAll().Print();

        Console.WriteLine("5. Демонстрація подій та статичних членів:");
        var myLaptop = new Laptop("Dell", "XPS 15", 15.6, "Windows", "Intel Core i7", 16);


        Console.WriteLine($"Категорія мобільних пристроїв: {MobileDevice.Category}");


        Console.WriteLine($"Тип пристрою: {Smartphone.GetDeviceType()}");


        myLaptop.OnPowerOn += message => Console.WriteLine($"Подія: {message}");
        myLaptop.PowerOn();

        Console.WriteLine("-------------------------------------------------");
        Console.WriteLine("Демонстрацію завершено. Натисніть будь-яку клавішу для виходу.");
        Console.ReadKey();
    }
}

using Devices.Common;
using Divaces.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

public class Program
{
    private const int TOTAL_OBJECTS = 10000;
    private const int PAGE_SIZE = 5;
    private static readonly string FilePath = "devices_data.json";

    public static async Task Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine($"--- Демонстрація Lab 2: Асинхронність та Паралелізм ({TOTAL_OBJECTS} об'єктів) ---");

        var service = new ICrudServiceAsync<Smartphone>(FilePath);

        Console.WriteLine($"\n1. Паралельне створення {TOTAL_OBJECTS} об'єктів, захищено Lock...");

        Parallel.For(0, TOTAL_OBJECTS, async i =>
        {
            var newPhone = Smartphone.CreateNew();
            await service.CreateAsync(newPhone);
        });

        await Task.Delay(1000);

        var initialCount = (await service.ReadAllAsync()).Count();
        Console.WriteLine($"   -> Фактично створено об'єктів: {initialCount}");

        Console.WriteLine("\n2. Статистичний аналіз цифрових значень (LINQ):");

        var allData = await service.ReadAllAsync();

        var minCamera = allData.Min(d => d.CameraMegapixels);
        var maxCamera = allData.Max(d => d.CameraMegapixels);
        var avgCamera = allData.Average(d => d.CameraMegapixels);

        var minScreen = allData.Min(d => d.ScreenSizeInches);
        var maxScreen = allData.Max(d => d.ScreenSizeInches);
        var avgScreen = allData.Average(d => d.ScreenSizeInches);

        Console.WriteLine($"   Камера (МП): Мін={minCamera}, Макс={maxCamera}, Середнє={avgCamera:F2}");
        Console.WriteLine($"   Екран (дюйми): Мін={minScreen:F2}, Макс={maxScreen:F2}, Середнє={avgScreen:F2}");

        Console.WriteLine($"\n3. Асинхронне збереження колекції у файл: {FilePath}");
        bool saved = await service.SaveAsync();
        Console.WriteLine($"   -> Збереження {(saved ? "УСПІШНЕ" : "НЕВДАЛЕ")}.");

        Console.WriteLine($"\n4. Демонстрація пагінації (сторінка 3, розмір {PAGE_SIZE}):");
        var page3 = await service.ReadAllAsync(3, PAGE_SIZE); 

        foreach (var device in page3)
        {
            Console.WriteLine($"   -> ID: {device.Id.ToString().Substring(0, 8)}... | {device.Manufacturer} {device.Model} | Камера: {device.CameraMegapixels} МП");
        }

        var firstElement = allData.First();

        var readElement = await service.ReadAsync(firstElement.Id);
        Console.WriteLine($"\n5. Асинхронне читання: {readElement.Manufacturer} {readElement.Model}");


        await service.RemoveAsync(firstElement);

        Console.WriteLine($"   -> Залишилося об'єктів після видалення: {(await service.ReadAllAsync()).Count()}");

        Console.ReadKey();
    }
}
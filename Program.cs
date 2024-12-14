
using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using Microsoft.Extensions.Configuration;

namespace Delegate
{


    internal class Program
    {
        static void Main()
        {

            // Пример использования функции расширения
            List<MyClass> items = new List<MyClass>
            {
                new MyClass(1),
                new MyClass(2),
                new MyClass(3)
            };

            MyClass? maxItem = items.GetMax(item => item.Value);

            Console.WriteLine($"Максимальное значение: {maxItem!.Value}");



            var configuration = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            string directory = configuration["Settings:Directory"];

            // Пример использования FileSearcher
            FileSearcher searcher = new FileSearcher();

            // Возможность отмены поиска
            Console.WriteLine("Введите 'q' для отмены поиска");

            searcher.FileFound += (sender, args) =>
            {
                Console.WriteLine($"Файл найден: {args.FileName}");
                
                // Проверка ввода пользователя для отмены поиска
                if (Console.KeyAvailable && Console.ReadKey(true).Key == ConsoleKey.Q)
                {
                    searcher.CancelSearch();
                    Console.WriteLine("Поиск отменен пользователем.");                    
                    Console.ReadLine();
                }
            };


            searcher.SearchDirectory(directory);
        }
    }

}


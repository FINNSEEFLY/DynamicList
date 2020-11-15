using System;
using System.Collections.Generic;

namespace DynamicListTest
{
    class Program
    {
        static void Main(string[] args)
        {
            var testList = new DynamicList<string>();
            Console.WriteLine($"Список из {testList.GetType()} создан");
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            testList.Add("Добрый вечер 1");
            testList.Add("Доброй ночи 2");
            testList.Add("Добрый день 3");
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            testList.Add("Добрый вечер 4");
            testList.Add("Доброй ночи 5");
            testList.Add("Добрый день 6");
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            testList.Add("Добрый вечер 7");
            testList.Add("Доброй ночи 8");
            testList.Add("Добрый день 9");
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            var i = 1;
            foreach (var item in testList)
            {
                Console.WriteLine($"[{i}]: {item}");
                i++;
            }
            testList.Remove("Добрый вечер 7");
            testList.RemoveAt(2);
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            i = 1;
            foreach (var item in testList)
            {
                Console.WriteLine($"[{i}]: {item}");
                i++;
            }
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
            Console.WriteLine(testList[testList.Count-1]);
            testList.Clear();
            Console.WriteLine($"Ёмкость: {testList.Capacity}; Количество элементов: {testList.Count}");
        }
    }
}
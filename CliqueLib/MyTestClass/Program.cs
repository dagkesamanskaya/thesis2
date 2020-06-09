using CliqueLib;
using System;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace TestConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            //Создаем тестовый массив
            MyTestClass[] testArr = new MyTestClass[]
            {
                new MyTestClass(1),
                new MyTestClass(2),
                new MyTestClass(3),
                new MyTestClass(4),
                new MyTestClass(5),
                new MyTestClass(6),
                new MyTestClass(7),
                new MyTestClass(8),
                new MyTestClass(9),
                new MyTestClass(10),
                new MyTestClass(11),
                new MyTestClass(12),
                new MyTestClass(13),
                new MyTestClass(14),
                new MyTestClass(15),
                new MyTestClass(16),
                new MyTestClass(17),
                new MyTestClass(18),
                new MyTestClass(19),
                new MyTestClass(20)
            };

            Stopwatch sw = Stopwatch.StartNew();//Засекаем время выполнения
            
            Console.WriteLine();
            //var MaxClique = Algorithms.MaxClique(testArr);
            var res1 = CliqueLib.CliqueLib.AllCliques(testArr);
            sw.Stop(); //Вывод времени выполнения
            for (int i = 0; i < res1.Count; i++)
                Console.WriteLine(String.Join(',', res1[i].Select(r => r.Id).ToArray()));
            Console.WriteLine();
            Console.WriteLine("{0} ms", sw.ElapsedMilliseconds);


            Console.ReadKey();

        }
    }
}

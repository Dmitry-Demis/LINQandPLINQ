using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

namespace LINQandPLINQ
{
    /*    Нечетные варианты:
     *     Сгенерировать случайно массив из N целых чисел.
     *     Затем, используя LINQ и PLINQ выполнить над сгенерированным массивом следующие действие.Посчитать время выполнения каждого запроса с использованием PLINQ и LINQ:
     *     1.	Найти все четные числа
     *     2.	Найти все нечётные числа
     *     3.	Найти все числа, сумма первой и последней цифры которых равна 6
     *     4.	Найти все числа, содержащие комбинацию цифр: 666
     *     Числа из каждой группы выводить пользователю списком, отсортированным по возрастанию
    */


    class Program
    {
        static void Main(string[] args)
        {
            Random random = new Random();
            int min = 30_000;
            int max = 300_000;
            int countOfNumbers = random.Next(min, max);
            int[] numbers = new int[countOfNumbers];
            numbers = numbers.Select(x => x = random.Next(0, 9999)).OrderBy(x=>x).ToArray();
            Stopwatch stopwatch = new Stopwatch();            
            // 1.	Найти все четные числа
            var evenSeq = numbers.Where(x => (x % 2 == 0));
            var evenParall = numbers.AsParallel().Where(x => (x % 2 == 0)).OrderBy(x => x);
            // 2.	Найти все нечётные числа
            var oddSeq = numbers.Where(x => (x % 2 != 0));
            var oddParall = numbers.AsParallel().Where(x => (x % 2 != 0)).OrderBy(x => x);
            //3.Найти все числа, сумма первой и последней цифры которых равна 6
            var sumOfFirstAndLastDigitSeq = numbers.Where(
                x =>
                {

                    var digits = x.ToString().Select(c => (int)char.GetNumericValue(c));
                    var firstDigit = digits.First();
                    var lastDigit = digits.Last();

                    return (firstDigit + lastDigit == 6);
                });
            var sumOfFirstAndLastDigitParallel= numbers.AsParallel().Where(
               x =>
               {

                   var digits = x.ToString().Select(c => (int)char.GetNumericValue(c));
                   var firstDigit = digits.First();
                   var lastDigit = digits.Last();

                   return (firstDigit + lastDigit == 6);
               }).OrderBy(x => x);
            //4.	Найти все числа, содержащие комбинацию цифр: 666
            var contains666Seq = numbers.Where(
                x =>
                {
                    return x.ToString().Contains("666");
                });
            var contains666Parallel = numbers.AsParallel().Where(
               x =>
               {
                   return x.ToString().Contains("666");
               }).OrderBy(x => x);

            Stopwatch[] stopwatches = new Stopwatch[8];
            for (int j = 0; j < stopwatches.Length; j++)
            {
                stopwatches[j] = new Stopwatch();
            }
            PrintAnArray(evenSeq, ref stopwatches[0], nameof(evenSeq));
            PrintAnArray(evenParall, ref stopwatches[1], nameof(evenParall));
            Console.WriteLine("\n\n");
            PrintAnArray(oddSeq, ref stopwatches[2], nameof(oddSeq));
            PrintAnArray(oddParall, ref stopwatches[3], nameof(oddParall));
            Console.WriteLine("\n\n");
            PrintAnArray(sumOfFirstAndLastDigitSeq, ref stopwatches[4], nameof(sumOfFirstAndLastDigitSeq));
            PrintAnArray(sumOfFirstAndLastDigitParallel, ref stopwatches[5], nameof(sumOfFirstAndLastDigitParallel));
            Console.WriteLine("\n\n");
            PrintAnArray(contains666Seq, ref stopwatches[6], nameof(contains666Seq));
            PrintAnArray(contains666Parallel, ref stopwatches[7], nameof(contains666Parallel));
            Console.WriteLine("\n\n");
            int i = 1;
            
            foreach (var stop in stopwatches)
            {
                string s = (i % 2 == 0) ? "parallel" : "sequential";
                if (s=="parallel")
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                }
                Console.WriteLine($"{s} stop {i++} = {stop.ElapsedMilliseconds}");
            }
            Console.ResetColor();
            Console.ReadLine();
        }
        public static void PrintAnArray(IEnumerable<int> n, ref Stopwatch stopwatch, string name = "")
        {
            
            int z = 0;
           
            if (n.Count()==0)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"{name} → There are not any values in the array");
                Console.ResetColor();

            }
            else
            {
                stopwatch.Start();
                foreach (var item in n)
                {
                    
                    if (z == 30)
                    {
                        z = 0;
                        Console.WriteLine();
                    }
                    Console.Write($"{item:d4} ");
                    z++;
                }
                stopwatch.Stop();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine($"\n\n {name} → Elapsed time is {stopwatch.ElapsedMilliseconds} ms\n\n");
                Console.ResetColor();
            }
           

        }
    }
}

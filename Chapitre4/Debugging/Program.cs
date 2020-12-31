using System;

namespace Debugging
{
    class Program
    {
        static double Add(double a, double b)
        {
            return a + b;
        }

        static void Main(string[] args)
        {
            var a = 4.5d;
            var b = 2.5d;
            var answer = Add(a, b);
            Console.WriteLine($"Answer is {answer}");
        }
    }
}

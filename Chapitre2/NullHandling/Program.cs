#nullable enable
using System;

namespace NullHandling
{
    class Program
    {
        static void Main(string[] args)
        {
            var address = new Address(); 
            address.Building = null; 
            address.Street = null; 
            address.City = "London"; 
            address.Region = null;
            Console.WriteLine("Hello World!");
        }
    }
}

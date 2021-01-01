using System;
using Shared;

namespace ObjectSamples
{
    class Program
    {
        static void Main(string[] args)
        {
            Employee aliceInEmployee = new Employee 
            { 
                Name = "Alice", 
                EmployeeCode = "AA123" 
            }; 

            Person aliceInPerson = aliceInEmployee; 
            if (aliceInPerson is Employee) 
            { 
                Console.WriteLine($"{nameof(aliceInPerson)} IS an Employee"); 
                Employee explicitAlice = (Employee)aliceInPerson; 
                Console.WriteLine(explicitAlice.ToString());
            }

            // With C# 9 and later, you can use the not keyword
            if (aliceInPerson is not Employee) 
            { 
                Console.WriteLine($"{nameof(aliceInPerson)} IS NOT an Employee");  
            }
        }
    }
}

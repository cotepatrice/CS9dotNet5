using System;
using System.Collections.Generic;

namespace Shared
{
    public class Person : IComparable<Person>
    {
        // fields 
        public string Name;
        public DateTime DateOfBirth;
        public List<Person> Children = new List<Person>();

        // methods 
        public virtual void WriteToConsole()
        {
            Console.WriteLine($"{Name} was born on a {DateOfBirth:dddd}.");
        }

        public int CompareTo(Person other)
        {
            return Name.CompareTo(other.Name);
        }

        // overridden methods 
        public override string ToString()
        {
            return $"{Name} is a {base.ToString()}";
        }
    }
}
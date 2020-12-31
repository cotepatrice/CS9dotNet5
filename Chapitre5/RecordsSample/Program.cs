using System;
using Shared;

namespace RecordsSample
{
    class Program
    {
        static void Main(string[] args)
        {
            var person = new ImmutablePerson 
            {
                FirstName = "Georges",
                LastName = "Cinq"
            };

            /*
            La propriété ou l'indexeur d'initialisation uniquement 'ImmutablePerson.LastName' ne peut être assigné que dans un 
            initialiseur d'objet, ou sur 'this' ou 'base' dans un constructeur d'instance ou un accesseur 'init'
            
            person.LastName = "Six";
            */

            /*
            Records should not have any state (properties and fields) that changes after instantiation. Instead, the idea is that 
            you create new records from existing ones with any changed state. This is called non-destructive mutation. 
            To do this, C# 9 introduces the with keyword:
            */
            var car = new ImmutableVehicle 
            { 
                Brand = "Mazda MX-5 RF", 
                Color = "Soul Red Crystal Metallic", 
                Wheels = 4 
            }; 
            
            var repaintedCar = car with 
            { 
                Color = "Polymetal Grey Metallic" 
            }; 
            
            Console.WriteLine(@$"Original color was {car.Color}, new color is {repaintedCar.Color}. 
                Repainted Wheels number is {repaintedCar.Wheels}. Repainted Brand number is {repaintedCar.Brand}");
        }
    }
}

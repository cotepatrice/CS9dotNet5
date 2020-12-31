using System;
using Shared;

namespace PatternMatchingSample
{
    class Program
    {
        static void Main(string[] args)
        {
            object[] passengers = 
            { 
                new FirstClassPassenger { AirMiles = 1_419 }, 
                new FirstClassPassenger { AirMiles = 16_562 }, 
                new BusinessClassPassenger(), 
                new CoachClassPassenger { CarryOnKG = 25.7 }, 
                new CoachClassPassenger { CarryOnKG = 0 }
            };

            foreach (object passenger in passengers) 
            { 
                decimal flightCost = passenger switch
                { 
                    /* Syntaxe switch statement C# 8
                    FirstClassPassenger p when p.AirMiles > 35000 
                        => 1500M, 
                    FirstClassPassenger p when p.AirMiles > 15000 
                        => 1750M, 
                    FirstClassPassenger _ 
                        => 2000M, 
                    */
                    // C# 9 syntax 
                    FirstClassPassenger p => p.AirMiles switch 
                    { 
                        > 35000 => 1500M, 
                        > 15000 => 1750M, 
                        _ => 2000M 
                    }, 
                    BusinessClassPassenger _ 
                        => 1000M, 
                    CoachClassPassenger p when p.CarryOnKG < 10.0 
                        => 500M, 
                    CoachClassPassenger _ 
                        => 650M, 
                    _ 
                        => 800M 
                };

                Console.WriteLine( $" Flight costs {flightCost:C} for {passenger}");
            }
        }
    }
}

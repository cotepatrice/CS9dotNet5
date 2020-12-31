using System;

namespace Shared
{
    public class BusinessClassPassenger
    {
        public override string ToString() 
            => "Business class";
    }
    public class FirstClassPassenger
    {
        public int AirMiles { get; set; }
        public override string ToString()
            => $"First class with {AirMiles} air miles";

    }
    public class CoachClassPassenger
    {
        public double CarryOnKG { get; set; }
        public override string ToString()
            => $"Coach class with {CarryOnKG} carry on KG";
    }
}

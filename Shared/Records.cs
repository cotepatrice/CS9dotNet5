namespace Shared 
{
    public class ImmutablePerson
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    public record ImmutableVehicle
    {
        public int Wheels { get; init; }
        public string Color { get; init; }
        public string Brand { get; init; }
    }

    public record Person 
    { 
        int Age; // public property equivalent to: // public int Age { get; init; } 
    }
}
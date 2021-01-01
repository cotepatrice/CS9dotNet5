namespace Shared 
{
    public class ImmutablePerson
    {
        public string FirstName { get; init; }
        public string LastName { get; init; }
    }

    // With the record keyword, the field becomes an init-only public property
    public record ImmutableVehicle
    {
        public int Wheels; // public property equivalent to: public int Wheels { get; init; }
        public string Color;
        public string Brand;
    }

    // Deconstructor since C# 7
    // public record ImmutableAnimal 
    // { 
    //     string Name; // i.e. public init-only properties 
    //     string Species; 
    //     public ImmutableAnimal( string name, string species) 
    //     { 
    //         Name = name; 
    //         Species = species; 
    //     } 
        
    //     public void Deconstruct( out string name, out string species) 
    //     { 
    //         name = Name; 
    //         species = Species; 
    //     } 
    // }

    // Positional record. Simpler way to define a record that does the equivalent, but parameters must be passed in specific order. 
    public record ImmutableAnimal(string Name, string Species);


}
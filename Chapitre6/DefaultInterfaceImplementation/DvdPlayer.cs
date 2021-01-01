using System;
using Shared;

namespace DefaultInterfaceImplementation
{
    public class DvdPlayer : IPlayable 
    { 
        public void Pause() 
        { 
            Console.WriteLine(" DVD player is pausing.");
        }

        public void Play() 
        { 
            Console.WriteLine(" DVD player is playing."); 
        }
    }
}

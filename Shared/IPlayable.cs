
using System;

namespace Shared 
{ 
    // Since C# 8, we can add default implementation to interfaces. Then you can add new methods to existing 
    // and already implemented interfaces
    public interface IPlayable 
    { 
        void Play(); 
        void Pause(); 
        void Stop() // default interface implementation 
        { 
            Console.WriteLine("Default implementation of Stop."); 
        } 
    } 
}
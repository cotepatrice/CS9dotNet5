using System;
using Shared;

namespace DefaultInterfaceImplementation
{
    class Program
    {
        static void Main(string[] args)
        {
            DvdPlayer player = new();

            player.Play();
            player.Pause();
        }
    }
}
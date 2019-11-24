using System;
using System.Threading;

namespace ChatBot
{
    class Program
    {
        static void Main()
        { 
            new Bot();
            Thread.Sleep(Timeout.Infinite);
        }
    }
}

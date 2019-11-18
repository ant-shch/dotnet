using System;

namespace DotNetDomainsManipulation
{
    class Program
    {
        static void Main(string[] args)
        {
            using (Isolated<Worker> isolated = new Isolated<Worker>())
            {
                isolated.Value.DoSomething();
            }
            Console.ReadLine();
        }
    }
}
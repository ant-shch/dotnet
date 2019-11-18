using System;

namespace DotNetDomainsManipulation
{
    public class Worker : MarshalByRefObject
    {
        public void DoSomething()
        {
            Console.WriteLine("Host domain: " + AppDomain.CurrentDomain.FriendlyName);
            Console.WriteLine("Working for you Master!");
        }
    }
}
using System;
using LoadAssemblyToDomainAndExecute.Bootstrap;

namespace LoadAssemblyToDomainAndExecute.Worker.Bob
{
    public class Startup : MarshalByRefObject, IBootstrap
    {
        public void Run()
        {
            Console.WriteLine("Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
            new BobWorker().DoSomething();
        }
    }
}
using System;
using LoadAssemblyToDomainAndExecute.Bootstrap;

namespace LoadAssemblyToDomainAndExecute.Worker.Joseph
{
    public class Startup : MarshalByRefObject, IBootstrap
    {
        public void Run()
        {
            Console.WriteLine("Domain Name: " + AppDomain.CurrentDomain.FriendlyName);
            new JosephWorker().DoSomething();
        }
    }
}
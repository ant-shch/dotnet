using System;

namespace LoadAssemblyToDomainAndExecute.Bootstrap
{
    public sealed class BootstrapDomain<T> : IDisposable where T : IBootstrap
    {
        private AppDomain domain;
        private T value;

        public BootstrapDomain(string assemblyName, string typeName)
        {
            domain = AppDomain.CreateDomain(assemblyName,
               null, AppDomain.CurrentDomain.SetupInformation);
            var bootstrapInstane = domain.CreateInstanceAndUnwrap(assemblyName, typeName);
            value = (T)domain.CreateInstanceAndUnwrap(assemblyName, typeName);
        }

        public T Value => value;

        public void Dispose()
        {
            if (domain != null)
            {
                AppDomain.Unload(domain);

                domain = null;
            }
        }
    }
}
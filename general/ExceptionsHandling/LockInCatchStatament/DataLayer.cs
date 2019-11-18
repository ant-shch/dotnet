using System;

namespace LockInCatchStatament
{
    public class DataLayer
    {
        static object lockobj = new object();

        public void Init()
        {
            try
            {
                throw new Exception();
            }
            catch (Exception)
            {
                lock (lockobj)
                {
                    Console.WriteLine(AppDomain.GetCurrentThreadId());
                    System.Threading.Thread.Sleep(4000);
                }
            }
        }
    }
}
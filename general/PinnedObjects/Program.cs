using System;
using System.Runtime.InteropServices;

namespace PinnedObjects
{
    class Program
    {
        static void Main(string[] args)
        {
            byte[] bytes = new byte[128];
            GCHandle gch = GCHandle.Alloc(bytes, GCHandleType.Pinned);
            GC.Collect();
            Console.WriteLine("Generation: " + GC.GetGeneration(bytes));
            gch.Free();
            GC.KeepAlive(bytes);
            Console.ReadLine();
        }
    }
}
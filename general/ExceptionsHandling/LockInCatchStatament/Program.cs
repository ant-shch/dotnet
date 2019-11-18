using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockInCatchStatament
{
    class Program
    {
        static void Main(string[] args)
        {
            var d = new DataLayer();

            new Task(()=>d.Init()).Start();
            new Task(()=>d.Init()).Start();

            Console.ReadLine();
        }
    }
}

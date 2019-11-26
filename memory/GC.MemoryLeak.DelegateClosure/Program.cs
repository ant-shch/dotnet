using System;
using System.Collections.Generic;

namespace GC.MemoryLeak.DelegateClosure
{
    class Program
    {

        static void Main(string[] args)
        {
            var r = new SamurayManager().WhoHasWeapon();
            Console.WriteLine(r);
        }

        static Samuray LookForWeapon(List<Samuray> samurays, Weapon weapon)
        {
            return samurays.Find(s => s.Weapon == weapon);
        }
    }

    class SamurayManager
    {
        readonly Weapon w1 = new Weapon();

        private readonly List<Samuray> samurays;

        public SamurayManager()
        {
            samurays = new List<Samuray> { new Samuray(new Weapon()), new Samuray(w1) };
        }

        public Samuray WhoHasWeapon()
        {
            return samurays.Find(d => d.Weapon == w1);
        }
    }

    class Samuray
    {
        public readonly Weapon Weapon;

        public Samuray(Weapon weapon)
        {
            Weapon = weapon;
        }
    }

    class Weapon
    {

    }
}

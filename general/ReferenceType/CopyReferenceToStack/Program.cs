using System;

namespace CopyReferenceToStack
{

    static class Animal
    {
        public static AnimalInfo Info = new AnimalInfo(10);
    }

    class AnimalInfo
    {
        public AnimalInfo(int age)
        {
            Age = age;
        }

        public int Age { get; private set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var animal = Animal.Info;
            animal = new AnimalInfo(5);

            Console.WriteLine(animal.Age);

            Console.Read();
        }
    }
}

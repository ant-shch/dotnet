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
            // override reference in the variable that stores in the stack, keep the reference in the Animal.Info without change
            animal = new AnimalInfo(5);
            Console.WriteLine(Animal.Info.Age); // 10
            Console.WriteLine(animal.Age); // 5

            Console.Read();
        }
    }
}

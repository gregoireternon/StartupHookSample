using CommonLib;
using System;
using System.Linq;

namespace ListVegetables
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("This project's goal is to list all available vegetables");
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(s => s.GetTypes())
                .Where(p => typeof(IVegetable).IsAssignableFrom(p) && p != typeof(IVegetable));

            foreach (var t in types)
            {
                IVegetable vege = t.GetConstructor(new Type[] { }).Invoke(new object[] { })as IVegetable;
                Console.WriteLine($"{vege.GetName()} is a vegetable");
            }

            //Console.ReadLine();
        }
    }
}

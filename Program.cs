using System;
using System.Numerics;

namespace ProtectedVariable
{
    class Program
    {
        static void Main(string[] args)
        {
            Random ran = new Random();
            int randomInt = 0;

            randomInt = ran.Next();
            Console.WriteLine($@"Setting protecedObject1 value to {randomInt}");
            ProtecedObject<int> protecedObject1 = new ProtecedInteger(randomInt);

            randomInt = ran.Next();

            Console.WriteLine($@"Setting protecedObject2 value to {randomInt}");
            ProtecedObject<int> protecedObject2 = new ProtecedInteger(randomInt);

            while (true)
            {
                Console.WriteLine($@"protecedObject1 value: {protecedObject1.Value}");
                Console.WriteLine($@"protecedObject2 value: {protecedObject2.Value}");
                var keyInfo = Console.ReadKey();
                if (keyInfo.Key == ConsoleKey.Enter)
                {
                    randomInt = ran.Next();
                    protecedObject1.Value = randomInt;
                }
                Console.WriteLine();
            }
        }
    }
}

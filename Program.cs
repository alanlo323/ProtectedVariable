using System;
using System.Numerics;

namespace ProtectedVariable
{
    class Program
    {
        static void Main(string[] args)
        {
            new Demo().Run();
        }

        class Demo : ProtecedObjectCallBack
        {
            public void OnValueInvalid(dynamic eventInfo)
            {
                Console.WriteLine($@"Invalid value injection detected.");
                Console.WriteLine($@"InjectedValue:{eventInfo.injectedValue}");
                Console.WriteLine($@"ProtectedValue:{eventInfo.protectedValue}");
            }

            public void Run()
            {
                Random ran = new Random();
                int randomInt = ran.Next(10, 100);
                Console.WriteLine($@"Setting protecedObject1 value to {randomInt}");
                ProtecedObject<int> protecedObject1 = new ProtecedInteger(randomInt, this);

                randomInt = ran.Next(10, 100);

                Console.WriteLine($@"Setting protecedObject2 value to {randomInt}");
                ProtecedObject<int> protecedObject2 = new ProtecedInteger(randomInt);

                while (true)
                {
                    Console.WriteLine($@"protecedObject1 value: {protecedObject1.Value}");
                    Console.WriteLine($@"protecedObject2 value: {protecedObject2.Value}");
                    Console.WriteLine();

                    var keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        randomInt = ran.Next(10, 100);
                        protecedObject1.Value = randomInt;
                    }
                }
            }
        }
    }
}

using System;
using System.Collections.Generic;

namespace ProtectedVariable
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            new Demo().Run();
        }

        private class Demo : IObserver<object>
        {
            public void OnCompleted()
            { }

            public void OnError(Exception error)
            {
                Console.WriteLine($@"Invalid value injection detected.");
                foreach (var key in error.Data.Keys)
                {
                    Console.WriteLine($@"{key}: {error.Data[key]}");
                }
            }

            public void OnNext(object value)
            { }

            public void Run()
            {
                Random ran = new Random();
                int randomInt = ran.Next(10, 100);
                Console.WriteLine($@"Setting protecedObject1 value to {randomInt}");
                ProtecedObject<int> protecedObject1 = new ProtecedInteger(randomInt);
                protecedObject1.Subscribe(this);

                randomInt = ran.Next(10, 100);

                Console.WriteLine($@"Setting protecedObject2 value to {randomInt}");
                ProtecedObject<int> protecedObject2 = new ProtecedInteger(randomInt);
                protecedObject2.Subscribe(this);

                while (true)
                {
                    Console.WriteLine($@"protecedObject1 value: {protecedObject1}");
                    Console.WriteLine($@"protecedObject2 value: {protecedObject2}");
                    Console.WriteLine();

                    var keyInfo = Console.ReadKey();
                    if (keyInfo.Key == ConsoleKey.Enter)
                    {
                        randomInt = ran.Next(10, 100);
                        Console.WriteLine($@"Setting protecedObject1 value to {randomInt}");
                        protecedObject1.Value = randomInt;
                    }
                }
            }
        }
    }
}
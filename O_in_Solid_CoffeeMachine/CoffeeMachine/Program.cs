using System;

namespace CoffeeMachine
{
    class Program
    {
        //TODO: 1. Hæld vand i en beholder alt afhængig af hvor mange kopper man ønsker 
        //TODO: 2. indsæt filter
        //TODO: 3. hæld kaffebønner i maskinen
        //TODO: 4. Tænd kaffemaskinen

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            CoffeeMachine coffee = new CoffeeMachine(2,"Coffee", "CoffeeBeans", 2);

            coffee.TurnMachineOn();

            coffee.TurnMachineOff();

        }
    }
}

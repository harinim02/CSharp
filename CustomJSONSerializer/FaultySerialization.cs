using Newtonsoft.Json;
using System;

namespace MySampleCode.CustomSerialization
{
    class FaultySerialization
    {
        static void Main(string[] args)
        {
            var car = new Car()
            {
                Company = "Ford",
                ModelNo = "ABC123",
                WheelType = new ChromeWheel()
                {
                    Material = "Rubber",
                    HasChromePlating = false
                }
            };

            string serializedCar = JsonConvert.SerializeObject(car);
            Car deserializedCar = JsonConvert.DeserializeObject<Car>(serializedCar);
            Console.WriteLine(serializedCar);
            Console.WriteLine("Type of WheelType " + deserializedCar.WheelType.GetType());
            var chromeWheel = deserializedCar.WheelType as ChromeWheel;
            Console.WriteLine("HasChromePlating " + chromeWheel?.HasChromePlating);
        }
    }
}

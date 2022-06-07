using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleCode.CustomSerialization
{
    class SecureSerialization
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
            string serializedCar = JsonConvert.SerializeObject(car, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.All,
                SerializationBinder = new MySerializationBinder(PayloadValidator.GetAllowedPayloadTypes())
            });
            Car deserializedCar = JsonConvert.DeserializeObject<Car>(serializedCar, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new MySerializationBinder(PayloadValidator.GetAllowedPayloadTypes())
            });
            Console.WriteLine(serializedCar);
            Console.WriteLine("Type of WheelType " + deserializedCar.WheelType.GetType());
            var chromeWheel = deserializedCar.WheelType as ChromeWheel;
            Console.WriteLine("HasChromePlating " + chromeWheel?.HasChromePlating);
        }
    }
}

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;

namespace MySampleCode
{
    public class Car
    {
        public Wheel WheelType;
        public string ModelNo;
        public string Company;
    }
    public class Wheel
    {
        public string Material;
    }
    public class AlloyWheel : Wheel
    {
        public string FrictionLevel;
    }
    public class ChromeWheel : Wheel
    {
        public bool HasChromePlating;
    }
    public class Program
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
                SerializationBinder = new MySerializationBinder(GetAllowedPayloadTypes())
            });
            Car deserializedCar = JsonConvert.DeserializeObject<Car>(serializedCar, new JsonSerializerSettings()
            {
                TypeNameHandling = TypeNameHandling.Auto,
                SerializationBinder = new MySerializationBinder(GetAllowedPayloadTypes())
            });
            Console.WriteLine(serializedCar);
            Console.WriteLine("Type of WheelType " + deserializedCar.WheelType.GetType());
            var chromeWheel = deserializedCar.WheelType as ChromeWheel;
            Console.WriteLine("HasChromePlating " + chromeWheel?.HasChromePlating);
            Console.ReadLine();
        }

        static ImmutableList<Type> GetAllowedPayloadTypes() =>
            ImmutableList.ToImmutableList(new List<Type>() { 
                typeof(Car), typeof(Wheel), typeof(AlloyWheel), typeof(ChromeWheel) });
    }

    public class MySerializationBinder : ISerializationBinder
    {
        private readonly IList<Type> _knownTypes;
        public MySerializationBinder(IList<Type> knowTypes)
        {
            _knownTypes = knowTypes;
        }
        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = serializedType.Assembly.FullName;
            typeName = serializedType.FullName;
        }
        public Type BindToType(string assemblyName, string typeName)
        {
            return _knownTypes?.SingleOrDefault(t => t.Name == typeName || t.FullName == typeName);
        }
    }
}

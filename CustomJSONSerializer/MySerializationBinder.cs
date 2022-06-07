using Newtonsoft.Json.Serialization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleCode.CustomSerialization
{
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

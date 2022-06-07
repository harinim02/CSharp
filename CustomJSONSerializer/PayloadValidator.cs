using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySampleCode.CustomSerialization
{
    class PayloadValidator
    {
        public static ImmutableList<Type> GetAllowedPayloadTypes() =>
        ImmutableList.ToImmutableList(typeof(PayloadRequest)
        .Assembly
        .GetTypes()
        .Where(t => t.IsClass && t.Namespace ==
        "MyAssemble.Models"));
    }
}

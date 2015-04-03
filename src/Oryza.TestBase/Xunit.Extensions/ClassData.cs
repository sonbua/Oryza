using System;
using System.Collections.Generic;
using System.Reflection;
using Xunit.Sdk;

namespace Oryza.TestBase.Xunit.Extensions
{
    public class ClassData : DataAttribute
    {
        private readonly Type _type;

        public ClassData(Type type)
        {
            if (!typeof (IEnumerable<object[]>).IsAssignableFrom(type))
            {
                throw new Exception("Type mismatch. Must implement IEnumerable<object[]>.");
            }

            _type = type;
        }

        public override IEnumerable<object[]> GetData(MethodInfo testMethod)
        {
            return (IEnumerable<object[]>) Activator.CreateInstance(_type);
        }
    }
}
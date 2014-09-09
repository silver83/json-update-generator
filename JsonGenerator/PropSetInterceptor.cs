using System.Collections;
using System.Linq;
using Castle.DynamicProxy;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JsonUpdateGenerator.JsonGenerator
{
    internal class PropSetInterceptor : IInterceptor
    {
        private JObject _backingObj = new JObject();

        public void Intercept(IInvocation invocation)
        {
            if (invocation.Method.Name.StartsWith("set_"))
            {
                var name = invocation.Method.Name.Substring(4); //set_(...)
                var value = invocation.Arguments[0];

                var attrib = invocation.Method.DeclaringType.GetProperty(name).GetCustomAttributes(false).OfType<JsonPropertyAttribute>().FirstOrDefault();
                if (attrib != null && !string.IsNullOrWhiteSpace(attrib.PropertyName))
                    name = attrib.PropertyName;

                var type = value.GetType();
                if (type.IsPrimitive)
                    _backingObj[name] = new JObject(value);
                else if (typeof(IEnumerable).IsAssignableFrom(type) && !typeof(IDictionary).IsAssignableFrom(type))
                    _backingObj[name] = JArray.FromObject(value);
                else
                    _backingObj[name] = JObject.FromObject(value);
            }
            else if (invocation.Method.Name == "GetJson")
            {
                invocation.ReturnValue = _backingObj;
            }
        }
    }
}

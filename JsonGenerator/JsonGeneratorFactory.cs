using Castle.DynamicProxy;

namespace JsonUpdateGenerator.JsonGenerator
{
    public class JsonGeneratorFactory
    {
        public T CreateJsonGenerator<T>() where T : class
        {
            var generator = new ProxyGenerator();
            var propInterceptor = new PropSetInterceptor();
            var proxy = generator.CreateInterfaceProxyWithoutTarget<T>(propInterceptor);
            return proxy;
        }
    }
}

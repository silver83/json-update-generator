using Castle.DynamicProxy;
using JsonUpdateGenerator.JsonGenerator;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace JsonUpdateGenerator
{
    public class Bla
    {
        public string BladiBla { get; set; }
    }

    public interface IPoolMember : IJsonGeneratable
    {
        string Session { get; set; }
        string State { get; set; }
        int MyInt { get; set; }
        [JsonProperty(PropertyName = "myArray")]
        List<string> MyArray { get; set; }
        Dictionary<string, int> MyDict { get; set; }
        Dictionary<string, Bla> MyDict2 { get; set; } 
    }

    class Program
    {
        static void Main(string[] args)
        {
            var generatorFactory = new JsonGeneratorFactory();
            var generator = generatorFactory.CreateJsonGenerator<IPoolMember>();
            generator.MyArray = new List<string>() { "hi", "hello" };
            generator.MyDict = new Dictionary<string, int>() { { "a", 1 }, { "c", 3 } };
            generator.MyDict2 = new Dictionary<string, Bla>() { { "stam", new Bla() { BladiBla = "Ido" } } };

            Console.WriteLine(generator.GetJson());
            Console.ReadLine();

        }
    }
}

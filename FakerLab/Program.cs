using FakerGeneratorInterfases;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace FakerLab
{
    class Program
    {
        class Generator : ISimpleGenerator
        {
            private static readonly string[] RAND_STRING = { "It", "starts", "with", "one", "thing" ," ",
                                                        "Memories", "consume", "like", "opening", "the", "wound", " ",
                                                        "Sometimes", "I", "need", "to", "remember", "just", "to", "breath", " ",
                                                        "When", "this", "began", "I", "had", "nothing", "to", "say", " ",
                                                        "I", "don't", "know", "who", "to", "trust" };
            Random rnd = new Random();

            public object Generate()
            {

                int index;
                string temp = "";
                for (int i = 0; i < rnd.Next(RAND_STRING.Length); i++)
                {
                    index = rnd.Next(RAND_STRING.Length);
                    temp += RAND_STRING[index];
                }
                return temp;

            }
        }
        class GeneratorInt : ISimpleGenerator
        {
           
            Random rnd = new Random();

            public object Generate()
            {

                return rnd.Next() % 5;

            }
        }

        static void Main(string[] args)
        {
            Faker faker = new Faker();
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Foo, int, GeneratorInt>(a => a.b);

            
            Foo foo = faker.Create<Foo>();
            

            
            Serializer serializer = new Serializer();


            Console.Out.Write(serializer.Serialize(foo));
            Console.ReadKey();
        }
    }
}

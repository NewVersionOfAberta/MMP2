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
            public object Generate()
            {
                return 2;
            }
        }

        static void Main(string[] args)
        {
            Faker faker = new Faker();
            FakerConfig fakerConfig = new FakerConfig();
            fakerConfig.Add<Foo, int, Generator>(a => a.b);

            
            Foo foo = faker.Create<Foo>();
            

            
            Serializer serializer = new Serializer();


            Console.Out.Write(serializer.Serialize(foo));
            Console.ReadKey();
        }
    }
}

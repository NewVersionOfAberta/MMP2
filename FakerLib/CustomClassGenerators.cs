using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    class FooClassGenerator : ISimpleGenerator
    {
        Faker faker = new Faker();
        Boolean isVisited = false;

        object ISimpleGenerator.Generate()
        {
            Foo result = null;
           if (!isVisited)
            {
                isVisited = true;
                result = faker.Create<Foo>();
                isVisited = false;
            }
            return result;
        }
    }
    class BarClassGenerator : ISimpleGenerator
    {
        Faker faker = new Faker();
        Boolean isVisited = false;

        object ISimpleGenerator.Generate()
        {
            Bar result = null;
            if (!isVisited)
            {
                isVisited = true;
                result = faker.Create<Bar>();
                isVisited = false;
            }
            return result;
        }
    }
}

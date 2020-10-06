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

        public object Generate()
        {
            Foo result = null;
            if (!isVisited)
            {
                isVisited = true;
                result = faker.Create<Foo>();
            }

            isVisited = false;
            return result;
        }
    }
}

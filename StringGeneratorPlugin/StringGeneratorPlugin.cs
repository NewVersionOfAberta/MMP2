using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    class StringGeneratorPlugin : IPlugin
    {
        public Type GetGeneratorType()
        {
            return typeof(String);
        }

        public ICollectionGenerator GetSimpleGenerator()
        {
            return new StringGenerator();
        }
    }
}

using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    public class BooleanPlugin : IPlugin
    {
        public Type GetGeneratorType()
        {
            return typeof(Boolean);
        }

        public ISimpleGenerator GetSimpleGenerator()
        {
            return new BooleanGenerator();
        }
    }
}

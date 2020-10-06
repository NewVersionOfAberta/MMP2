using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Text;

namespace Plugins
{
    class BooleanGenerator : ISimpleGenerator
    {
        Random rnd = new Random();
        public object Generate()
        {
           

            return ((rnd.Next() % 2) == 0);
                    }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace FakerGeneratorInterfases
{
    public interface IPlugin
    {
        Type GetGeneratorType();

        ISimpleGenerator GetSimpleGenerator();
    }
}

using System;

namespace FakerGeneratorInterfases
{
    public interface ICollectionGenerator
    {
        Object Generate(Type type, Type collectionType);
    }
    
}

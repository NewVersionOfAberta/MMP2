using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    class ListGenerator : ICollectionGenerator
    {
        private const int MODULE = 100; 

        public object Generate(Type type, Type collectionType)
        {
            var collection = System.Activator.CreateInstance(collectionType);
            var rnd = new Random();

            ISimpleGenerator simpleGenerator;

            if (Faker.simpleGenerators.TryGetValue(type, out simpleGenerator))
            {
                //var values = new object[rnd.Next() % MODULE];
                var value = new object[1];
                MethodInfo methodAdd;
                
                if ((methodAdd = collectionType.GetMethod("Add")) != null)
                {
                    for (int i = 0; i < rnd.Next() % MODULE; i++)
                    {
                        value[0] = simpleGenerator.Generate();
                        methodAdd.Invoke(collection, value);
                    }
                }
                
                
            }
            return collection;
            //var ts = new List<T>();
            //
            //ISimpleGenerator simpleGenerator;
            //Faker.simpleGenerators.TryGetValue(typeof(T), out simpleGenerator);
            //for (int i = 0; i < rnd.Next() % module; i++)
            //{
            //    ts.Add((T)simpleGenerator.Generate());
            //}
            //return ts;
        }

        //public object GenerateA(Type pe)
        //{
            
        //}
    }
}

using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;


namespace FakerLab
{
    public class Faker
    {

        private static readonly string pluginPath = System.IO.Path.Combine(
                                                Directory.GetCurrentDirectory(),
                                                "Plugin");
        private static List<IPlugin> plugins = new List<IPlugin>();

        public static Dictionary<Type, ISimpleGenerator> simpleGenerators;
        public static Dictionary<Type, ICollectionGenerator> collectionGenerators;
        public static Dictionary<(string, Type), ISimpleGenerator> customGenerators;

        static private void RefreshPlugins()
        {
            plugins.Clear();

            DirectoryInfo pluginDirectory = new DirectoryInfo(pluginPath);
            if (!pluginDirectory.Exists)
                pluginDirectory.Create();

      
            var pluginFiles = Directory.GetFiles(pluginPath, "*.dll");
            foreach (var file in pluginFiles)
            {

                Assembly asm = Assembly.LoadFrom(file);
                var types = asm.GetTypes().
                                Where(t => t.GetInterfaces().
                                Where(i => i.FullName == typeof(IPlugin).FullName).Any());

                foreach (var type in types)
                {
                    var plugin = asm.CreateInstance(type.FullName) as IPlugin;
                    plugins.Add(plugin);
                }
            }
        }
        static Faker()
        {
            simpleGenerators = new Dictionary<Type, ISimpleGenerator>();
            simpleGenerators.Add(typeof(int), new IntGenerator());
            simpleGenerators.Add(typeof(long), new LongGenerator());
            simpleGenerators.Add(typeof(double), new DoubleGenerator());
            simpleGenerators.Add(typeof(string), new StringGenerator());
            simpleGenerators.Add(typeof(byte), new ByteGenerator());

            simpleGenerators.Add(typeof(Foo), new FooClassGenerator());
            simpleGenerators.Add(typeof(Bar), new BarClassGenerator());

            collectionGenerators = new Dictionary<Type, ICollectionGenerator>();
            collectionGenerators.Add(typeof(ICollection<>), new ListGenerator());

            customGenerators = new Dictionary<(string, Type), ISimpleGenerator>();

            RefreshPlugins();

            foreach(IPlugin plugin in plugins)
            {
                if (simpleGenerators.ContainsKey(plugin.GetGeneratorType()))
                {
                    simpleGenerators.Remove(plugin.GetGeneratorType());
                }
                simpleGenerators.Add(plugin.GetGeneratorType(), plugin.GetSimpleGenerator());
              
            }
        }


        private ConstructorInfo GetBestFitConstructor(ConstructorInfo[] constructors)
        {
            ConstructorInfo currentBest = constructors[0];
            Boolean isPublic = currentBest.IsPublic;
            foreach (ConstructorInfo constructor in constructors)
            {
                if ((currentBest.GetParameters().Length < constructor.GetParameters().Length) && constructor.IsPublic)
                {
                    currentBest = constructor;
                    isPublic = true;
                }
            }
            return isPublic ? currentBest : null;
        }

        private object[] MakeParametersArray(ParameterInfo[] paramInfos, Type type)
        {
            ISimpleGenerator simpleGenerator;
            ICollectionGenerator collectionGenerator;
            

            var paramArray = new Object[paramInfos.Length];
            Type paramType;
            int i = 0;

            foreach (var paramInfo in paramInfos)
            {
                paramType = paramInfo.ParameterType;
                if (customGenerators.TryGetValue((paramInfo.Name, type), out simpleGenerator)){
                    paramArray[i] = simpleGenerator.Generate();

                }
                else if (simpleGenerators.TryGetValue(paramType, out simpleGenerator))
                {
                    paramArray[i] = simpleGenerator.Generate();
                }
                else if (paramType.GetInterface("ICollection") != null && collectionGenerators.TryGetValue(typeof(ICollection<>), 
                                                                                            out collectionGenerator))
                {
                    paramArray[i] = collectionGenerator.Generate(paramType.GetGenericArguments()[0], paramType);
                }
                else
                {
                    paramArray[i] = null;
                }
                i++;
            }
            return paramArray;
        }

        private void SetPublicFields(FieldInfo[] fields, Object instance)
        {
            ISimpleGenerator simpleGenerator;
            ICollectionGenerator collectionGenerator;

            foreach (FieldInfo fieldInfo in fields)
            {
                if (customGenerators.TryGetValue((fieldInfo.Name, instance.GetType()), out simpleGenerator))
                {
                    fieldInfo.SetValue(instance, simpleGenerator.Generate());

                }
                else if (simpleGenerators.TryGetValue(fieldInfo.FieldType, out simpleGenerator))
                {
                    fieldInfo.SetValue(instance, simpleGenerator.Generate());
                }
                else if (fieldInfo.FieldType.GetInterface("ICollection") != null
                    && collectionGenerators.TryGetValue(typeof(ICollection<>), out collectionGenerator))
                {
                    fieldInfo.SetValue(instance, collectionGenerator.Generate(fieldInfo.FieldType.GetGenericArguments()[0],
                                                                                    fieldInfo.FieldType));
                }

            }
        }

        //private void SetPrivateProperties(PropertyInfo[] properties, Object instance)
        //{
        //    MethodInfo methodInfo;
        //    ISimpleGenerator simpleGenerator;
        //    ICollectionGenerator collectionGenerator;
        //    var value = new object[1];

        //    foreach (PropertyInfo property in properties)
        //    {
        //        if ((methodInfo = property.GetSetMethod()) != null)
        //        {
        //            if (simpleGenerators.TryGetValue(property.PropertyType, out simpleGenerator))
        //            {
        //                value[0] = simpleGenerator.Generate();
        //                methodInfo.Invoke(instance, value);
        //            }
        //            else if (property.PropertyType.GetInterface("ICollection") != null
        //                && collectionGenerators.TryGetValue(typeof(ICollection<>), out collectionGenerator))
        //            {
        //                value[0] = collectionGenerator.Generate(property.PropertyType.GetGenericArguments()[0],
        //                                                                                property.PropertyType);
        //                methodInfo.Invoke(instance, value);
        //            }
        //        }

        //    }
        //}


        public T Create<T>()
        {
            var classType = typeof(T);
 
            ConstructorInfo constructor;

            ConstructorInfo[] cInf = classType.GetConstructors();
            if ((constructor = GetBestFitConstructor(cInf)) == null)
                return default(T);
            ParameterInfo[] paramInfos = constructor.GetParameters();

            var paramArr = MakeParametersArray(paramInfos, classType);

            var classInstance = constructor.Invoke(paramArr);

            var publicFields = classType.GetFields(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Static);
            SetPublicFields(publicFields, classInstance);            
            return (T)classInstance;
        }
    }
}

using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    class ValueGenerator
    {
        Random rnd = new Random();

        delegate Object Generate();
        private Dictionary<Type, Generate> generators = new Dictionary<Type, Generate>();

        private const int SHIFT_LEFT = 8;
        private static readonly string[] RAND_STRING = { "It", "starts", "with", "one", "thing",
                                                        "Memories", "consume", "like", "opening", "the", "wound",
                                                        "Sometimes", "I", "need", "to", "remember", "just", "to", "breath",
                                                        "When", "this", "began", "I", "had", "nothing", "to", "say",
                                                        "I", "don't", "know", "who", "to", "trust" };

        public ValueGenerator()
        {
            generators.Add(typeof(int), GenerateInt);
            generators.Add(typeof(double), GenerateDouble);
            generators.Add(typeof(string), GenerateString);
            generators.Add(typeof(long), GenerateLong);
            generators.Add(typeof(byte), GenerateByte);
            generators.Add(typeof(List<>), GenerateList);
        }

        public Object GenerateInt()
        {
            return rnd.Next();
        }

        public Object GenerateDouble()
        {
            return rnd.NextDouble();
        }

        public Object GenerateLong()
        {
            long l = (rnd.Next() << SHIFT_LEFT) + rnd.Next();
            return l;
        }

        public Object GenerateString()
        {
            int index;
            string temp = "";
            for (int i = 0; i < rnd.Next(RAND_STRING.Length); i++){
                index = rnd.Next(RAND_STRING.Length);
                temp += RAND_STRING[index];
            }
            return temp;
        }

        public Object GenerateByte()
        {
            return rnd.Next() % (Byte.MaxValue + 1);
        }

        public Object GenerateList<T>()
        {
            List<T> list = new List<T>();
            if (generators.ContainsKey(typeof(T))) {
                Generate generate;
                generators.TryGetValue(typeof(T), out generate);
                for (int i = 0; i < rnd.Next() % 100 + 1; i++)
                {
                    list.Add((T)generate());
                }
            }
            return list;
        }


    }
}

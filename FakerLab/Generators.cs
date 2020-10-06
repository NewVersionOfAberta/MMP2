using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    static class RandomNumber
    {
        static public Random rnd = new Random();
        
    }
    class DoubleGenerator : ISimpleGenerator
    {
        public object Generate()
        {
            
            return RandomNumber.rnd.NextDouble();
        }
    }
    class IntGenerator : ISimpleGenerator
    {
        public object Generate()
        {
            
            return RandomNumber.rnd.Next();
        }
    }

    class ByteGenerator : ISimpleGenerator
    {
        public object Generate()
        {
            
            return (Byte)RandomNumber.rnd.Next();
        }
    }

    class StringGenerator : ISimpleGenerator
    {
        private static readonly string[] RAND_STRING = { "It", "starts", "with", "one", "thing" ," ",
                                                        "Memories", "consume", "like", "opening", "the", "wound", " ",
                                                        "Sometimes", "I", "need", "to", "remember", "just", "to", "breath", " ",
                                                        "When", "this", "began", "I", "had", "nothing", "to", "say", " ",
                                                        "I", "don't", "know", "who", "to", "trust" };
        public object Generate()
        {
            
            int index;
            string temp = "";
            for (int i = 0; i < RandomNumber.rnd.Next(RAND_STRING.Length); i++)
            {
                index = RandomNumber.rnd.Next(RAND_STRING.Length);
                temp += RAND_STRING[index];
            }
            return temp;

        }
    }

    class LongGenerator : ISimpleGenerator
    {
        private const int SHIFT_LEFT = 8;        
        
        public object Generate()
        {
            
            long l = (RandomNumber.rnd.Next() << SHIFT_LEFT) + RandomNumber.rnd.Next();
            return l;
        }
    }

   

    
}


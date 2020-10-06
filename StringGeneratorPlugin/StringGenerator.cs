using FakerGeneratorInterfases;
using System;
using System.Linq;
using System.Text;

namespace Plugins
{
    public class StringGenerator : ICollectionGenerator
    {
        const String ALPHABET = "ABCDEFGHIGKLMNOPQRSTUVWXYZ";
        const int MAX_SIZE = 100;

        Random rnd = new Random();

        public object Generate()
        {
            var sb = new StringBuilder();
            

            for (int i = 0; i < rnd.Next() % MAX_SIZE; i++)
                sb.Append(ALPHABET.ElementAt(rnd.Next() % ALPHABET.Length));

            return sb.ToString();
        }

    }
}

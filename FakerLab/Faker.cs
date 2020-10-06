using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    class Faker
    {
        
        
        ValueGenerator valueGenerator;

        public Faker()
        {
            valueGenerator = new ValueGenerator();
            

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



        public T Create<T>()
        {
            var classType = typeof(T);
            ConstructorInfo constructor;
            ConstructorInfo[] cInf = classType.GetConstructors();
            if ((constructor = GetBestFitConstructor(cInf)) == null)
                return default(T);
            ParameterInfo[] paramInfo = constructor.GetParameters();
            var t = paramInfo[0].ParameterType;
            return default(T);
        }
    }
}

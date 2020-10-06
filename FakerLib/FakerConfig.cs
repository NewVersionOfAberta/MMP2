using FakerGeneratorInterfases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    public class FakerConfig
    {
        public void Add<ClassType, ResultType, GeneratorType>(Expression<Func<ClassType, ResultType>> expression){
            if (expression.Body.NodeType == ExpressionType.MemberAccess)
            {
                MemberExpression memberExpression = (MemberExpression)expression.Body;

                var generator = (ISimpleGenerator)Activator.CreateInstance(typeof(GeneratorType));
                Faker.customGenerators.Add((memberExpression.Member.Name, typeof(ClassType)), generator);
            }
        }
    }
}

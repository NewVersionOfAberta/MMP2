using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FakerLab
{
    public class Foo
    {

        public Foo(List<string> vs, Boolean isOk)
        {
            this.vs = vs;
            this.isOk = isOk;
        }

        private Boolean isOk;
        private List<string> vs;
        public int b;
        public Bar bar;

    }
}

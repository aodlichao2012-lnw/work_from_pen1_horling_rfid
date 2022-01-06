using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tcp_test
{
    class cl_instance
    {
        public static cl_instance instance = null;
        public static cl_instance Instance
        {
            get
            {
                if(instance == null)
                {
                    instance = new cl_instance();
                }
                return instance;
            }
        }

        public int b { get; set;}
        public int c { get; set;}
    }
}

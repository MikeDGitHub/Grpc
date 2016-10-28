using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Greeter
{ 
    //贴上方法标签  
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
    public class MethodAttribute: Attribute
    {
        public MethodAttribute()
        {
            Console.WriteLine("MethodAttribute");
        }
    }
}

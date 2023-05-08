using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_vache.StringExtension
{
    public static  class StringExt
    {
        public static void dump(this string str, ConsoleColor color = ConsoleColor.White)
        {
            if (str != null)
            {
                Console.ForegroundColor = color;
                Console.WriteLine(str);
            }

        }
    }
}

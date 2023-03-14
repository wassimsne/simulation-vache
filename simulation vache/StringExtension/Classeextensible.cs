using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_vache.StringExtension
{
    public static class Classeextensible
    {
       
        // la clsse extenible doit commencer par this au niveau de parametre apres la classe qu'on veut l'extensibiliser 

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

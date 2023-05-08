using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace simulation_vache
{
    public enum TypeMesure { Cardiaque,Temperature}
    public class Mesure
    {
        public int idMesure;
        public int idVache;
        public DateTime time;
        public  TypeMesure Type;
        public float valeur;
        public override string ToString()
        {
            return "Id Mesure=" + idMesure + "  Id Vache= " + idVache + "  time=" + time + "  Type=" + Type + " Valeur=" + valeur;
        }
    }

}

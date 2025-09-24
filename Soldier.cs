using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Soldier : Unit
    {
        protected Soldier(string name, int dange, int health, int speed, int price) : base(name, dange, health, speed, price)
        {

        }
        internal static Soldier Create(string name, int dange, int health, int speed, int priece)
        {
            return new Soldier(name, dange, health, speed, priece);
        }
    }
}

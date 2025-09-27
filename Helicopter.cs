using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Helicopter : Unit
    {
        protected Helicopter(string name, int dange, int health, int speed, int price) : base(name, dange, health, speed, price)
        {

        }
        internal static Helicopter Create(string name, int dange, int health, int speed, int priece)
        {
            return new Helicopter(name, dange, health, speed, priece);
        }
        public override bool CanAttack(Unit target)
        {
            return target is Tank;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Tank : Unit
    {
        protected Tank(string name, int dange, int health, int speed, int price) : base(name, dange, health, speed, price)
        {

        }
        internal static Tank Create(string name, int dange, int health, int speed, int priece)
        {
            return new Tank(name, dange, health, speed, priece);
        }
        public override bool CanAttack(Unit target)
        {
            return target is Soldier;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class DefenseStructure : Building
    {
        private int dange;

        protected DefenseStructure(string name, int price, int health, int dange) : base(name, price, health)
        {
            this.dange = dange;
        }
        public virtual int Attack(int newDange)
        {
            newDange = dange;
            return dange;
        }
        public override string GetInfo()
        {
            return base.GetInfo() + $" Daño: {dange}";
        }
        internal static DefenseStructure Create(string name, int price, int health, int dange)
        {
            return new DefenseStructure(name, price, health, dange);
        }
    }
}

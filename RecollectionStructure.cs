using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Promedio1Examen1
{
    internal class RecollectionStructure :  Building
    {
        private int income;

        protected RecollectionStructure(string name, int price, int health, int income) : base(name, price, health)
        {
            this.income = income;
        }
        public virtual int Attack(int newIncome)
        {
            newIncome = income;
            return income;
        }
        public override string GetInfo()
        {
            return base.GetInfo() + $" Recoleccion: {income}";
        }
        internal static RecollectionStructure Create(string name, int price, int health, int income)
        {
            return new RecollectionStructure(name, price, health, income);
        }
        public override int CollectIncome()
        {
            return income;
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class MaintenanceStructure : Building
    {

        protected MaintenanceStructure(string name, int price, int health) : base(name, price, health)
        {
        }
        internal static MaintenanceStructure Create(string name, int price, int health)
        {
            return new MaintenanceStructure(name, price, health);
        }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Building
    {
        internal class Structure
        {
            protected string name { get; }
            protected int price { get; }
            protected int health { get; set; }

            protected Structure(string name, int price, int health)
            {
                this.name = name;
                this.price = price;
                this.health = health;
            }
            protected virtual void TakeDamage(int damage)
            {
                health -= damage;
                if (health < 0) health = 0;
            }
            protected virtual string GetInfo()
            {
                return $"{name} Cuesta: {price} Salud: {health}";
            }
        }
    }

}

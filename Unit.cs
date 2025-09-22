using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Unit
    {
        protected string name { get; }
        protected int dange { get; }
        protected int health { get; set; }
        protected int speed { get; }
        protected int price { get;  }

        protected Unit(string name, int dange, int health, int speed, int price)
        {
            this.name = name;
            this.dange = dange;
            this.health = health;
            this.speed = speed;
            this.price = price;
        }
        protected virtual void TakeDamage(int damage)
        {
            health -= damage;
            if (health < 0) health = 0;
        }
        protected virtual string GetInfo()
        {
            return $"{name} Cuesta: {price} Salud: {health} Daño: {dange} Velocidad: {speed}";
        }
    }
}

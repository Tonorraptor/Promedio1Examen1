using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Game
    {
        public void Start()
        {
            int option = -1;
            while (option != 2)
            {
                Console.WriteLine("Menu Principal");
                Console.WriteLine("1 Jugar");
                Console.WriteLine("2 Salir");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        break;
                    case 2:
                        Console.WriteLine("Gracias por jugar");
                        break;
                }
            }
        }
    }
}

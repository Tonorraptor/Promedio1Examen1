using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Game
    {
        private AssetsList lists = new AssetsList();
        private List<Node> nodes = new List<Node>();

        public void Start()
        {
            lists.AddStructures();
            lists.AddUnits();
            lists.AddEnemys();

            nodes.Add(new Node("Tu Base", isPlayerBase: true));
            nodes.Add(new Node("Nudo 1"));
            nodes.Add(new Node("Nudo 2"));
            nodes.Add(new Node("Nudo 3"));
            nodes.Add(new Node("Nudo 4"));
            nodes.Add(new Node("Nudo 5"));
            nodes.Add(new Node("Base enemiga", isEnemyBase: true));


            int option = -1;
            int gameOption = -1;
            while (option != 2)
            {
                
                Console.WriteLine("Menu Principal");
                Console.WriteLine("1 Jugar");
                Console.WriteLine("2 Salir");
                option = int.Parse(Console.ReadLine());
                switch (option)
                {
                    case 1:
                        while (gameOption != 7)
                        {
                            Console.WriteLine("Partida");
                            Console.WriteLine("1 Ver Edificios");
                            Console.WriteLine("2 Ver Unidades");
                            Console.WriteLine("3 Salir");
                            gameOption = int.Parse(Console.ReadLine());
                            switch (gameOption)
                            {
                                case 1:
                                    ShowStructureStatus();
                                    break;
                                case 2:
                                    ShowUnitsStatus();
                                    break;
                            }
                        }
                        break;
                    case 2:
                        Console.WriteLine("Gracias por jugar");
                        break;
                }
            }
        }
        private void ShowStructureStatus()
        {
            Console.WriteLine("Estructuras");
            foreach(var node in nodes)
            {
                Console.WriteLine(node.GetStructureStatus());
            }
        }
        private void ShowUnitsStatus()
        {
            Console.WriteLine("Unidades");
            foreach (var node in nodes)
            {
                Console.WriteLine(node.GetUnitStatus());
            }
        }
    }
}

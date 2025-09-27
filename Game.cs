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
        private int cash = 500;
        private int maxStructures = 3;
        private int count = 0;

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
                            Console.WriteLine($"Monedas: {cash}");
                            Console.WriteLine("1 Ver Edificios");
                            Console.WriteLine("2 Ver Unidades");
                            Console.WriteLine("3 Construir edificio");
                            Console.WriteLine("4 Salir");
                            gameOption = int.Parse(Console.ReadLine());
                            switch (gameOption)
                            {
                                case 1:
                                    ShowStructureStatus();
                                    break;
                                case 2:
                                    ShowUnitsStatus();
                                    break;
                                case 3:
                                    CreateBuilding();
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
            Console.WriteLine($"Limite de construccion: {maxStructures}");
            foreach (var node in nodes)
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
        private void CreateBuilding()
        {
            if (count >= maxStructures)
            {
                Console.WriteLine("No puedes construir mas edificios");
                return;
            }

            Console.WriteLine("Selecciona un edificio");
            Console.WriteLine("1 Estructura de recoleccion");
            Console.WriteLine("2 Estructura de mantenimiento");
            Console.WriteLine("3 Estructura de defensa");
            int option;
            option = int.Parse(Console.ReadLine());
            Building newBuilding = null;
            switch (option)
            {
                case 1:
                    newBuilding = RecollectionStructure.Create("Estructura de Recoleccion", 100, 600, 150);
                    break;
                case 2:
                    newBuilding = MaintenanceStructure.Create("Estructura de mantenimiento", 175, 1200);
                    break;
                case 3:
                    newBuilding = DefenseStructure.Create("Estructura de defensa", 175, 1020, 5);
                    break;
                default:
                    Console.WriteLine("Comando incorrecto");
                    break;
            }
            int price = newBuilding.GetPriece();
            if (cash < price)
            {
                Console.WriteLine("No tienes monedas suficiente");
                return;
            }
            
            
            cash -= price;

            nodes[0].AddStructure(newBuilding);
            count++;
            Console.WriteLine($"- {newBuilding.GetPriece()}");

            if (newBuilding is MaintenanceStructure)
            {
                maxStructures += 3;
                Console.WriteLine("Se aumento la capacidad maxima de construccion");
            }

            Console.WriteLine($"{newBuilding.GetName()} fue creado");
        }
    }
}

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
        private int Turn = 0;

        public void Start()
        {
            lists.AddStructures();
            lists.AddUnits();
            lists.AddEnemys();

            nodes.Add(new Node("Tu Base", isPlayerBase: true));
            nodes[0].Conquer();
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
                            Console.WriteLine("4 Crear unidad");
                            Console.WriteLine("5 Ver mapa");
                            Console.WriteLine("6 Siguiente turno");
                            Console.WriteLine("7 Salir");
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
                                case 4:
                                    CreateUnit();
                                    break;
                                case 5:
                                    ShowMap();
                                    break;
                                case 6:
                                    EndTurn();
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
            int option;
            int selectNode = -1;
            List<Node> availableNodes = new List<Node>();

            if (count >= maxStructures)
            {
                Console.WriteLine("No puedes construir más edificios.");
                return;
            }

            Console.WriteLine("Selecciona un edificio");
            Console.WriteLine("1. Estructura de recolección");
            Console.WriteLine("2. Estructura de mantenimiento");
            Console.WriteLine("3. Estructura de defensa");
            option = int.Parse(Console.ReadLine());

            Building newBuilding = null;
            switch (option)
            {
                case 1:
                    newBuilding = RecollectionStructure.Create("Estructura de Recolección", 100, 600, 150);
                    break;
                case 2:
                    newBuilding = MaintenanceStructure.Create("Estructura de mantenimiento", 175, 1200);
                    break;
                case 3:
                    newBuilding = DefenseStructure.Create("Estructura de defensa", 175, 1020, 5);
                    break;
                default:
                    Console.WriteLine("Comando incorrecto");
                    return;
            }

            int price = newBuilding.GetPriece();
            if (cash < price)
            {
                Console.WriteLine($"No tienes monedas suficientes (necesitas {price}).");
                return;
            }

            availableNodes = GetAvailableNodes();


            Console.WriteLine("Selecciona el nodo donde quieres construir:");
            for (int i = 0; i < availableNodes.Count; i++)
            {
                Console.WriteLine($"{i}. {availableNodes[i].GetName()}");
            }

            selectNode = int.Parse(Console.ReadLine());
            if (selectNode < 0 || selectNode >= availableNodes.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            Node targetNode = availableNodes[selectNode];

            if (!targetNode.IsConquered())
            {
                targetNode.Conquer();
                Console.WriteLine($"{targetNode.GetName()} ha sido conquistado.");
            }
            cash -= price;
            targetNode.AddStructure(newBuilding);
            count++;

            Console.WriteLine($"- {price} monedas gastadas.");
            Console.WriteLine($"{newBuilding.GetName()} fue creado en {targetNode.GetName()}");

            if (newBuilding is MaintenanceStructure)
            {
                maxStructures += 3;
                Console.WriteLine("Se aumentó la capacidad máxima de construcción.");
            }
        }
        private void CreateUnit()
        {
            List<Node> availableNodes = new List<Node>();
            int option;
            int nodeSelect=-1;
            foreach (var n in nodes)
            {
                if(n.IsConquered() && n.GetMaintenanceStructures().Count > 0)
                {
                    availableNodes.Add(n);
                }
            }

            if (availableNodes.Count == 0)
            {
                Console.WriteLine("No tienes estructuras de mantenimiento para crear unidades.");
                return;
            }

            Console.WriteLine("Seleccione estructura de mantenimiento");
            for (int i = 0; i < availableNodes.Count; i++)
            {
                var node = availableNodes[i];
                var maints = node.GetMaintenanceStructures();
                Console.WriteLine($"{i}. {node.GetName()} ({maints.Count} mantenimiento)");
            }

            nodeSelect = int.Parse(Console.ReadLine());
            if (nodeSelect < 0 || nodeSelect >= availableNodes.Count)
            {
                Console.WriteLine("Selección inválida.");
                return;
            }

            Node targetNode = availableNodes[nodeSelect];

            Console.WriteLine("Crear Unidad");
            Console.WriteLine("1. Soldado");
            Console.WriteLine("2. Tanque");
            Console.WriteLine("3. Helicóptero");
            option = int.Parse(Console.ReadLine());

            Unit newUnit = null;
            switch (option)
            {
                case 1:
                    newUnit = Soldier.Create("Soldado", 13, 70, 1, 80);
                    break;
                case 2:
                    newUnit = Tank.Create("Tanque", 24, 100, 2, 105);
                    break;
                case 3:
                    newUnit = Helicopter.Create("Helicóptero", 75, 120, 3, 295);
                    break;
                default:
                    Console.WriteLine("Opción inválida.");
                    return;
            }

            int price = newUnit.GetPrice();
            if (cash < price)
            {
                Console.WriteLine($"Dinero insuficiente. Necesitas {price}.");
                return;
            }

            cash -= price;
            targetNode.AddUnit(newUnit);
            Console.WriteLine($"- {newUnit.GetPrice()}");
            Console.WriteLine($"{newUnit.GetName()} creado en {targetNode.GetName()}");
        }
        private void EndTurn()
        {
            int totalIncome = 0;

            foreach (var node in nodes)
            {
                foreach (var structure in node.GetStructures())
                {
                    totalIncome += structure.CollectIncome();
                }
            }

            if (totalIncome > 0)
            {
                cash += totalIncome;
                Console.WriteLine($"Recolectaste {totalIncome} monedas este turno.");
            }
        }
        private void ShowMap()
        {
            Console.WriteLine("MAPA");
            for (int i = 0; i < nodes.Count; i++)
            {
                var node = nodes[i];
                string conquered = node.IsConquered() ? "[✔]" : "[ ]";
                Console.WriteLine($"{i}. {node.GetName()} {conquered}");
            }
        }
        private List<Node> GetAvailableNodes()
        {
            List<Node> availableNodes = new List<Node>();
            // Todos los nodos conquistados
            availableNodes.AddRange(nodes.Where(n => n.IsConquered()));

            // El primer nodo no conquistado que no sea la base enemiga
            Node nextNode = GetNextNode();
            if (nextNode != null && !nextNode.IsEnemyBase())
            {
                availableNodes.Add(nextNode);
            }

            return availableNodes;
        }
        private Node GetNextNode()
        {
            for (int i = 0; i < nodes.Count - 1; i++)
            {
                if (nodes[i].IsConquered() && !nodes[i + 1].IsConquered())
                {
                    return nodes[i + 1];
                }
            }
            return null;
        }
        private int Fibonacci(int n)
        {
            if (n <= 1) return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
    }
}

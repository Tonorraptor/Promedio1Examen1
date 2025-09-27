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
        private int turn = 0;

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
            while (option != 2)
            {
                Console.WriteLine("Menu Principal");
                Console.WriteLine("1 Jugar");
                Console.WriteLine("2 Salir");
                option = int.Parse(Console.ReadLine());

                switch (option)
                {
                    case 1:
                        PlayGame();
                        break;
                    case 2:
                        Console.WriteLine("Gracias por jugar");
                        break;
                }
            }
        }

        private void PlayGame()
        {
            int gameOption = -1;
            while (gameOption != 8)
            {
                Console.WriteLine("Partida");
                Console.WriteLine($"Monedas: {cash}");
                Console.WriteLine("1 Ver Edificios");
                Console.WriteLine("2 Ver Unidades");
                Console.WriteLine("3 Construir edificio");
                Console.WriteLine("4 Crear unidad");
                Console.WriteLine("5 Ver mapa");
                Console.WriteLine("6 Ver enemigos");
                Console.WriteLine("7 Siguiente turno");
                Console.WriteLine("8 Salir");

                // Validar entrada
                if (!int.TryParse(Console.ReadLine(), out gameOption))
                {
                    Console.WriteLine("Opción inválida. Introduce un número.");
                    gameOption = -1;
                    continue;
                }

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
                        ShowEnemyBaseStatus();
                        break;
                    case 7:
                        EndTurn();
                        break;
                    case 8:
                        Console.WriteLine("Saliendo de la partida...");
                        break;
                    default:
                        Console.WriteLine("Opción incorrecta");
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
            int nodeSelect = -1;
            foreach (var n in nodes)
            {
                if (n.IsConquered() && n.GetMaintenanceStructures().Count > 0)
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
            EnemyTurn();
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
            availableNodes.AddRange(nodes.Where(n => n.IsConquered()));

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
        private void ShowEnemyBaseStatus()
        {
            int enemyDeaths = 0;
            int unitDeaths = 0;
            int structureDestroyed = 0;
            foreach (var node in nodes)
            {
                if (node.IsEnemyBase())
                {
                    Console.WriteLine($"=== {node.GetName()} ===");
                    Console.WriteLine(node.GetUnitStatus());

                    int totalPower = 1200;
                    foreach (var unit in node.GetUnits())
                    {
                        Console.WriteLine($"- {unit.GetName()} (Ataque: {unit.GetAttack()}, Vida: {unit.GetHealth()})");
                        totalPower += unit.GetAttack();
                    }

                    Console.WriteLine($"Puntaje total de la base enemiga: {totalPower}");
                }
            }

            Console.WriteLine("El enemigo ataca...");

            Node targetNode = null;

            foreach (var n in nodes)
            {
                if (n.IsConquered() && !n.IsPlayerBase() && !n.IsEnemyBase())
                {
                    targetNode = n;
                }
            }

            if (targetNode != null)
            {
                Node enemyBase = null;

                foreach (var n in nodes)
                {
                    if (n.IsEnemyBase())
                    {
                        enemyBase = n;
                        break;
                    }
                }
                var enemyUnits = enemyBase.GetUnits();

                if (enemyUnits.Count > 0)
                {
                    Console.WriteLine($"El enemigo envía {enemyUnits.Count} unidades a atacar {targetNode.GetName()}!");

                    foreach (var enemy in enemyUnits.ToList())
                    {
                        if (targetNode.GetUnits().Count > 0)
                        {
                            var defender = targetNode.GetUnits()[0];
                            Combat(enemy, defender, targetNode, ref enemyDeaths, ref unitDeaths);
                        }
                        else if (targetNode.GetStructures().Count > 0)
                        {
                            var structure = targetNode.GetStructures()[0];
                            Combat(enemy, structure, targetNode, ref structureDestroyed, ref enemyDeaths);
                        }
                        else
                        {
                            Console.WriteLine($"{targetNode.GetName()} fue conquistado por el enemigo!");
                            targetNode.Conquer();
                            break;
                        }
                    }
                    enemyUnits.Clear();
                    Console.WriteLine("=== Resumen del Combate ===");
                    Console.WriteLine($"Unidades enemigas destruidas: {enemyDeaths}");
                    Console.WriteLine($"Tus unidades caídas: {unitDeaths}");
                    Console.WriteLine($"Edificios destruidos: {structureDestroyed}");
                }
            }
            else
            {
                Console.WriteLine("El enemigo no encontró nodos para atacar este turno.");
            }

        }
        private int Fibonacci(int n)
        {
            if (n <= 1) return n;
            return Fibonacci(n - 1) + Fibonacci(n - 2);
        }
        private void EnemyTurn()
        {
            turn++;
            int actions = Fibonacci(turn);

            Console.WriteLine($"Turno del enemigo {turn}");

            foreach (var node in nodes)
            {
                if (node.IsEnemyBase())
                {
                    for (int i = 0; i < actions; i++)
                    {
                        Unit newUnit;
                        if (i % 3 == 0)
                            newUnit = Soldier.Create("Soldado Enemigo", 10, 60, 1, 0);
                        else if (i % 3 == 1)
                            newUnit = Tank.Create("Tanque Enemigo", 20, 90, 2, 0);
                        else
                            newUnit = Helicopter.Create("Helicóptero Enemigo", 70, 110, 3, 0);

                        node.AddUnit(newUnit);
                        Console.WriteLine($"El enemigo creó {newUnit.GetName()} en {node.GetName()}");
                    }
                }
            }

            Console.WriteLine("Fin del turno enemigo");
        }
        private void Combat(Unit unitEnemy, Unit unit, Node targetNode,
                    ref int enemyDeaths, ref int unitDeaths)
        {
            Console.WriteLine($"{unitEnemy.GetName()} ataca a {unit.GetName()}");

            unit.TakeDamage(unitEnemy.GetAttack());

            if (unit.GetHealth() <= 0)
            {
                Console.WriteLine($"{unit.GetName()} fue destruido.");
                targetNode.RemoveUnit(unit);

                if (unit.GetName().Contains("Enemigo"))
                    enemyDeaths++;
                else
                    unitDeaths++;

                return;
            }

            Console.WriteLine($"{unit.GetName()} contraataca a {unitEnemy.GetName()}");

            unitEnemy.TakeDamage(unit.GetAttack());

            if (unitEnemy.GetHealth() <= 0)
            {
                Console.WriteLine($"{unitEnemy.GetName()} fue destruido.");

                if (unitEnemy.GetName().Contains("Enemigo"))
                    enemyDeaths++;
                else
                    unitDeaths++;
            }
        }

        private void Combat(Unit unitEnemy, Building structure, Node targetNode,
          ref int structureDestroyed, ref int enemyDeaths)
        {
            Console.WriteLine($"{unitEnemy.GetName()} ataca a {structure.GetName()}");
            structure.TakeDamage(unitEnemy.GetAttack());

            if (structure.GetHealth() <= 0)
            {
                Console.WriteLine($"{structure.GetName()} fue destruida.");
                targetNode.RemoveStructure(structure);
                structureDestroyed++;
                return;
            }
            else
            {
                Console.WriteLine($"{structure.GetName()} sobrevivió con {structure.GetHealth()} de vida.");
            }

            if (structure is DefenseStructure defenseStructure)
            {
                Console.WriteLine($"{structure.GetName()} contraataca a {unitEnemy.GetName()}");
                unitEnemy.TakeDamage(defenseStructure.Attack());

                if (unitEnemy.GetHealth() <= 0)
                {
                    Console.WriteLine($"{unitEnemy.GetName()} fue destruido por la defensa.");
                    targetNode.RemoveUnit(unitEnemy);
                    enemyDeaths++;
                }
                else
                {
                    Console.WriteLine($"{unitEnemy.GetName()} sobrevivió con {unitEnemy.GetHealth()} de vida.");
                }
            }
        }
    }
}

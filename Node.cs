using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class Node
    {
        private string name;                 
        private bool isPlayerBase;            
        private bool isEnemyBase;
        public List<Unit> units;             
        public List<Building> structures;

        private bool isConquered;
        private bool isConqueredByEnemy;

        public Node(string name, bool isPlayerBase = false, bool isEnemyBase = false)
        {
            this.name = name;
            this.isPlayerBase = isPlayerBase;
            this.isEnemyBase = isEnemyBase;
            units = new List<Unit>();
            structures = new List<Building>();
        }
        public void AddUnit(Unit unit)
        {
            units.Add(unit);
        }

        public void AddStructure(Building structure)
        {
            structures.Add(structure);
        }
        public void RemoveUnit(Unit unit)
        {
            units.Remove(unit);
        }
        public string GetStructureStatus()
        {
            if(isPlayerBase || isConquered)
            {
                return $"{name} - Estructuras: {structures.Count}";
            }
            return $"{name} | Estructuras: {structures.Count}";
        }
        public string GetUnitStatus()
        {
            if (isPlayerBase || isConquered)
            {
                return $"{name} - Unidades: {units.Count}";
            }
            return $"{name} - Unidades: {units.Count}";
        }
        public bool IsPlayerBase()
        {
            return isPlayerBase;
        }
        public bool IsEnemyBase()
        {
            return isEnemyBase;
        }
        public void Conquer()
        {
            isConquered = true;
            isConqueredByEnemy = false;
        }
        public bool IsConquered()
        {
            return isConquered;
        }
        public List<MaintenanceStructure> GetMaintenanceStructures()
        {
            return structures.OfType<MaintenanceStructure>().ToList();
        }
        public List<Unit> GetUnits()
        {
            return units;
        }
        public string GetName()
        {
            return $"{name}";
        }
        public List<Building> GetStructures()
        {
            return structures;
        }
        public void RemoveStructure(Building structure)
        {
            structures.Remove(structure);
        }
        public void ConquerByEnemy()
        {
            isConqueredByEnemy = true;
            isConquered = false;
        }
        public bool IsConqueredByEnamy()
        {
            return isConqueredByEnemy;
        }
    }
}

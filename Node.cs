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
        private List<Unit> units;             
        private List<Building> structures;

        private bool isConquered;

        public Node(string name, bool isPlayerBase = false, bool isEnemyBase = false)
        {
            this.name = name;
            this.isPlayerBase = isPlayerBase;
            this.isEnemyBase = isEnemyBase;
            units = new List<Unit>();
            structures = new List<Building>();

            if (isPlayerBase)
            {
                isConquered = true;
            }
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
        }
        public bool IsConquered()
        {
            return isConquered;
        }
        public List<MaintenanceStructure> GetMaintenanceStructures()
        {
            return structures.OfType<MaintenanceStructure>().ToList();
        }
        public string GetName()
        {
            return $"{name}";
        }
        public List<Building> GetStructures()
        {
            return structures;
        }
    }
}

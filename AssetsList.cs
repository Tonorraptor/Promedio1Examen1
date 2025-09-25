using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Promedio1Examen1
{
    internal class AssetsList
    {
        private List<Building> structures = new List<Building>();
        private List<Unit> units = new List<Unit>();
        private List<Unit> enemys = new List<Unit>();

        public void AddStructures()
        {
            structures.Add(RecollectionStructure.Create("Estructura de Recoleccion", 100, 600, 150));
            structures.Add(MaintenanceStructure.Create("Estructura de mantenimiento", 175, 1200));
            structures.Add(DefenseStructure.Create("Estructura de defensa", 175, 1020, 5));
        }
        public void AddUnits()
        {
            units.Add(Soldier.Create("Soldado", 13, 70, 1, 80));
            units.Add(Tank.Create("Tanque", 24, 100, 2, 105));
            units.Add(Helicopter.Create("Helicoptero", 75, 120, 3, 295));
        }
        public void AddEnemys()
        {
            enemys.Add(Soldier.Create("Soldado", 13, 0, 1, 80));
            enemys.Add(Tank.Create("Tanque", 24, 0, 2, 105));
            enemys.Add(Helicopter.Create("Helicoptero", 75, 0, 3, 295));
        }
    }
}

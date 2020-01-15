using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Units.ShipModel.Reflexes
{
    internal class DieReflex : IReflex
    {
        private readonly Ship _ship;
        private readonly Map _map;

        public DieReflex(Ship ship, Map map)
        {
            _ship = ship;
            _map = map;
        }

        public void DoReflex()
        {
            var unitsInCell = _map.GetUnits(_ship.CurrentPosition).ToArray();
            if (unitsInCell.Length > 1)
            {
                foreach (var unit in unitsInCell)
                {
                    unit.Kill();
                }
            }

        }
    }
}
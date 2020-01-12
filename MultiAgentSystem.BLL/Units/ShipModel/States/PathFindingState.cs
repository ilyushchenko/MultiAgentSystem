using System;
using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Tools;

namespace MultiAgentSystem.BLL.Units.ShipModel.States
{
    internal class PathFindingState : IShipState
    {
        private readonly Ship _ship;
        private readonly Map _map;

        public PathFindingState(Ship ship, Map map)
        {
            _ship = ship ?? throw new ArgumentNullException(nameof(ship));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public void Handle()
        {
            var arr = new int[_map.Width, _map.Height];
            var path = AStarPathFinding.FindPath(arr, _ship.CurrentPosition, new Point(9, 9));

            _ship.SetPath(path);
            _ship.SetState(new WaitState(_ship, _map));
        }
    }
}
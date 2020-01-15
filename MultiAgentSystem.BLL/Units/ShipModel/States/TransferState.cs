using System.Collections.Generic;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Units.ShipModel.States
{
    internal class TransferState : IShipState
    {
        private readonly Ship _ship;
        private readonly Map _map;
        private float _cellLength;

        public TransferState(Ship ship, Map map, float cellLength)
        {
            _ship = ship;
            _map = map;
            _cellLength = cellLength;
        }

        public void Handle()
        {
            if (_ship.Path.Count == 0) return;

            var nextPathPosition = _ship.Path.Peek();

            var nextPositionByDirection = Point.GetFromDirection(_ship.CurrentPosition, _ship.MoveDirection);

            if (nextPositionByDirection != nextPathPosition)
            {
                _ship.SetState(new ChangeDirectionState(_ship, _map));
                return;
            }

            // Move by the direction
            _cellLength -= _ship.Speed;

            if (_cellLength <= 0f)
            {
                var nextPosition = _ship.Path.Dequeue();
                _map.Move(_ship, nextPosition);
                _cellLength = 50f;
            }
        }
    }
}
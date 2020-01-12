using System;
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Units.ShipModel.States
{
    internal class WaitState : IShipState
    {
        private readonly Ship _ship;
        private readonly Map _map;

        public WaitState(Ship ship, Map map)
        {
            _ship = ship ?? throw new ArgumentNullException(nameof(ship));
            _map = map ?? throw new ArgumentNullException(nameof(map));
        }

        public void Handle()
        {
            _ship.Time += 15;
            var currentCell = _map.GetCell(_ship.CurrentPosition);

            if (_ship.Time > Cell.Size) _ship.SetState(new TransferState(_ship, _map));
        }
    }
}
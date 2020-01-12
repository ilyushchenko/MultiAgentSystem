using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Units.ShipModel.States
{
    internal class TransferState : IShipState
    {
        private readonly Ship _ship;
        private readonly Map _map;

        public TransferState(Ship ship, Map map)
        {
            _ship = ship;
            _map = map;
        }

        public void Handle()
        {
            var currentCell = _map.GetCell(_ship.CurrentPosition);

            if (_ship.Time > Cell.Size)
            {
                _ship.Time -= Cell.Size;
                if (_ship.Path.Count > 0)
                {
                    var nextPosition = _ship.Path[0];
                    _ship.Path.RemoveAt(0);
                    _map.Move(_ship, nextPosition);
                    _ship.SetState(new WaitState(_ship, _map));
                }
            }
        }
    }
}
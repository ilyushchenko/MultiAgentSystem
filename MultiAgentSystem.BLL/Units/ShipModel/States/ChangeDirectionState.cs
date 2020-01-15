using System;
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Units.ShipModel.States
{
    internal class ChangeDirectionState : IShipState
    {
        private readonly Ship _ship;
        private readonly Map _map;
        private readonly bool _rotateToRight;

        private static readonly Random Random = new Random();

        public ChangeDirectionState(Ship ship, Map map)
        {
            _ship = ship;
            _map = map;

            _rotateToRight = Random.NextDouble() < 0.5f;
        }

        public void Handle()
        {
            var nextPositionByDirection = Point.GetFromDirection(_ship.CurrentPosition, _ship.MoveDirection);
            if (nextPositionByDirection == _ship.Path.Peek())
            {
                _ship.SetState(new TransferState(_ship, _map, Cell.Size));
            }
            else
            {
                var newDirection = GetNextDirection(_ship.MoveDirection, _rotateToRight);
                _ship.SetDirection(newDirection);
            }
        }

        private static Direction GetNextDirection(Direction currentDirection, bool toRight)
        {
            switch (currentDirection)
            {
                case Direction.Nord:
                {
                    return toRight ? Direction.NordEast : Direction.NordWest;
                }
                case Direction.NordWest:
                {
                    return toRight ? Direction.Nord : Direction.West;
                }
                default:
                {
                    var directionValue = (int) currentDirection;
                    directionValue += toRight ? 1 : -1;
                    return (Direction) directionValue;
                }
            }
        }
    }
}
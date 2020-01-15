using MultiAgentSystem.BLL.Models;
using MultiAgentSystem.BLL.Units.ShipModel;

namespace MultiAgentSystem.BLL.Interfaces
{
    public interface IUnit
    {
        Point CurrentPosition { get; }
        Direction MoveDirection { get; }
        Point? TargetPosition { get; }
        void SetPosition(Point newPosition);
        void SetDirection(Direction newDirection);
        void SetTarget(Point target);
    }
}
using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Interfaces
{
    public interface IUnit
    {
        Position? Position { get; }
        void SetPosition(Position newPosition);
    }
}
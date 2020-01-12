using MultiAgentSystem.BLL.Models;

namespace MultiAgentSystem.BLL.Interfaces
{
    public interface IUnit
    {
        Point CurrentPosition { get; }
        void SetPosition(Point newPosition);
    }
}
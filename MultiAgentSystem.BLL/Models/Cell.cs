using System.Collections.Generic;
using MultiAgentSystem.BLL.Interfaces;

namespace MultiAgentSystem.BLL.Models
{
    public class Cell
    {
        private readonly object _threadSync = new object();
        private readonly List<IUnit> _units;

        public Cell(Point position, float depth)
        {
            Position = position;
            Depth = depth;
            _units = new List<IUnit>();
        }

        public Point Position { get; }
        public float Depth { get; }
        public static float Size { get; } = 50;

        public IReadOnlyCollection<IUnit> GetUnits()
        {
            lock (_threadSync)
            {
                return _units.AsReadOnly();
            }
        }

        public void AddUnit(IUnit unit)
        {
            lock (_threadSync)
            {
                _units.Add(unit);
            }
        }

        public bool RemoveUnit(IUnit unit)
        {
            bool removed;
            lock (_threadSync)
            {
                removed = _units.Remove(unit);
            }

            return removed;
        }

        public override string ToString()
        {
            return $"{Position} Depth: {Depth}";
        }
    }
}
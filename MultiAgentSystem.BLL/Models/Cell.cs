using System.Collections.Generic;
using MultiAgentSystem.BLL.Interfaces;

namespace MultiAgentSystem.BLL.Models
{
    public class Cell
    {
        private readonly List<IUnit> _units;
        
        public Position Position { get; }

        public IReadOnlyCollection<IUnit> GetUnits()
        {
            return _units.AsReadOnly();
        }

        public Cell(Position position)
        {
            Position = position;
            _units = new List<IUnit>();
        }

        public void AddUnit(IUnit unit)
        {
            _units.Add(unit);
        }

        public bool RemoveUnit(IUnit unit)
        {
            return _units.Remove(unit);
        }
    }
}
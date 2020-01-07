using System;
using MultiAgentSystem.BLL.Interfaces;

namespace MultiAgentSystem.BLL.Models
{
    public class Map
    {
        public int Width { get; }
        public int Height { get; }

        private readonly Cell[] _cells;
        public Map(int width, int height)
        {
            Width = width;
            Height = height;

            _cells = new Cell[width * height];

            for (int y = 0; y < Height; y++)
            {
                for (int x = 0; x < Width; x++)
                {
                    var index = GetCellIndex(x, y);
                    _cells[index] = new Cell(new Position(x, y));
                }
            }
        }

        public void Move(IUnit unit, Position newPosition)
        {
            if (unit.Position.HasValue)
            {
                var oldPosition = unit.Position.Value;
                var oldCell = GetCell(oldPosition);

                var removed = oldCell.RemoveUnit(unit);
                if (!removed)
                {
                    throw new Exception($"Unit ({unit}) not found in cell ({oldCell})");
                }
                //TODO: Add cell updated event
            }

            var newCell = GetCell(newPosition);
            newCell.AddUnit(unit);
            unit.SetPosition(newPosition);
            
            //TODO: Add cell updated event
        }

        private int GetCellIndex(int x, int y)
        {
            int index = Height * y + x;
            return index;
        }

        private Cell GetCell(Position position)
        {
            var newCellIndex = GetCellIndex(position.X, position.Y);
            var newCell = _cells[newCellIndex];
            return newCell;
        }
    }
}

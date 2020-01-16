using MultiAgentSystem.BLL.Interfaces;
using MultiAgentSystem.BLL.Models.Generation;

namespace MultiAgentSystem.BLL.Models
{
    public class Map
    {
        private readonly Cell[] _cells;

        public Map(int width, int height) : this(width, height, new RandomMapGeneration(30, -10))
        {
        }

        public Map(int width, int height, IMapGenerator generator)
        {
            Width = width;
            Height = height;

            _cells = new Cell[width * height];

            for (var y = 0; y < Height; y++)
            for (var x = 0; x < Width; x++)
            {
                var index = GetCellIndex(x, y);
                _cells[index] = generator.GetCell(new Point(x, y));
            }
        }

        public int Width { get; }
        public int Height { get; }

        public float GetMaxDepth()
        {
            var maxdepth = float.MinValue;
            foreach (var cell in _cells)
                if (cell.Depth > maxdepth)
                    maxdepth = cell.Depth;

            return maxdepth;
        }

        public float GetMinDepth()
        {
            var minDepth = float.MaxValue;
            foreach (var cell in _cells)
                if (cell.Depth < minDepth)
                    minDepth = cell.Depth;

            return minDepth;
        }

        public void Move(IUnit unit, Point newPosition)
        {
            var oldPosition = unit.CurrentPosition;
            var oldCell = GetCell(oldPosition);

            oldCell.RemoveUnit(unit);

            var newCell = GetCell(newPosition);
            newCell.AddUnit(unit);
            unit.SetPosition(newPosition);
        }

        public Cell GetCell(Point position)
        {
            var newCellIndex = GetCellIndex(position.X, position.Y);
            var newCell = _cells[newCellIndex];
            return newCell;
        }

        public float GetCellDepth(Point position)
        {
            var cell = GetCell(position);
            return cell.Depth;
        }

        private int GetCellIndex(int x, int y)
        {
            var index = Height * y + x;
            return index;
        }
    }
}
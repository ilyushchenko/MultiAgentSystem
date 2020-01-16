using System;

namespace MultiAgentSystem.BLL.Models.Generation
{
    public class RandomMapGeneration : IMapGenerator
    {
        private readonly float _maxDepth;
        private readonly float _maxHeight;
        private readonly Random _random;

        public RandomMapGeneration(float maxDepth, float maxHeight)
        {
            if (maxDepth < 0)
                throw new ArgumentOutOfRangeException(nameof(maxDepth), "Depth value must be positive value");
            if (maxHeight >= 0)
                throw new ArgumentOutOfRangeException(nameof(maxHeight), "Max height value must be negative value");
            
            _maxDepth = maxDepth;
            _maxHeight = maxHeight;
            _random = new Random(DateTime.Now.Millisecond);
        }

        public Cell GetCell(Point position)
        {
            return new Cell(position, _random.Next((int) _maxHeight, (int) _maxDepth));
        }
    }
}
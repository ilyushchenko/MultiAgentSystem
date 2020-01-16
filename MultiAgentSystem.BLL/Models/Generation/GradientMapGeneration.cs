using System;

namespace MultiAgentSystem.BLL.Models.Generation
{
    public class GradientMapGeneration : IMapGenerator
    {
        private readonly int _width;
        private readonly int _height;
        private readonly float _range;

        /// <summary>
        /// Generate linear map gradient
        /// </summary>
        /// <param name="width">Map width</param>
        /// <param name="height">Map height</param>
        /// <param name="range">Full depth range
        /// <example>for range 100 depth will be -50:50</example>
        /// </param>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        public GradientMapGeneration(int width, int height, float range)
        {
            if (width <= 0) throw new ArgumentOutOfRangeException(nameof(width));
            if (height <= 0) throw new ArgumentOutOfRangeException(nameof(height));
            
            _width = width;
            _height = height;
            _range = range;
        }
        public Cell GetCell(Point position)
        {
            var coefficient = (float) (position.X + position.Y) / (_height + _width);
            var depth = _range * coefficient;
            return new Cell(position, -(_range / 2) + depth);
        }
    }
}
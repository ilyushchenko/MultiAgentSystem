using System;
using MultiAgentSystem.BLL.Units.ShipModel;

namespace MultiAgentSystem.BLL.Models
{
    public struct Point
    {
        public Point(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; }
        public int Y { get; }

        public bool Equals(Point other)
        {
            return X == other.X && Y == other.Y;
        }

        public override bool Equals(object obj)
        {
            return obj is Point other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return (X * 397) ^ Y;
            }
        }

        public static bool operator ==(Point firstPoint, Point secondPoint)
        {
            return firstPoint.Equals(secondPoint);
        }

        public static bool operator !=(Point firstPoint, Point secondPoint)
        {
            return !(firstPoint == secondPoint);
        }

        public override string ToString()
        {
            return $"[{X}, {Y}]";
        }

        public static Point GetFromDirection(Point position, Direction direction)
        {
            switch (direction)
            {
                case Direction.Nord:
                    return new Point(position.X, position.Y - 1);
                case Direction.NordEast:
                    return new Point(position.X + 1, position.Y - 1);
                case Direction.East:
                    return new Point(position.X + 1, position.Y);
                case Direction.SouthEast:
                    return new Point(position.X + 1, position.Y + 1);
                case Direction.South:
                    return new Point(position.X, position.Y + 1);
                case Direction.SouthWest:
                    return new Point(position.X - 1, position.Y + 1);
                case Direction.West:
                    return new Point(position.X - 1, position.Y);
                case Direction.NordWest:
                    return new Point(position.X - 1, position.Y - 1);
                default:
                    throw new ArgumentOutOfRangeException(nameof(direction), direction, $"The enumeration {direction} not found in {typeof(Direction)}");
            }
        }
    }
}
using System;

namespace Coordinates
{
    [Serializable]
    public struct Coordinate
    {
        public int X;
        public int Y;

        public Coordinate(int x, int y)
        {
            X = x;
            Y = y;
        }

        public Coordinate(CubeCoordinate coordinate)
        {
            X = coordinate.x + (coordinate.z + (coordinate.z & 1)) / 2;
            Y = coordinate.z;
        }

        public static Coordinate operator +(Coordinate a, Coordinate b)
        {
            return new Coordinate(a.X + b.X, a.Y + b.Y);
        }

        public static double CalculateDistance(Coordinate a, Coordinate b)
        {
            return Math.Sqrt(Math.Pow(b.X - a.X, 2) + Math.Pow(b.Y - a.Y, 2));
        }

        public override string ToString()
        {
            return $"X: {X} Y: {Y}";
        }

        public override bool Equals(object obj)
        {
            Coordinate b = (Coordinate)obj;
            return b.X == X && b.Y == Y;
        }

        public override int GetHashCode()
        {
            var hashCode = 1861411795;
            hashCode = hashCode * -1521134295 + X.GetHashCode();
            hashCode = hashCode * -1521134295 + Y.GetHashCode();
            return hashCode;
        }
    }
}

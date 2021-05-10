using System;

namespace Coordinates
{
    [Serializable]
    public struct CubeCoordinate
    {
        public int x;
        public int y;
        public int z;

        public CubeCoordinate(int x, int y, int z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public CubeCoordinate(Coordinate coordinate)
        {
            x = coordinate.X - (coordinate.Y + (coordinate.Y & 1)) / 2;
            z = coordinate.Y;
            y = -x - z;
        }

        public bool IsCorrect()
        {
            return x + y + z == 0;
        }

        public override string ToString()
        {
            return $"(X: {x} Y: {y} Z: {z})";
        }

        public static CubeCoordinate operator+(CubeCoordinate A, CubeCoordinate B)
        {
            return new CubeCoordinate(A.x + B.x, A.y + B.y, A.z + B.z);
        }


        public static CubeCoordinate operator -(CubeCoordinate A, CubeCoordinate B)
        {
            return new CubeCoordinate(A.x - B.x, A.y - B.y, A.z - B.z);
        }


        public static CubeCoordinate operator *(CubeCoordinate A, int scalar)
        {
            return new CubeCoordinate(A.x * scalar, A.y * scalar, A.z * scalar);
        }

        public override bool Equals(object obj)
        {
            CubeCoordinate B = (CubeCoordinate)obj;
            return x == B.x && y == B.y && z == B.z;
        }

        public override int GetHashCode()
        {
            var hashCode = -307843816;
            hashCode = hashCode * -1521134295 + x.GetHashCode();
            hashCode = hashCode * -1521134295 + y.GetHashCode();
            hashCode = hashCode * -1521134295 + z.GetHashCode();
            return hashCode;
        }

        public static CubeCoordinate[] Neighbours = new[]
        {
            new CubeCoordinate(+1, -1, 0),
            new CubeCoordinate(+1, 0, -1),
            new CubeCoordinate(0, +1, -1),
            new CubeCoordinate(-1, +1, 0),
            new CubeCoordinate(-1, 0, +1),
            new CubeCoordinate(0, -1, +1),
        };
    }
}

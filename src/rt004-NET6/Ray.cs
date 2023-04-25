using System.Numerics;


namespace rt004
{
    public struct Ray
    {
        public Vector3D Start;
        public Vector3D Direction;
        public Ray(Vector3D start, Vector3D direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}

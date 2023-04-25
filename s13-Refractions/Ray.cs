using System.Numerics;


namespace rt004
{
    public struct Ray
    {
        public Vector3 Start;
        public Vector3 Direction;
        public Ray(Vector3 start, Vector3 direction)
        {
            Start = start;
            Direction = direction;
        }
    }
}

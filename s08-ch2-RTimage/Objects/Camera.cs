namespace rt004
{
    public class Camera
    {
        public Vector Position;
        public Vector Forward;
        public Vector Up;
        public Vector Right;

        public Camera(Vector pos, Vector forward, Vector up, Vector right)
        {
            Position = pos;
            Forward = forward;
            Up = up;
            Right = right;

        }

        public Camera(Vector pos, Vector lookAt)
        {
           Position = pos;
           Forward = Vector.Normalize(lookAt - pos);
           Right = 1.5 * Vector.Normalize(Vector.Cross(Forward, new Vector(0, -1, 0)));
           Up = 1.5 * Vector.Normalize(Vector.Cross(Forward, Right));
        }
    }
}

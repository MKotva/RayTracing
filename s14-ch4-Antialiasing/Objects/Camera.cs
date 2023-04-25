using System.Numerics;

namespace rt004
{
    public class Camera
    {
        public Vector3 Position {get; set;}
        public Vector3 Forward { get; set;}
        public Vector3 Up { get; set; }
        public Vector3 Right { get; set;}

        public Camera(Vector3 pos, Vector3 forward, Vector3 up, Vector3 right)
        {
            Position = pos;
            Forward = forward;
            Up = up;
            Right = right;

        }

        public Camera(Vector3 pos, Vector3 lookAt)
        {
           Position = pos;
           Forward = Vector3.Normalize(lookAt - pos);
           Right = 1.5f * Vector3.Normalize(Vector3.Cross(Forward, new Vector3(0, -1, 0)));
           Up = 1.5f * Vector3.Normalize(Vector3.Cross(Forward, Right));
        }
    }
}

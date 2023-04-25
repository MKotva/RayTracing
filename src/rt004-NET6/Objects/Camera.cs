using Newtonsoft.Json;
using System.Numerics;

namespace rt004
{
    public class Camera
    {
        public Vector3D Position {get; set;}
        public Vector3D Forward { get; set;}
        public Vector3D Up { get; set; }
        public Vector3D Right { get; set;}

        private Vector3D LookingAt { get; set;}

        [JsonConstructor]
        public Camera(Vector3D pos, Vector3D forward, Vector3D up, Vector3D right, Vector3D lookAt)
        {
            Position = pos;
            Forward = forward;
            Up = up;
            Right = right;
            LookingAt = lookAt;
        }

        public Camera(Vector3D pos, Vector3D lookAt)
        {
           Position = pos;
           LookingAt = lookAt;
           Forward = Vector3D.Normalize(lookAt - pos);
           Right = 1.5f * Vector3D.Normalize(Vector3D.Cross(Forward, new Vector3D(0, -1, 0)));
           Up = 1.5f * Vector3D.Normalize(Vector3D.Cross(Forward, Right));
        }

        public override string ToString()
        {
            return $"Camera[{Vector3Extension.ToString(Position)},{Vector3Extension.ToString(LookingAt)}]";
        }

        public static Camera FromString(string str)
        {
            var substring = str.Substring(str.IndexOf('['), str.IndexOf(']'));
            var values = substring.Split(',');

            return new Camera(Vector3Extension.FromString(values[0]), Vector3Extension.FromString(values[1]));
        }
    }
}

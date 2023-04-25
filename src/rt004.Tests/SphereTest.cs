using rt004.Objects;
using Xunit;

namespace rt004.Tests
{
    public class SphereTest
    {
        [Fact]
        public void ColisionFromCenterOut()
        {
            Sphere s = new Sphere(new Vector3D(0, 0, 0), 1, null);
            Ray r = new Ray(new Vector3D(0, 0, 0), new Vector3D(1, 0, 0));
            Selection selection = s.Intersect(r);

            Vector3D expectedIntersect = new Vector3D(1, 0, 0);
            Vector3D reresultedIntersect = r.Start + selection.Distance * r.Direction;

            Assert.Equivalent(expectedIntersect, reresultedIntersect);
        }

        [Fact]
        public void ColisionFromOutToCenter()
        {
            Sphere s = new Sphere(new Vector3D(0, 0, 0), 1, null);
            Ray r = new Ray(new Vector3D(2, 0, 0), new Vector3D(-1, 0, 0));
            Selection selection = s.Intersect(r);

            Vector3D expectedIntersect = new Vector3D(1, 0, 0);
            Vector3D reresultedIntersect = r.Start + selection.Distance * r.Direction;

            Assert.Equivalent(expectedIntersect, reresultedIntersect);
        }

        [Fact]
        public void NoCollisionInBotDirectionalInterection()
        {
            Sphere s = new Sphere(new Vector3D(0, 0, 0), 1, null);
            Ray r = new Ray(new Vector3D(2, 0, 0), new Vector3D(1, 0, 0));
            Selection selection = s.Intersect(r);
            Assert.Null(selection);
        }

        [Fact]
        public void NoCollision()
        {
            Sphere s = new Sphere(new Vector3D(0, 0, 0), 1, null);
            Ray r = new Ray(new Vector3D(2, 0, 0), new Vector3D(0, 1, 0));
            Selection selection = s.Intersect(r);
            Assert.Null(selection);
        }
    }
}
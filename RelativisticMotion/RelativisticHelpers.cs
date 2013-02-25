using Microsoft.Xna.Framework;

namespace RelativisticMotion
{
    public static class RelativisticHelpers
    {
        public static bool IsFasterThanLight(Vector4 a, Vector4 b, float speedOfLight)
        {
            Vector4 difference = a - b;
            return new Vector3(difference.X, difference.Y, difference.Z).LengthSquared() > difference.W * difference.W * speedOfLight * speedOfLight;
        }
    }
}

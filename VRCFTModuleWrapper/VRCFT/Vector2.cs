using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace VRCFTModuleWrapper.VRCFT
{
    // Make a nullable class called Vector2
    [StructLayout(LayoutKind.Sequential)]
    public struct Vector2
    {
        public float x;
        public float y;

        public Vector2(float x, float y)
        {
            this.x = x;
            this.y = y;
        }

        // Make an implicit conversion from vector3 to vector2
        public static implicit operator Vector2(Vector3 v) => new Vector2(v.x, v.y);

        public static Vector2 operator *(Vector2 a, float d)
        => new Vector2(a.x * d, a.y * d);

        public static Vector2 zero => new Vector2(0, 0);
    }
}

using UnityEngine;

namespace Vector
{
    public struct Vector3D 
    {
        public float x;
        public float y;
        public float z;

        public float magnitude => Mathf.Sqrt(x*x + y*y + z*z);
        public Vector3D normalized => this / magnitude;

        public Vector3D(float x, float y, float z)
        {
            this.x = x;
            this.y = y;
            this.z = z;
        }

        public static float ScalarTriple(Vector3D v1, Vector3D v2, Vector3D v3) => Dot(Cross(v1, v2), v3);

        public static float Dot(Vector3D v1, Vector3D v2) => v1.x * v2.x + v1.y * v2.y + v1.z * v2.z;

        public static Vector3D Cross(Vector3D v1, Vector3D v2) =>
            new Vector3D(
                v1.y * v2.z - v1.z * v2.y,
                v1.z * v2.x - v1.x * v2.z,
                v1.x * v2.y - v1.y * v2.z);

        public Vector3D RejectOn(Vector3D target, bool isTargetNormalized = false)
        {
            var projection = ProjectOn(target, isTargetNormalized);
            return this - projection;
        }
        
        public Vector3D ProjectOn(Vector3D target, bool isTargetNormalized = false)
        {
            var normalizedTarget = isTargetNormalized ? target : target.normalized;
            return normalizedTarget * Dot(this, normalizedTarget);
        }

        public Vector3 ToNativeVector()
        {
            return new Vector3(x, y, z);
        }

        #region Operators

        public static Vector3D operator -(Vector3D v1, Vector3D v2) =>
            new Vector3D(v1.x - v2.x, v1.y - v2.y, v1.z - v2.z);

        public static Vector3D operator /(Vector3D source, float value) => source * (1f / value);

        public static Vector3D operator *(Vector3D source, float value) => 
            new Vector3D(source.x * value, source.y * value, source.z * value);

        #endregion
    }
}

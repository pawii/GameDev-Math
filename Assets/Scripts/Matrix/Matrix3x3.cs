using NUnit.Framework.Constraints;
using Vector;

namespace Matrix
{
    public class Matrix3x3
    {
        private readonly float[,] values;

        public float this[int row, int column]
        {
            set => values[row, column] = value;
            get => values[row, column];
        }

        public float determinant =>   this[0, 0] * this[1, 1] * this[2, 2] 
                                    + this[0, 1] * this[1, 2] * this[2, 0]
                                    + this[0, 2] * this[1, 0] * this[2, 1]
                                    - this[0, 1] * this[1, 0] * this[2, 2] 
                                    - this[0, 2] * this[1, 1] * this[2, 0]
                                    - this[0, 0] * this[1, 2] * this[2, 1];

        public Matrix3x3()
        {
            values = new float[,] {{0, 0, 0}, {0, 0, 0}, {0, 0, 0}};
        }

        public Matrix3x3 Transpose()
        {
            var transposed = new Matrix3x3();
            
            transposed[0, 0] = this[0, 0];
            transposed[0, 1] = this[1, 0];
            transposed[0, 2] = this[2, 0];
            
            transposed[1, 0] = this[0, 1];
            transposed[1, 1] = this[1, 1];
            transposed[1, 2] = this[2, 1];
            
            transposed[2, 0] = this[0, 2];
            transposed[2, 1] = this[1, 2];
            transposed[2, 2] = this[2, 2];

            return transposed;
        }

        public Matrix3x3 GetInverse()
        {
            var a = new Vector3D(this[0, 0], this[1, 0], this[2, 0]);
            var b = new Vector3D(this[0, 1], this[1, 1], this[2, 1]);
            var c = new Vector3D(this[0, 2], this[1, 2], this[2, 2]);

            var bc = Vector3D.Cross(b, c);
            var ca = Vector3D.Cross(c, a);
            var ab = Vector3D.Cross(a, b);
            var abc = Vector3D.Dot(ab, c);

            var inverse = new Matrix3x3();
            inverse[0, 0] = bc.x;
            inverse[0, 1] = bc.y;
            inverse[0, 2] = bc.z;
            
            inverse[1, 0] = ca.x;
            inverse[1, 1] = ca.y;
            inverse[1, 2] = ca.z;
            
            inverse[2, 0] = ab.x;
            inverse[2, 1] = ab.y;
            inverse[2, 2] = ab.z;

            return inverse / abc;
        }

        public static Matrix3x3 operator +(Matrix3x3 m1, Matrix3x3 m2)
        {
            return new Matrix3x3
            {
                [0, 0] = m1[0, 0] + m2[0, 0],
                [0, 1] = m1[0, 1] + m2[0, 1],
                [0, 2] = m1[0, 2] + m2[0, 2],
                
                [1, 0] = m1[1, 0] + m2[1, 0],
                [1, 1] = m1[1, 1] + m2[1, 1],
                [1, 2] = m1[1, 2] + m2[1, 2],
                
                [2, 0] = m1[2, 0] + m2[2, 0],
                [2, 1] = m1[2, 1] + m2[2, 1],
                [2, 2] = m1[2, 2] + m2[2, 2]
            };
        }

        public static Matrix3x3 operator /(Matrix3x3 m, float v)
        {
            var multiplicator = 1f / v;
            return m * multiplicator;
        }

        public static Matrix3x3 operator *(Matrix3x3 m, float v)
        {
            var result = new Matrix3x3();

            result[0, 0] *= v;
            result[0, 1] *= v;
            result[0, 2] *= v;

            result[1, 0] *= v;
            result[1, 1] *= v;
            result[1, 2] *= v;

            result[2, 0] *= v;
            result[2, 1] *= v;
            result[2, 2] *= v;

            return result;
        }

        public static Matrix3x3 operator *(Matrix3x3 m1, Matrix3x3 m2)
        {
            return new Matrix3x3
            {
                [0, 0] = m1[0, 0] * m2[0, 0] + m1[0, 1] * m2[1, 0] + m1[0, 2] * m2[2, 0],
                [0, 1] = m1[0, 0] * m2[0, 1] + m1[0, 1] * m2[1, 1] + m1[0, 2] * m2[2, 1],
                [0, 2] = m1[0, 0] * m2[0, 2] + m1[0, 1] * m2[1, 2] + m1[0, 2] * m2[2, 2],
                
                [1, 0] = m1[1, 0] * m2[0, 0] + m1[1, 1] * m2[1, 0] + m1[1, 2] * m2[2, 0],
                [1, 1] = m1[1, 0] * m2[0, 1] + m1[1, 1] * m2[1, 1] + m1[1, 2] * m2[2, 1],
                [1, 2] = m1[1, 0] * m2[0, 2] + m1[1, 1] * m2[1, 2] + m1[1, 2] * m2[2, 2],
                
                [2, 0] = m1[2, 0] * m2[0, 0] + m1[2, 1] * m2[1, 0] + m1[2, 2] * m2[2, 0],
                [2, 1] = m1[2, 0] * m2[0, 1] + m1[2, 1] * m2[1, 1] + m1[2, 2] * m2[2, 1],
                [2, 2] = m1[2, 0] * m2[0, 2] + m1[2, 1] * m2[1, 2] + m1[2, 2] * m2[2, 2]
            };
        }

        public static Vector3D operator *(Matrix3x3 m1, Vector3D v1)
        {
            return new Vector3D
            {
                x = m1[0, 0] * v1.x + m1[0, 1] * v1.y + m1[0, 2] * v1.z,
                y = m1[1, 0] * v1.x + m1[1, 1] * v1.y + m1[1, 2] * v1.z,
                z = m1[2, 0] * v1.x + m1[2, 1] * v1.y + m1[2, 2] * v1.z
            };
        }
    }
}

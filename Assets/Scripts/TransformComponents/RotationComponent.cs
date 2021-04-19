using System;
using Matrix;
using UnityEngine;
using Vector;

namespace TransformComponents
{
    [Serializable]
    public class RotationComponent
    {
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;

        private Matrix3x3 transformMatrix = new Matrix3x3();

        public Vector3D Apply(Vector3D sourceVector)
        {
            var result = CalculateXMatrix() * sourceVector;
            result = CalculateYMatrix() * result;
            result = CalculateZMatrix() * result;
            return result;
        }

        private Matrix3x3 CalculateXMatrix()
        {
            var radianAngel = x * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radianAngel);
            var sin = Mathf.Sin(radianAngel);
            return new Matrix3x3()
            {
                [0, 0] = 1,
                [0, 1] = 0,
                [0, 2] = 0,
                
                [1, 0] = 0,
                [1, 1] = cos,
                [1, 2] = -sin,
                
                [2, 0] = 0,
                [2, 1] = sin,
                [2, 2] = cos,
            };
        }

        private Matrix3x3 CalculateYMatrix()
        {
            var radianAngel = y * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radianAngel);
            var sin = Mathf.Sin(radianAngel);
            return new Matrix3x3()
            {
                [0, 0] = cos,
                [0, 1] = 0,
                [0, 2] = sin,
                
                [1, 0] = 0,
                [1, 1] = 1,
                [1, 2] = 0,
                
                [2, 0] = -sin,
                [2, 1] = 0,
                [2, 2] = cos,
            };
        }

        private Matrix3x3 CalculateZMatrix()
        {
            var radianAngel = z * Mathf.Deg2Rad;
            var cos = Mathf.Cos(radianAngel);
            var sin = Mathf.Sin(radianAngel);
            return new Matrix3x3()
            {
                [0, 0] = cos,
                [0, 1] = -sin,
                [0, 2] = 0,
                
                [1, 0] = sin,
                [1, 1] = cos,
                [1, 2] = 0,
                
                [2, 0] = 0,
                [2, 1] = 0,
                [2, 2] = 1,
            };
        }
    }
}

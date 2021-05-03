using System;
using Matrix;
using UnityEngine;
using Vector;

namespace TransformComponents
{
    [Serializable]
    public class RotationComponent : MonoBehaviour
    {
        [Serializable]
        public enum RotationType { Euler, Quaternion }

        [SerializeField] private RotationType rotationType;
        
        [SerializeField] private float eulerX;
        [SerializeField] private float eulerY;
        [SerializeField] private float eulerZ;
        
        [SerializeField] private float quaternionX;
        [SerializeField] private float quaternionY;
        [SerializeField] private float quaternionZ;
        [SerializeField] private float quaternionW;

        public Vector3D Apply(Vector3D sourceVector)
        {
            switch (rotationType)
            {
                case RotationType.Euler:
                    return CalculateEulerMatrix() * sourceVector;
                case RotationType.Quaternion:
                    return RotateQuaternion(sourceVector);
                default:
                    return sourceVector;
            }
        }

        #region Quaternion

        public Vector3D RotateQuaternion(Vector3D sourceVector)
        {
            if (Mathf.Approximately(quaternionX, 0f) &&
                Mathf.Approximately(quaternionY, 0f) && 
                Mathf.Approximately(quaternionZ, 0f))
            {
                return sourceVector;
            }
            
            var rotAngel = quaternionW * Mathf.Deg2Rad;

            // sourceVector = i + j;
            var i = new Vector3D(quaternionX, quaternionY, quaternionZ).normalized;
            var j = sourceVector - (Vector3D.Dot(sourceVector, i) * i);
            var k = Vector3.Cross(i.ToNativeVector(), sourceVector.ToNativeVector()).ToCustomVector();

            // result = ir + jr;
            var ir = Vector3D.Dot(sourceVector, i) * i;
            var jr = j * Mathf.Cos(rotAngel) + k * Mathf.Sin(rotAngel);

            var result = ir + jr;
            return result;
        }

        #endregion

        #region Euler

        private Matrix3x3 CalculateEulerMatrix()
        {
            return CalculateEulerYMatrix() * CalculateEulerXMatrix() * CalculateEulerZMatrix();
        }

        private Matrix3x3 CalculateEulerXMatrix()
        {
            var radianAngel = eulerX * Mathf.Deg2Rad;
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

        private Matrix3x3 CalculateEulerYMatrix()
        {
            var radianAngel = eulerY * Mathf.Deg2Rad;
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

        private Matrix3x3 CalculateEulerZMatrix()
        {
            var radianAngel = eulerZ * Mathf.Deg2Rad;
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

        #endregion

        private void OnDrawGizmos()
        {
            if (rotationType == RotationType.Quaternion)
            {
                Gizmos.color = Color.red;
                var vector = new Vector3(quaternionX, quaternionY, quaternionZ).normalized;
                Gizmos.DrawLine(transform.position + vector * -10f, transform.position + vector * 10f);
            }
        }
    }
}

using System;
using UnityEngine;
using Vector;

namespace TransformComponents
{
    [Serializable]
    public class PositionComponent 
    {
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;
        
        // TODO: производить операции матрицами
        public Vector3D Apply(Vector3D sourcePosition)
        {
            sourcePosition.x += x;
            sourcePosition.y += y;
            sourcePosition.z += z;
            return sourcePosition;
        }
    }
}

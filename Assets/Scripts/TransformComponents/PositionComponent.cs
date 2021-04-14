using System;
using UnityEngine;

namespace TransformComponents
{
    [Serializable]
    public class PositionComponent 
    {
        [SerializeField] private float x;
        [SerializeField] private float y;
        [SerializeField] private float z;
        
        public Vector3 Apply(Vector3 sourcePosition)
        {
            sourcePosition.x += x;
            sourcePosition.y += y;
            sourcePosition.z += z;
            return sourcePosition;
        }
    }
}

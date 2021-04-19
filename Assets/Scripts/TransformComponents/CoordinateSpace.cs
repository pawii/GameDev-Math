using System;
using UnityEngine;
using Vector;

namespace TransformComponents
{
    [Serializable]
    public class CoordinateSpace
    {
        [SerializeField] private PositionComponent positionComponent;
        [SerializeField] private RotationComponent rotationComponent;

        public Vector3D Apply(Vector3D sourcePosition)
        {
            var localSpacePosition = positionComponent.Apply(sourcePosition);
            localSpacePosition = rotationComponent.Apply(localSpacePosition);
            return localSpacePosition;
        }
    }
}

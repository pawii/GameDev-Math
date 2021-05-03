using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using TransformComponents;
using UnityEngine;
using Vector;

public class GridCube : MonoBehaviour
{
    [SerializeField] private int gridResolution = 10;
    [SerializeField] private Vector3 cubeSize;
    [SerializeField] private CoordinateSpace coordinateSpace;
    

    private void OnDrawGizmos() 
    {
        var points = new Vector3[gridResolution, gridResolution, gridResolution];
        
        for (int z = 0; z < gridResolution; z++) 
        {
            for (int y = 0; y < gridResolution; y++) 
            {
                for (int x = 0; x < gridResolution; x++)
                {
                    points[x, y, z] = CreateGridPoint(x, y, z);
                }
            }
        }

        var maxId = gridResolution - 1;
        
        Gizmos.DrawLine(points[0, 0, 0], points[0, 0, maxId]);
        Gizmos.DrawLine(points[0, 0, maxId], points[maxId, 0, maxId]);
        Gizmos.DrawLine(points[maxId, 0, maxId], points[maxId, 0, 0]);
        Gizmos.DrawLine(points[maxId, 0, 0], points[0, 0, 0]);
        
        Gizmos.DrawLine(points[0, maxId, 0], points[0, maxId, maxId]);
        Gizmos.DrawLine(points[0, maxId, maxId], points[maxId, maxId, maxId]);
        Gizmos.DrawLine(points[maxId, maxId, maxId], points[maxId, maxId, 0]);
        Gizmos.DrawLine(points[maxId, maxId, 0], points[0, maxId, 0]);
        
        Gizmos.DrawLine(points[0, 0, 0], points[0, maxId, 0]);
        Gizmos.DrawLine(points[0, 0, maxId], points[0, maxId, maxId]);
        Gizmos.DrawLine(points[maxId, 0, maxId], points[maxId, maxId, maxId]);
        Gizmos.DrawLine(points[maxId, 0, 0], points[maxId, maxId, 0]);
    }
    
    private Vector3 CreateGridPoint (int x, int y, int z) 
    {
        var position = GetCoordinates(x, y, z);
        position = coordinateSpace.Apply(position);
        
        var color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );

        Gizmos.color = color;
        Gizmos.DrawCube(position.ToNativeVector(), cubeSize);
        return position.ToNativeVector();
    }
    
    private Vector3D GetCoordinates (int x, int y, int z) 
    {
        return new Vector3D(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }

    private void DrawRectangle(Vector3 minBound, Vector3 maxBound)
    {
        Gizmos.DrawLine(new Vector3(minBound.x, minBound.y, minBound.z), new Vector3(minBound.x, minBound.y, maxBound.z));
        Gizmos.DrawLine(new Vector3(minBound.x, minBound.y, maxBound.z), new Vector3(maxBound.x, minBound.y, maxBound.z));
        Gizmos.DrawLine(new Vector3(maxBound.x, minBound.y, maxBound.z), new Vector3(maxBound.x, minBound.y, minBound.z));
        Gizmos.DrawLine(new Vector3(maxBound.x, minBound.y, minBound.z), new Vector3(minBound.x, minBound.y, minBound.z));
    }
}

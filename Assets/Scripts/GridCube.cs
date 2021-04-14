using TransformComponents;
using UnityEngine;

public class GridCube : MonoBehaviour
{
    [SerializeField] private int gridResolution = 10;
    [SerializeField] private Vector3 cubeSize;
    [SerializeField] private PositionComponent positionComponent;

    private void OnDrawGizmos() 
    {
        for (int i = 0, z = 0; z < gridResolution; z++) 
        {
            for (int y = 0; y < gridResolution; y++) 
            {
                for (int x = 0; x < gridResolution; x++, i++) 
                {
                    CreateGridPoint(x, y, z);
                }
            }
        }
    }
    
    private void CreateGridPoint (int x, int y, int z) 
    {
        var position = GetCoordinates(x, y, z);
        position = positionComponent.Apply(position);
        
        var color = new Color(
            (float)x / gridResolution,
            (float)y / gridResolution,
            (float)z / gridResolution
        );

        Gizmos.color = color;
        Gizmos.DrawCube(position, cubeSize);
    }
    
    private Vector3 GetCoordinates (int x, int y, int z) 
    {
        return new Vector3(
            x - (gridResolution - 1) * 0.5f,
            y - (gridResolution - 1) * 0.5f,
            z - (gridResolution - 1) * 0.5f
        );
    }
}

using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class PathSystem : MonoBehaviour
{
    [Header("Tilemaps")]
    [SerializeField] private Tilemap pathTilemap;
    [SerializeField] private Tilemap spawnTilemap;
    [SerializeField] private Tilemap goalTilemap;

    private List<Vector3> _path;

    public List<Vector3> BuildPath()
    {
        if (_path != null)
            return _path;

        Vector3Int spawnCell = GetSingleCell(spawnTilemap, "Spawn");
        Vector3Int goalCell  = GetSingleCell(goalTilemap,  "Goal");

        _path = BuildConnectedPath(spawnCell, goalCell);
        return _path;
    }

    Vector3Int GetSingleCell(Tilemap tilemap, string name)
    {
        BoundsInt bounds = tilemap.cellBounds;

        foreach (Vector3Int cell in bounds.allPositionsWithin)
        {
            if (tilemap.HasTile(cell))
                return cell;
        }

        Debug.LogError($"{name} tile not found!");
        return Vector3Int.zero;
    }

    List<Vector3> BuildConnectedPath(Vector3Int start, Vector3Int goal)
    {
        List<Vector3> path = new();
        HashSet<Vector3Int> visited = new();

        Vector3Int current = start;

        while (true)
        {
            visited.Add(current);
            path.Add(pathTilemap.GetCellCenterWorld(current));

            if (current == goal)
                break;

            Vector3Int next = GetNextCell(current, visited);

            if (next == current)
            {
                Debug.LogError("Path broken! Check PathTilemap.");
                break;
            }

            current = next;
        }

        return path;
    }

    Vector3Int GetNextCell(Vector3Int cell, HashSet<Vector3Int> visited)
    {
        Vector3Int[] dirs =
        {
            Vector3Int.up,
            Vector3Int.down,
            Vector3Int.left,
            Vector3Int.right
        };

        foreach (var dir in dirs)
        {
            Vector3Int next = cell + dir;

            if (visited.Contains(next))
                continue;

            if (pathTilemap.HasTile(next))
                return next;
        }

        return cell;
    }
}

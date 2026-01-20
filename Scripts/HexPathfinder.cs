using System.Collections.Generic;
using UnityEngine;

public static class HexPathfinder
{
    public static List<Vector2Int> FindPath(
        HexGrid grid,
        Vector2Int start,
        Vector2Int goal)
    {
        var frontier = new Queue<Vector2Int>();
        frontier.Enqueue(start);

        var cameFrom = new Dictionary<Vector2Int, Vector2Int>();
        cameFrom[start] = start;

        while (frontier.Count > 0)
        {
            var current = frontier.Dequeue();

            if (current == goal)
                break;

            foreach (var next in grid.GetNeighbors(current))
            {
                if (!cameFrom.ContainsKey(next))
                {
                    frontier.Enqueue(next);
                    cameFrom[next] = current;
                }
            }
        }

        if (!cameFrom.ContainsKey(goal))
            return null;

        List<Vector2Int> path = new();
        Vector2Int step = goal;

        while (step != start)
        {
            path.Add(step);
            step = cameFrom[step];
        }

        path.Reverse();
        return path;
    }
}

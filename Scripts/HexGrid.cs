using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float hexSize = 1f;

    public GameObject hexPrefab; // Sprite → Hexagon Flat Top (дебаг)

    private Dictionary<Vector2Int, HexCell> cells = new();

    // ====== НАПРАВЛЕНИЯ СОСЕДЕЙ (FLAT-TOP AXIAL) ======
    private static readonly Vector2Int[] directions = new Vector2Int[]
    {
        new Vector2Int( 1,  0),
        new Vector2Int( 1, -1),
        new Vector2Int( 0, -1),
        new Vector2Int(-1,  0),
        new Vector2Int(-1,  1),
        new Vector2Int( 0,  1),
    };

    void Start()
    {
        Generate();
    }

    void Generate()
    {
        cells.Clear();

        int qOffset = width / 2;
        int rOffset = height / 2;

        for (int q = -qOffset; q < width - qOffset; q++)
        {
            for (int r = -rOffset; r < height - rOffset; r++)
            {
                var cell = new HexCell(q, r);
                cells[new Vector2Int(q, r)] = cell;

                if (hexPrefab != null)
                {
                    Instantiate(
                        hexPrefab,
                        HexToWorld(q, r),
                        Quaternion.identity,
                        transform
                    );
                }
            }
        }
    }

    // ====== FLAT-TOP AXIAL → WORLD (2D XY) ======
    public Vector3 HexToWorld(int q, int r)
    {
        float x = hexSize * (1.5f * q);
        float y = hexSize * (Mathf.Sqrt(3f) * (r + q * 0.5f));
        return new Vector3(x, y, 0f);
    }

    // ====== ПРОВЕРКИ И ДОСТУП ======
    public bool HasCell(Vector2Int hex)
    {
        return cells.ContainsKey(hex);
    }

    public HexCell GetCell(Vector2Int hex)
    {
        cells.TryGetValue(hex, out var cell);
        return cell;
    }

    // ====== СОСЕДИ ХЕКСА ======
    public List<Vector2Int> GetNeighbors(Vector2Int hex)
    {
        List<Vector2Int> result = new();

        foreach (var dir in directions)
        {
            Vector2Int neighbor = hex + dir;

            if (cells.ContainsKey(neighbor))
                result.Add(neighbor);
        }

        return result;
    }

    // ====== WORLD → AXIAL (2D XY, FLAT-TOP) ======
    public Vector2Int WorldToHex(Vector3 world)
    {
        float q = (2f / 3f * world.x) / hexSize;
        float r = (-1f / 3f * world.x + Mathf.Sqrt(3f) / 3f * world.y) / hexSize;

        return CubeRound(q, r);
    }

    Vector2Int CubeRound(float q, float r)
    {
        float x = q;
        float z = r;
        float y = -x - z;

        int rx = Mathf.RoundToInt(x);
        int ry = Mathf.RoundToInt(y);
        int rz = Mathf.RoundToInt(z);

        float dx = Mathf.Abs(rx - x);
        float dy = Mathf.Abs(ry - y);
        float dz = Mathf.Abs(rz - z);

        if (dx > dy && dx > dz)
            rx = -ry - rz;
        else if (dy > dz)
            ry = -rx - rz;
        else
            rz = -rx - ry;

        return new Vector2Int(rx, rz);
    }

}

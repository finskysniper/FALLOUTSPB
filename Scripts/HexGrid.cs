using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float hexSize = 1f;

    public GameObject hexPrefab; // <-- prefab Hexagon Flat Top

    private Dictionary<Vector2Int, HexCell> cells =
        new Dictionary<Vector2Int, HexCell>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
        cells.Clear();

        for (int q = 0; q < width; q++)
        {
            for (int r = 0; r < height; r++)
            {
                HexCell cell = new HexCell(q, r);
                cells.Add(new Vector2Int(q, r), cell);

                CreateVisual(cell);
            }
        }
    }

    void CreateVisual(HexCell cell)
    {
        if (hexPrefab == null) return;

        GameObject hex = Instantiate(
            hexPrefab,
            HexToWorld(cell.q, cell.r),
            Quaternion.identity,
            transform
        );

        hex.transform.localScale = Vector3.one * hexSize;
    }

    // ====== AXIAL → WORLD (FLAT TOP) ======
    public Vector3 HexToWorld(int q, int r)
    {
        float x = hexSize * (3f / 2f * q);
        float z = hexSize * (Mathf.Sqrt(3f) * (r + q / 2f));
        return new Vector3(x, 0f, z);
    }

    // ====== WORLD → AXIAL ======
    public Vector2Int WorldToHex(Vector3 worldPos)
    {
        float q = (2f / 3f * worldPos.x) / hexSize;
        float r = (-1f / 3f * worldPos.x + Mathf.Sqrt(3f) / 3f * worldPos.z) / hexSize;

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

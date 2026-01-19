using System.Collections.Generic;
using UnityEngine;

public class HexGrid : MonoBehaviour
{
    public int width = 10;
    public int height = 10;
    public float hexSize = 1f;

    private Dictionary<Vector2Int, HexCell> cells =
        new Dictionary<Vector2Int, HexCell>();

    void Start()
    {
        GenerateGrid();
    }

    void GenerateGrid()
    {
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
        GameObject hex = GameObject.CreatePrimitive(PrimitiveType.Quad);
        hex.transform.position = HexToWorld(cell.q, cell.r);
        hex.transform.rotation = Quaternion.Euler(90, 0, 0);
        hex.transform.localScale = Vector3.one * hexSize;
    }

    Vector3 HexToWorld(int q, int r)
    {
        float x = hexSize * (Mathf.Sqrt(3f) * q + Mathf.Sqrt(3f) / 2f * r);
        float z = hexSize * (3f / 2f * r);
        return new Vector3(x, 0, z);
    }
}

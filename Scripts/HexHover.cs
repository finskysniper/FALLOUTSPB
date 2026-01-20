using UnityEngine;

public class HexHover : MonoBehaviour
{
    public HexGrid grid;
    public Transform highlight;

    void Update()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;

        Vector2Int hex = grid.WorldToHex(world);

        if (grid.HasCell(hex))
        {
            highlight.gameObject.SetActive(true);
            highlight.position = grid.HexToWorld(hex.x, hex.y);
        }
        else
        {
            highlight.gameObject.SetActive(false);
        }
    }

    public Vector2Int CurrentHex()
    {
        Vector3 world = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        world.z = 0f;
        return grid.WorldToHex(world);
    }
}

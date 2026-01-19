using UnityEngine;

public class HexHover : MonoBehaviour
{
    public HexGrid grid;
    public Transform highlight;

    void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            Vector2Int hex = grid.WorldToHex(worldPos);

            Vector3 hexWorld = grid.HexToWorld(hex.x, hex.y);
            highlight.position = hexWorld + Vector3.up * 0.01f;
            highlight.gameObject.SetActive(true);
        }
    }

    public Vector2Int GetHoveredHex()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, Vector3.zero);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 worldPos = ray.GetPoint(distance);
            return grid.WorldToHex(worldPos);
        }

        return Vector2Int.zero;
    }

    public void HideHighlight()
    {
        highlight.gameObject.SetActive(false);
    }
}

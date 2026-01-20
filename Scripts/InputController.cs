using UnityEngine;

public class InputController : MonoBehaviour
{
    public HexGrid grid;
    public HexHover hover;
    public PlayerController player;

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector2Int target = hover.CurrentHex();

            if (!grid.HasCell(target))
                return;

            var path = HexPathfinder.FindPath(
                grid,
                player.CurrentHex,
                target
            );

            if (path != null)
                player.MoveAlong(path);
        }
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public InputModeController inputMode;
    public HexHover hexHover;

    void Update()
    {
        if (inputMode.currentMode != CursorMode.Move)
            return;

        if (Input.GetMouseButtonDown(0)) // ЛКМ
        {
            Vector2Int targetHex = hexHover.GetHoveredHex();
            MoveToHex(targetHex);
        }
    }

    void MoveToHex(Vector2Int hex)
    {
        Debug.Log($"Идём в хекс: {hex}");
        // тут дальше:
        // 1. построение пути
        // 2. проверка AP
        // 3. пошаговое движение
    }
}

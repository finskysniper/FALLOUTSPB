using UnityEngine;

public class InputModeController : MonoBehaviour
{
    public CursorMode currentMode = CursorMode.Select;

    public HexHover hexHover;

    void Update()
    {
        if (Input.GetMouseButtonDown(1)) // ПКМ
        {
            ToggleMode();
        }
    }

    void ToggleMode()
    {
        if (currentMode == CursorMode.Select)
            SetMode(CursorMode.Move);
        else
            SetMode(CursorMode.Select);
    }

    void SetMode(CursorMode mode)
    {
        currentMode = mode;

        if (mode == CursorMode.Move)
        {
            Cursor.visible = false;
            hexHover.enabled = true;
        }
        else
        {
            Cursor.visible = true;
            hexHover.enabled = false;
            hexHover.HideHighlight();
        }
    }
}

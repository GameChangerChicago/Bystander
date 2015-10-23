using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour
{
    public Texture SelectCursor,
                    RegularCursor,
                    NoClickCursor;
    public int CursorIndex;

    void Awake()
    {
        Screen.showCursor = false;
        CursorIndex = 1;
    }

    void OnGUI()
    {
        GUI.depth = 0;

        if (CursorIndex == 0)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), SelectCursor);
        else if (CursorIndex == 1)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), RegularCursor);
        else if (CursorIndex == 2)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), NoClickCursor);
    }

    public void ChangeCursor(int cursorIndex)
    {
        CursorIndex = cursorIndex;
    }
}
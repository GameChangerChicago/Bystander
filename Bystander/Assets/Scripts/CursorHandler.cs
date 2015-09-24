using UnityEngine;
using System.Collections;

public class CursorHandler : MonoBehaviour
{
    public Texture SelectCursor,
                    RegularCursor,
                    NoClickCursor;
    private int _cursorIndex;

    void Awake()
    {
        Screen.showCursor = false;
        _cursorIndex = 1;
    }

    void OnGUI()
    {
        if (_cursorIndex == 0)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), SelectCursor);
        else if (_cursorIndex == 1)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), RegularCursor);
        else if (_cursorIndex == 2)
            GUI.DrawTexture(new Rect(Input.mousePosition.x, Screen.height - Input.mousePosition.y, Screen.height / 20, Screen.height / 20), NoClickCursor);
    }

    public void ChangeCursor(int cursorIndex)
    {
        Debug.Log(cursorIndex);
        _cursorIndex = cursorIndex;
        //Debug.Log("This is what i am typing right now");
    }
}
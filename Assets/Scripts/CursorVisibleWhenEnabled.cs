using UnityEngine;

public class CursorVisibleWhenEnabled : MonoBehaviour
{
    void Update()
    {
        Cursor.visible = true;
    }

    void OnDisable()
    {
        Cursor.visible = false;
    }
}

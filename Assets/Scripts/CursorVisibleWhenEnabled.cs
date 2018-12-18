using UnityEngine;

public class CursorVisibleWhenEnabled : MonoBehaviour
{
    void Awake()
    {
        #if UNITY_ANDROID || UNITY_IOS
        Destroy(this);
        #endif
    }
    void Update()
    {
        Cursor.visible = true;
    }

    void OnDisable()
    {
        #if !(UNITY_ANDROID || UNITY_IOS)
        Cursor.visible = false;
        #endif
    }
}

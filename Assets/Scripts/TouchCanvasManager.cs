using UnityEngine;

public class TouchCanvasManager : MonoBehaviour
{
    void Awake()
    {
        #if !(UNITY_ANDROID || UNITY_IOS)
        Destroy(gameObject);
        #endif
    }
}

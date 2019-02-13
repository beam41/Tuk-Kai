using UnityEngine;

public class GameTouchManager : MonoBehaviour
{
    public static GameTouchManager Instance { get; private set; } = null;

    [Header("Ediable Variable")]
    public float lenCountAsMove;
    public float timeCountAsTouch;

    [HideInInspector]
    public float movementX;
    [HideInInspector]
    public float movementY;
    [HideInInspector]
    public bool callPickup;
    private float touchDeltaTime;

    void Awake()
    {
        #if !(UNITY_ANDROID || UNITY_IOS)
        Destroy(this);
        #endif

        #region Singleton Loader
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        #endregion
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0 && !(GameManager.Instance.exitCanvas.activeInHierarchy || 
                                      GameManager.Instance.openCanvas.activeInHierarchy || 
                                      GameManager.Instance.selectCanvas.activeInHierarchy))
        {
            if (Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                Vector2 movement = Input.GetTouch(0).deltaPosition;
                if (movement.magnitude > lenCountAsMove)
                {
                    movementX = movement.x;
                    movementY = movement.y;
                }
            }
            if (Input.GetTouch(0).phase == TouchPhase.Began)
            {
                touchDeltaTime = 0;
            }
            touchDeltaTime += Time.deltaTime;
            if (Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                if (touchDeltaTime < timeCountAsTouch)
                {
                    callPickup = true;
                }
            }
        }
        else
        {
            touchDeltaTime = 1;
            callPickup = false;
        }
        
    }

    public void Pickup()
    {
        callPickup = true;
    }
}

using UnityEngine;

public class PlayerController : MonoBehaviour
{

    private CharacterController playerChar;
    
    #if !(UNITY_ANDROID || UNITY_IOS)
    private float moveHoz;
    private float moveVer;
    #endif
	// Use this for initialization
	void Start() 
    {
		playerChar = GetComponent<CharacterController>();
	}

    void Update()
    {
        if (GameManager.Instance.currSelectBall == null && !GameManager.Instance.exitCanvas.activeInHierarchy && GameManager.Instance.insideBall == "")
        {
            #if !(UNITY_ANDROID || UNITY_IOS)

            moveHoz = Input.GetAxis("Horizontal");
            moveVer = Input.GetAxis("Vertical");

            float mouseX = Input.GetAxisRaw("Mouse X");
            transform.eulerAngles = new Vector3(transform.eulerAngles.x, 
                                                transform.eulerAngles.y + mouseX * GameManager.Instance.turnSpeedY, 
                                                transform.eulerAngles.z);

            #elif UNITY_ANDROID || UNITY_IOS
            
            //transform.eulerAngles = new Vector3(transform.eulerAngles.x, 
                                               // transform.eulerAngles.y + GameTouchManager.Instance.movementX * GameManager.Instance.turnSpeedY, 
                                                // transform.eulerAngles.z);
            //GameTouchManager.Instance.movementX = 0;
            #endif
        }
        #if !(UNITY_ANDROID || UNITY_IOS)
        else
        {
            moveHoz = 0;
            moveVer = 0;
        }
        #endif
    }
    // Update is called once per frame
    void FixedUpdate()
    {
		playerChar.Move(Vector3.down * GameManager.Instance.gravity); // make character Grounded (and never jump again!)
        #if !(UNITY_ANDROID || UNITY_IOS)
        playerChar.Move(GameManager.Instance.moveSpeed * moveHoz * transform.right);
        playerChar.Move(GameManager.Instance.moveSpeed * moveVer * transform.forward);
        #endif
	}
}

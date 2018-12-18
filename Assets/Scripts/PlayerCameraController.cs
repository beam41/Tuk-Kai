using UnityEngine;

public class PlayerCameraController : MonoBehaviour
{
	// Update is called once per frame
	void Update()
    {
        
        if (GameManager.Instance.currSelectBall == null && !GameManager.Instance.exitCanvas.activeInHierarchy && GameManager.Instance.insideBall == "")
        {

            #if UNITY_STANDALONE || UNITY_WEBGL

		    float mouseY = Input.GetAxisRaw("Mouse Y");
            transform.localEulerAngles = new Vector3(ClamperX(mouseY * GameManager.Instance.turnSpeedX), 0, 0);
            #elif UNITY_ANDROID || UNITY_IOS
            transform.localEulerAngles = new Vector3(ClamperX(GameTouchManager.Instance.movementY * GameManager.Instance.turnSpeedX), 0, 0);
            GameTouchManager.Instance.movementY = 0;
            #endif
            #region Raycast
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.forward, out hit) && GameManager.Instance.currSelectBall == null)
            {
                if (hit.transform.CompareTag("Ball"))
                {
                    hit.transform.GetComponent<BallSelection>().hitting = true;
                    #if !(UNITY_ANDROID || UNITY_IOS)
                    if (Input.GetButtonDown("Fire1"))
                    {
                        GameManager.Instance.currSelectBall = hit.transform.gameObject;
                        GameManager.Instance.SelectionOn();
                    }
                    #elif UNITY_ANDROID || UNITY_IOS
                    if (GameTouchManager.Instance.callPickup)
                    {
                        GameManager.Instance.currSelectBall = hit.transform.gameObject;
                        GameManager.Instance.SelectionOn();
                        GameTouchManager.Instance.callPickup = false;
                    }
                    #endif
                }
            }
            #endregion
        }
    }

    float ClamperX(float pos)
    {
        pos = transform.localEulerAngles.x - pos;
        if (pos < 0)
        {
            pos = Mathf.Clamp(pos, -GameManager.Instance.cameraClampUp, 0f);
        }
        else if (pos < 180)
        {
            pos = Mathf.Clamp(pos, 0f, GameManager.Instance.cameraClampDown);
        }
        else if (pos >= 180)
        {
            pos = Mathf.Clamp(pos, 360f - GameManager.Instance.cameraClampUp, 360f);
        }
        return pos;
    }
}

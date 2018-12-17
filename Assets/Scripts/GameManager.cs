using System.Collections;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    #region declare variable thing
    private static GameManager instance = null;
    public static GameManager Instance
    {
        get {return instance;}
    }
    [Header("Ball Spawner")]
    public Transform ballKeeper;
    public GameObject theBall;
    public int maxBallCount;
    public int ballCount;
    public float pickupTransitionTime;

    [Header("Character Control")]
    public float gravity;
    public float moveSpeed;
    public float cameraClampUp;
    public float cameraClampDown;
    public float turnSpeedX;
    public float turnSpeedY;

    [Header("Float")]
    public float waterLevel;
    public float waterDrag;

    [Header("GameObject Reference")]
    public GameObject blurry;
    public GameObject crosshairCanvas;
    public GameObject selectCanvas;
    public PickedUpBall PickedUpBallScript;
    public GameObject exitCanvas;
    [HideInInspector]
    public GameObject currSelectBall;

    #endregion
    void Awake()
    {
        #region Singleton Loader
        if (Instance == null)
        {
            instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
        #endregion
        StartCoroutine("Addball");
        Cursor.visible = false;
    }

    private void Update()
    {
        if (currSelectBall != null)
        {
            blurry.SetActive(true);
            selectCanvas.SetActive(true);
            crosshairCanvas.SetActive(false);
            PickedUpBallScript.enabled = true;
        }

        if (Input.GetButtonDown("Cancel"))
        {
            if (!exitCanvas.activeInHierarchy)
                exitCanvas.SetActive(true);
            else
                exitCanvas.SetActive(false);
        }
    }
    IEnumerator Addball()
    {
        while (ballCount < maxBallCount)
        {
            GameObject newBall = Instantiate(theBall, new Vector3(Random.Range(-4f,4f),4,Random.Range(-4f,4f)), Quaternion.identity, ballKeeper);
            ballCount++;
            newBall.GetComponent<Renderer>().material.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
            if (ballCount < maxBallCount / 2)
                yield return null;
            else
                yield return new WaitForSeconds(2f);
        }
    }

    public void BtnReselect()
    {
        currSelectBall = null;
        blurry.SetActive(false);
        selectCanvas.SetActive(false);
        crosshairCanvas.SetActive(true);
    }

    public void BtnExitNo()
    {
        exitCanvas.SetActive(false);
    }

    public void BtnExitYes()
    {
        Debug.Log("Exit!");
        Application.Quit();
    }
}

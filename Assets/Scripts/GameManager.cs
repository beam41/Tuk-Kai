using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

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
    public GameObject openCanvas;
    [HideInInspector]
    public GameObject currSelectBall;
    [HideInInspector]
    public string insideBall;

    [Header("Add Things")]
    public string[] possibleThings;
    [Range(0f,1f)]
    public float[] possibleThingsChance;
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
        #endregion
        StartCoroutine("Addball");
        Cursor.visible = false;
    }

    private void Update()
    {
        if (Input.GetButtonDown("Cancel"))
        {
            if (!exitCanvas.activeInHierarchy)
            {
                exitCanvas.SetActive(true);
                blurry.SetActive(true);
            }
            else
            {
                BtnExitNo();
            }
        }
    }

    IEnumerator Addball()
    {
        while (ballCount < maxBallCount)
        {
            GameObject newBall = Instantiate(theBall, new Vector3(Random.Range(-4f,4f),4,Random.Range(-4f,4f)), Quaternion.identity, ballKeeper);
            ballCount++;
            #region random item
            for (int i = 0; i < possibleThingsChance.Length; i++)
            {
                if (Random.value <= possibleThingsChance[i])
                {
                    newBall.GetComponent<BallSelection>().inside = possibleThings[i];
                    break;
                }
            }
            #endregion
            newBall.GetComponent<Renderer>().material.color = new Color(Random.Range(0f,1f),Random.Range(0f,1f),Random.Range(0f,1f));
            if (ballCount < maxBallCount / 2)
                yield return null;
            else
                yield return new WaitForSeconds(2f);
        }
    }

    public void SelectionOn()
    {
        blurry.SetActive(true);
        selectCanvas.SetActive(true);
        crosshairCanvas.SetActive(false);
        PickedUpBallScript.enabled = true;
    }

    public void BtnReselect()
    {
        currSelectBall = null;
        blurry.SetActive(false);
        selectCanvas.SetActive(false);
        crosshairCanvas.SetActive(true);
    }

    public void BtnOpen()
    {
        selectCanvas.SetActive(false);
        insideBall = currSelectBall.GetComponent<BallSelection>().inside;
    }

    public void BtnExitNo()
    {
        exitCanvas.SetActive(false);
        if (!openCanvas.activeInHierarchy && !selectCanvas.activeInHierarchy)
            blurry.SetActive(false);
    }

    public void BtnExitYes()
    {
        Application.Quit();
        Debug.Log("Exit!");
    }

    public void BtnTryYes()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    public void BtnTryNo()
    {
        exitCanvas.SetActive(true);
    }
}

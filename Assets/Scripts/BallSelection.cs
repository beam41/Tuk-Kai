using UnityEngine;

public class BallSelection : MonoBehaviour
{
    public bool hitting;

    private Outline outliner;

    void Start()
    {
        outliner = GetComponent<Outline>();
    }
    // Update is called once per frame
    void Update()
    {
        if (hitting)
        {
            outliner.enabled = true;
            hitting = false;
        }
        else
        {
            outliner.enabled = false;
        }
    }
}

using UnityEngine;
using UnityEngine.UI;

public class ThingTextReplacer : MonoBehaviour
{
    private Text textThing;
    // Start is called before the first frame update
    void Start()
    {
        textThing = GetComponent<Text>();
        textThing.text = GameManager.Instance.insideBall;
    }
}

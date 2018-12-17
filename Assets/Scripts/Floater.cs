using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floater : MonoBehaviour
{

    private Rigidbody body;
    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        float force = 1f - (body.position.y - GameManager.Instance.waterLevel);
        if (force > 0f)
        {
            Vector3 lift = -Physics.gravity *(force - body.velocity.y * GameManager.Instance.waterDrag);
            body.AddForce(lift);
        }
    }
}

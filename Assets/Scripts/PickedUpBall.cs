using UnityEngine;

public class PickedUpBall : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 backupPosition;
    private Rigidbody select;
    private void OnEnable()
    {
        select = GameManager.Instance.currSelectBall.GetComponent<Rigidbody>();
        select.GetComponent<SphereCollider>().enabled = false;
        select.GetComponent<Rigidbody>().isKinematic = true;
        select.GetComponent<Rigidbody>().detectCollisions = false;
        backupPosition = select.position;
    }

    void Update()
    {
        if (GameManager.Instance.currSelectBall != null)
        {
            select.position = Vector3.SmoothDamp(select.position, transform.TransformPoint(Vector3.forward * 2), ref velocity, GameManager.Instance.pickupTransitionTime);

        }
        else if (select.position != backupPosition)
        {
            select.position = Vector3.SmoothDamp(select.position, backupPosition, ref velocity, GameManager.Instance.pickupTransitionTime);
        }
        else
        {
            enabled = false;
        }

    }

    private void OnDisable()
    {
        select.GetComponent<SphereCollider>().enabled = true;
        select.GetComponent<Rigidbody>().isKinematic = false;
        select.GetComponent<Rigidbody>().detectCollisions = true;
    }
}

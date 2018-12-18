using UnityEngine;

public class PickedUpBall : MonoBehaviour
{
    private Vector3 velocity;
    private Vector3 velocity2;
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
            if (GameManager.Instance.currSelectBall != select.gameObject)
            {
                select.GetComponent<SphereCollider>().enabled = true;
                select.GetComponent<Rigidbody>().isKinematic = false;
                select.GetComponent<Rigidbody>().detectCollisions = true;
                OnEnable();
            }
            if (GameManager.Instance.insideBall == "")
                select.position = Vector3.SmoothDamp(select.position, transform.TransformPoint(Vector3.forward * 2), ref velocity, GameManager.Instance.pickupTransitionTime);
            else
            {
                Destroy(select.gameObject);
                GameManager.Instance.openCanvas.SetActive(true);
                enabled = false;
            }     
        }
        else if (select.position != backupPosition)
            select.position = Vector3.SmoothDamp(select.position, backupPosition, ref velocity2, GameManager.Instance.pickupTransitionTime);
        else
        {
            select.GetComponent<SphereCollider>().enabled = true;
            select.GetComponent<Rigidbody>().isKinematic = false;
            select.GetComponent<Rigidbody>().detectCollisions = true;
            enabled = false;
        }
    }
}

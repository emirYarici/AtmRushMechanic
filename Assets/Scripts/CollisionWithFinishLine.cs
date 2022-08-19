using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithFinishLine : MonoBehaviour
{

    [SerializeField]Transform endStackPos;

    float waitTime = 0f;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "CollectedBall")

        {

            Collect.Instance.DeleteLastElement(other.gameObject);
            Debug.Log("girdi");
            StartCoroutine( WaitAndSendLeft(other.gameObject));
            Collect.Instance.isOnFinishLine = true;
            waitTime += 0.2f;
            Movement.Instance.StopSideMovement();

        }

        if(other.transform.tag == "Player")
        {
            Movement.Instance.StopForwardMovement();
        }
    }

    public IEnumerator WaitAndSendLeft(GameObject other)
    {
        Vector3 pos = other.transform.localPosition;
        pos.x += 20;
        yield return new WaitForSeconds(waitTime);
        other.transform.DOLocalMove(pos,2f);

    }
    /*
     * public void DeParent()
    {
        List<GameObject> ballstack = Collect.Instance.stack;
        for(int i = ballstack.Count - 1; i > 0; i++)
        {
            stack
        }
    }*/



}

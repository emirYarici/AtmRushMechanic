using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithWall : MonoBehaviour
{
    [SerializeField]GameObject Ball;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("CollectedBall"))
        {
                GameObject currentCreatedBall = Instantiate(Ball,transform);
                InstantiateBall(currentCreatedBall);
        }
    }

    private void InstantiateBall(GameObject ball)
    {
        ball.GetComponent<SphereCollider>().isTrigger = false;
        ball.transform.tag = "CollectedBall";
        ball.AddComponent<CollisionWithBall>();
        ball.AddComponent<CollisionWithWall>();
        ball.AddComponent<Rigidbody>();
        ball.GetComponent<Rigidbody>().isKinematic = true;
        Collect.Instance.Stack(ball, Collect.Instance.stack.Count - 1);
    }
}

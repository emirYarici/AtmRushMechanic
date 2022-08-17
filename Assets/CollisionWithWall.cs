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
            GameObject created_ball = Instantiate(Ball,gameObject.transform);
            other.GetComponent<BoxCollider>().isTrigger = false;
            created_ball.transform.tag = "CollectedBall";
            created_ball.AddComponent<CollisionWithBall>();
            created_ball.AddComponent<CollisionWithWall>();
            created_ball.AddComponent<Rigidbody>();
            created_ball.GetComponent<Rigidbody>().isKinematic = true;
            Collect.Instance.Stack(created_ball, Collect.Instance.stack.Count - 1);
            
        }
    }
}

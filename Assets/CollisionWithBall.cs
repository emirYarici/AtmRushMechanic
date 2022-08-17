using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithBall : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        
        if (other.gameObject.transform.tag == "CollectableBall") 
        {
            Debug.Log("if oncesi");

            if (!Collect.Instance.stack.Contains(other.gameObject))
            {
                Debug.Log("if sonrasý");
                other.GetComponent<BoxCollider>().isTrigger = false;
                other.gameObject.transform.tag = "CollectedBall";
                other.gameObject.AddComponent<CollisionWithBall>();
                other.gameObject.AddComponent<CollisionWithWall>();
                other.gameObject.AddComponent<Rigidbody>();
                other.gameObject.GetComponent<Rigidbody>().isKinematic = true;
                Collect.Instance.Stack(other.gameObject, Collect.Instance.stack.Count- 1);
            }
        }
    }
}

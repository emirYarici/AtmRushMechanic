using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithDestroyWall : MonoBehaviour
{
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
       
        Destroy(other.gameObject);
    }
}

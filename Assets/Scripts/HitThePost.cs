using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitThePost : MonoBehaviour
{
    
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("CollectedBall"))
        {
            Movement.Instance.stopForwardMovement = true;
            Movement.Instance.GoBackAndSpread();
            
           
        }
    }
}

using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithFinishLine : MonoBehaviour
{

    [SerializeField]Transform endStackPos;
    [SerializeField] GameObject rotator;
    float waitTime = 0f;
    float lastStackIndex;

    private void Start()
    {
        lastStackIndex = Collect.Instance.GetLastElementIndex();
    }
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "CollectedBall")
        {
            /*
             *  Collect.Instance.NormalizeStackPositions();
             //Collect.Instance.DeleteLastElement(other.gameObject);
             Collect.Instance.GotoFirstBallsPosition();
             Debug.Log(other.gameObject.transform.localPosition);
             Collect.Instance.isOnFinishLine = true;
             waitTime += 0.05f;
             Movement.Instance.StopSideMovement();*/
         }

         if(other.transform.tag == "Player")
         {
            Movement.Instance.StopMovement();
            Collect.Instance.GotoFirstBallsPosition(rotator);
            Collect.Instance.isOnFinishLine = true;
             Collect.Instance.NormalizeStackPositions();
         }
     }

   
   



        }

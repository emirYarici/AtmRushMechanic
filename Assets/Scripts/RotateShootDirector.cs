using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateShootDirector : MonoBehaviour
{
    Vector3 leftLimit = new Vector3(0, -15, 0);
    Vector3 rightLimit = new Vector3(0, 15, 0);
    float randomXValue1;
    float randomXValue2;
    // Start is called before the first frame update

    private void Start()
    {
        Rotate();
        randomXValue1 = Random.Range(0, 25);
        randomXValue2 = Random.Range(0, 25);
    }
 
    public void Rotate()
    {
        rightLimit.x=randomXValue1;
        leftLimit.x = randomXValue2;
        gameObject.transform.DOLocalRotate(rightLimit, 2f).OnComplete(() =>
        gameObject.transform.DOLocalRotate(leftLimit, 2f).OnComplete(() => Rotate()));
        randomXValue1 = Random.Range(0, 25);
        randomXValue2 = Random.Range(0, 25);
    }
    }


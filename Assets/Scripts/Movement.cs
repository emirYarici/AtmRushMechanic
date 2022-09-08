using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    bool stop_movement = false;
    public GameObject previous;
    public static Movement Instance;
    float forwardMoveSpeed = 20;
    private void Start()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (stop_movement != false)
        {
            ForwardMove();
        }
    }
    public void ForwardMove()
    {

        transform.position -= transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
    }



}

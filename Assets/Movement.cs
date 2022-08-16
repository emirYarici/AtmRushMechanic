using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    public float forwardMoveSpeed;
    public float horizontalSpeed;
    private Camera cam;
    float horizontalInput;
    GameObject firstCube;
    // Start is called before the first frame update
    void Start()
    {
        cam = Camera.main;
        firstCube = Collect.Instance.stack[0];
        Debug.Log(firstCube);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
        horizontalInput=Input.GetAxis("Horizontal");
        
        Vector3 pos = firstCube.transform.localPosition;
        pos.x -= horizontalInput * horizontalSpeed * Time.deltaTime;
        firstCube.transform.DOLocalMoveX(pos.x, Time.deltaTime);
    }
}

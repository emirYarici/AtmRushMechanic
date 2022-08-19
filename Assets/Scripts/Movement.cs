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
    private bool stopForwardMovement = false;
    private bool stopSideMovement = false;
    public static Movement Instance ;
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
  
    void Start()
    {
        cam = Camera.main;
        firstCube = Collect.Instance.stack[0];
        
    }

    // Update is called once per frame
    void Update()
    {

        if (stopForwardMovement == false)
        {
            transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
        }
        if(stopSideMovement == false) {
            horizontalInput = Input.GetAxis("Horizontal");
            Vector3 pos = firstCube.transform.localPosition;
            pos.x -= horizontalInput * horizontalSpeed * Time.deltaTime;
            firstCube.transform.DOLocalMoveX(pos.x, Time.deltaTime);
        }
            
        
    }

    public void StopForwardMovement()
    {
        stopForwardMovement = true;
        Collect.Instance.NormalizeStackPositions();
    }
    public void StopSideMovement()
    {
        stopSideMovement = true;
        
    }
    public void OneStepForward()
    {
        Vector3 newpos = transform.localPosition;
        newpos.z -= 1;//scale 1, bu nedenle 1 adým öne atarsa tam olur
        transform.localPosition = newpos;
    }
   
}

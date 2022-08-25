using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Movement : MonoBehaviour
{
    public float forwardMoveSpeed;
    public float horizontalSpeed;
    private Camera cam;
    GameObject firstCube;
    private bool stopForwardMovement = false;
    private bool stopSideMovement = false;
    Vector3 cursor_pos;
    Vector3 start_pos;
    public bool isTouching=false;
    public GameObject EndLine;
    public enum PLATFORM { PC, MOBILE };
    [SerializeField] PLATFORM platform = PLATFORM.PC;
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
        if (platform == PLATFORM.PC)
        {
            cursor_pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }
        else
        {
            if (Input.touchCount > 0) cursor_pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }

       if (stopForwardMovement == false)
       {
           transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
       }

        if (stopSideMovement == false)
        {

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            { // This is actions when finger/cursor hit screen
                start_pos = cursor_pos;
                isTouching = true;
            }
            if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) || Input.GetMouseButton(0))
            { // This is actions when finger/cursor pressed on screen
                HorizontalMove(cursor_pos);
            }
            if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)))
            { // This is actions when finger/cursor get out from screen
                isTouching = false;
            }

        }
   }
       /*if (stopForwardMovement == false)
       {
           transform.position -= Vector3.forward * forwardMoveSpeed * Time.deltaTime;//regular go forward
       }
       if(stopSideMovement == false) {
           horizontalInput = Input.GetAxis("Horizontal");
           Vector3 pos = firstCube.transform.localPosition;
           pos.x -= horizontalInput * horizontalSpeed * Time.deltaTime;
           firstCube.transform.DOLocalMoveX(pos.x, Time.deltaTime);
       }*/






 

    public void StopMovement()
    {
        stopSideMovement = true;
        stopForwardMovement = true;
    }

    public void HorizontalMove(Vector3 cursor_pos)
    {
       
        Vector3 pos = firstCube.transform.localPosition;
        pos.x = (start_pos - cursor_pos).x/50;
        if (pos.x >= 8.15f) { pos.x = 8.15f; }
        if (pos.x <= -8.15f) { pos.x = -8.15f; }
        firstCube.transform.DOLocalMoveX(pos.x, Time.deltaTime);
        Vector3 Parentpos = transform.position;
        Parentpos.x = 0;
        transform.position = Parentpos;
    }

   
   
}

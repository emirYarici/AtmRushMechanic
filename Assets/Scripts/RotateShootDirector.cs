using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class RotateShootDirector : MonoBehaviour
{
    Vector3 start_pos;
    public static RotateShootDirector Instance = null;
    Vector3 cursor_pos;
    Vector3 rotate_direction_horizontal = new Vector3(0,0,0);
    public Vector3 rotation;
    public enum PLATFORM { PC, MOBILE };
    [SerializeField] PLATFORM platform = PLATFORM.PC; // Controll type
    // Start is called before the first frame update
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }
    private void Update()
    {
        if (platform == PLATFORM.PC)
        {
            cursor_pos = Camera.main.ScreenToViewportPoint(Input.mousePosition) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }
        else
        {
            if (Input.touchCount > 0) cursor_pos = Camera.main.ScreenToViewportPoint(Input.GetTouch(0).position) * 900; // Instead of getting pixels, we are getting viewport coordinates which is resolution independent
        }
        

        

            if ((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began) || Input.GetMouseButtonDown(0))
            { // This is actions when finger/cursor hit screen
                start_pos = cursor_pos;
            }
            if ((Input.touchCount > 0 && (Input.GetTouch(0).phase == TouchPhase.Stationary || Input.GetTouch(0).phase == TouchPhase.Moved)) || Input.GetMouseButton(0))
            { // This is actions when finger/cursor pressed on screen
                Move(cursor_pos);
            }
            if (((Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended) || Input.GetMouseButtonUp(0)))
            { // This is actions when finger/cursor get out from screen
                    
            }

        
    }

    public void Move(Vector3 cursor_pos)
    {
        rotation.x = Mathf.Abs((start_pos - cursor_pos).y / 10);
        rotation.y = -(start_pos - cursor_pos).x/10;
        rotation.z = gameObject.transform.localRotation.z;
        gameObject.transform.DOLocalRotate(rotation, 0.1f);
    }

  


}


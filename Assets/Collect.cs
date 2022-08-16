using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Collect : MonoBehaviour
{
    public static Collect Instance;
    public float movementDelay = 0.25f;
    public  List<GameObject> stack = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
       if(Input.GetAxis("Horizontal") == 0)
        {
            NormalizeStackPositions();
        }
        else
        {
            MoveListElements();
        }
    }

    public void Stack(GameObject collectedObject, int index)
    {
        Debug.Log("giriyomu");
        collectedObject.transform.parent = transform;//make collected object go under this parent
        Vector3 newpos = stack[index].transform.localPosition;
        newpos.z -= 1;//scale 1, bu nedenle 1 adým öne atarsa tam olur
        collectedObject.transform.localPosition = newpos;
        stack.Add(collectedObject);
        StartCoroutine(MakeObjectsDoMexicanWave());
    }

    private IEnumerator MakeObjectsDoMexicanWave()
    {
        for( int i = stack.Count-1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(1, 1, 1);//þu anki scale
            scale *= 1.5f;//make 1.5x bigger

            stack[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            stack[index].transform.DOScale(new Vector3(1, 1, 1), 0.1f));
            yield return new WaitForSeconds(0.05f);
                
        }
    }


    private void MoveListElements()
    {
        for(int i = 1; i < stack.Count; i++)
        {
            Vector3 pos = stack[i].transform.localPosition;
            pos.x = stack[i - 1].transform.localPosition.x;
            stack[i].transform.DOLocalMove(pos, movementDelay);
        }
    }

    private void NormalizeStackPositions()
    {
        for (int i = 1; i < stack.Count; i++)
        {
            Vector3 pos = stack[i].transform.localPosition;
            pos.x = stack[0].transform.localPosition.x;
            stack[i].transform.DOLocalMove(pos, 0.70f);
        }
    }
}

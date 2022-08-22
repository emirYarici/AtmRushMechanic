using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class Collect : MonoBehaviour
{
    public static Collect Instance;
    public float movementDelay = 0.25f;
    public  List<GameObject> stack = new List<GameObject>();
    public bool isOnFinishLine = false;
    // Start is called before the first frame update
    void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (isOnFinishLine == false)
        {
            if (Movement.Instance.isTouching == false)
            {
                NormalizeStackPositions();
            }
            else
            {
                MoveListElements();
            }
        }
        //MoveListElements(); E�er �ndeki toplar�n bir ando sa�a veya sola ge�mesi istenmiyorsa 
    }

    public void Stack(GameObject collectedObject, int index)
    {

        collectedObject.transform.SetParent(transform);//make collected object go under this parent
        Vector3 newpos = stack[index].transform.localPosition;
        newpos.z -= 1;//scale 1, bu nedenle 1 ad�m �ne atarsa tam olur
        collectedObject.transform.localPosition = newpos;
        stack.Add(collectedObject);
        StartCoroutine(MakeObjectsDoMexicanWave());
    }

    private IEnumerator MakeObjectsDoMexicanWave()
    {
        for( int i = stack.Count-1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(5, 5, 5);//�u anki scale
            scale *= 1.5f;//make 1.5x bigger

            stack[index].transform.DOScale(scale, 0.1f).OnComplete(() =>
            stack[index].transform.DOScale(new Vector3(5, 5, 5), 0.1f));
            yield return new WaitForSeconds(0.10f);
                
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

    public void NormalizeStackPositions()
    {
        for (int i = 1; i < stack.Count; i++)
        {
            Vector3 pos = stack[i].transform.localPosition;
            pos.x = stack[0].transform.localPosition.x;
            stack[i].transform.DOLocalMove(pos, 0.70f);

        }
    }
    public void DeleteLastElement(GameObject other)
    {

        stack.Remove(other);
        
    }

}

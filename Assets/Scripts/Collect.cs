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
    public float[] forceAmount = { 1,2,3 };
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
        if(Movement.Instance.stopForwardMovement == false){
            MoveListElementsForward();
        }
        
        if (isOnFinishLine == false)
        {
            if (Movement.Instance.isTouching == false )
            {
                NormalizeStackPositions();
            }
            else
            {
                MoveListElements();
            }
        }
        //MoveListElements(); Eðer öndeki toplarýn bir ando saða veya sola geçmesi istenmiyorsa 
    }

    public void Stack(GameObject collectedObject, int index)
    {
        Vector3 pos = stack[index].transform.position;
        pos.z = stack[index].transform.position.z-2;
        collectedObject.transform.position = pos;
        pos.y = 0;
        stack.Add(collectedObject);
        StartCoroutine(MakeObjectsDoMexicanWave());
    }

    private IEnumerator MakeObjectsDoMexicanWave()
    {
        for( int i = stack.Count-1; i > 0; i--)
        {
            int index = i;
            Vector3 scale = new Vector3(5, 5, 5);//þu anki scale
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
            stack[i].transform.DOLocalMoveX(pos.x, movementDelay);
        }
    }
    private void MoveListElementsForward()
    {
        for(int i = 1; i < stack.Count; i++)
        {
            Vector3 pos = stack[i].transform.position;
            pos.z = stack[i - 1].transform.position.z-1;
            stack[i].transform.position = pos;
        }
    }
   
    public void NormalizeStackPositions()
    {
        for (int i = 1; i < stack.Count; i++)
        {
            Vector3 pos = stack[i].transform.localPosition;
            pos.x = stack[0].transform.localPosition.x;
            stack[i].transform.DOLocalMoveX(pos.x, 0.70f);
        }
    }
    public void DeleteLastElement(GameObject other)
    {

        stack.Remove(other);
        
    }

    public float GetLastElementIndex()
    {
        return stack.Count-1;
    }

    public void GotoFirstBallsPosition(GameObject rotator)
    {
        for (int i = stack.Count-1; i > 0; i--)
        {
            stack[i].transform.localPosition = stack[1].transform.localPosition;
            stack[i].transform.tag = "OnFinishLine";
            Debug.Log(stack[i].transform.localPosition);
        }
        isOnFinishLine = false;
        transform.DOLocalMoveX(0,0.70f).OnComplete(() => StartCoroutine(KickTheBalls(rotator)));
        
    }
    
    public IEnumerator KickTheBalls(GameObject rotator)
    {
        
        for (int i = 0; i< stack.Count; i++)
        {
            stack[i].transform.DOLocalMoveX(0, 0.70f);
        }

        
        rotator.SetActive(true);
        for (int i = 1; i < stack.Count;i++)
        {
            yield return new WaitForSeconds(2);
            int index = i;
         
            GameObject currentBall = stack[index];
            currentBall.gameObject.GetComponent<SphereCollider>().isTrigger = false;
            currentBall.GetComponent<Rigidbody>().isKinematic = false;
            /*RotateShootDirector.Instance.rotation.z = -10;
            Debug.Log(RotateShootDirector.Instance.rotation);
            currentBall.GetComponent<Rigidbody>().AddForce(RotateShootDirector.Instance.rotation * 30);*/
            Vector3 direction = rotator.transform.localPosition;
            Debug.Log(RotateShootDirector.Instance.rotation);
            direction.x -= RotateShootDirector.Instance.rotation.y;
            direction.y += RotateShootDirector.Instance.rotation.x;
            direction.z -= 50;

            currentBall.GetComponent<Rigidbody>().AddForce(direction * 30);
        }
    }
    public void EmptyStackSpread()
    {
        
        for(int i = stack.Count-1; i>0 ; i--)
        {
            
            GameObject ball = stack[i];
            stack.RemoveAt(i);
            ball.transform.tag = "CollectableBall";
            ball.transform.DOScale(new Vector3(5, 5, 5),0.1f);
            Vector3 targetpos = ball.transform.position;
            ball.transform.DOLocalJump( new Vector3(Random.Range(0, 6),0, Random.Range(targetpos.z, targetpos.z + 20)),0.5f,3,1f);
            Destroy(ball.GetComponent<CollisionWithBall>());
            Destroy(ball.GetComponent<Rigidbody>());
        }
        Movement.Instance.stopForwardMovement = false;
        
    }
}

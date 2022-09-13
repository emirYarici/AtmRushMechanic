using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
public class EnemyPlayerManager : MonoBehaviour
{
    [SerializeField]GameObject leftBarrier;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartSlidingTackle()
    {
        transform.DOLocalRotate(new Vector3(0, 0, 90), 0.2f * Time.deltaTime).OnComplete(() =>
        transform.DOLocalMoveX(leftBarrier.transform.position.x, 1.4f*Time.deltaTime).OnComplete(() =>
        transform.DOLocalRotate(Vector3.zero, 0.2f * Time.deltaTime)));
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("CollectedBall"))
        {
            Collect.Instance.EnemySlideToTheBall(other.gameObject);
        }
    }
    // Update is called once per frame

}

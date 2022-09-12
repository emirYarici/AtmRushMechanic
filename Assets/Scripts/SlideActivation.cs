using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlideActivation : MonoBehaviour
{
    [SerializeField]GameObject slider;
    // Start is called before the first frame update
    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.CompareTag("CollectedBall"))
        {
            slider.SetActive(true);
        }
    }
}

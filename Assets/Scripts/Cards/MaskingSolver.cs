using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MaskingSolver : MonoBehaviour
{
    public Transform target;
    public float rangeMax;

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            if (Vector3.Distance(target.transform.forward, Camera.main.transform.forward) > rangeMax)
            {
                target.gameObject.SetActive(false);
            }
            else
            {
                target.gameObject.SetActive(true);
            }
        }
    }
}
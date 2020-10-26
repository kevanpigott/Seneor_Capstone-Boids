using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SimpleAvoider : MonoBehaviour
{
    public GameObject obj2Avoid;
    public float maxVelocity;
    public float avoidanceStrength;
    public float avoidanceDistance;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * maxVelocity);
        avoid();
    }

    public float GetDistance(GameObject otherObject)
    {
        float dist;
        if (otherObject)
        {
            dist = Vector3.Distance(otherObject.transform.position, transform.position);
        }
        else
        {
            dist = 0;
        }
        return dist;
    }

    private void avoid()
    {
        if (GetDistance(obj2Avoid) <= avoidanceDistance)
        {
            Quaternion targetRotation = Quaternion.LookRotation(-obj2Avoid.transform.position - transform.position);
            var str = Mathf.Min(avoidanceStrength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
    }
}

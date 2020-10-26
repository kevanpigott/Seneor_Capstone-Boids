using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlockWatcher : MonoBehaviour
{
	public Transform boidSpawner;

    // Update is called once per frame
    void LateUpdate()
    {
    	if(boidSpawner)
    	{
        	Vector3 cameraTarget = boidSpawner.GetComponent<BoidSpawner>().flockCenter;
        	transform.LookAt(cameraTarget+boidSpawner.transform.position);
   		}
    }
}

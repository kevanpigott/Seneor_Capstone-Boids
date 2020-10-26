using UnityEngine;
using System.Collections;
using System.Collections.Generic;




public class BoidSpawner : MonoBehaviour
{
	public float minVelocity;
	public float maxVelocity;
	public int flockSize;
	public GameObject prefab;
	public Vector3 flockCenter;
	public GameObject[] boids;
	public float sightRadius;
    public GameObject Target;
    public GameObject Ray;
    // Start is called before the first frame update
    void Start()
    {
        
        boids = new GameObject[flockSize];
        for (var i = 0; i < flockSize; i++)
        {
            Vector3 position = new Vector3(
                Random.value * GetComponent<Collider>().bounds.size.x,
                Random.value * GetComponent<Collider>().bounds.size.y,
                Random.value * GetComponent<Collider>().bounds.size.z
            ) - GetComponent<Collider>().bounds.extents;

            GameObject boid = Instantiate(prefab, transform.position, transform.rotation) as GameObject;
            boid.transform.parent = transform;
            boid.transform.localPosition = position;
            boid.transform.eulerAngles = new Vector3(Random.Range(-180f, 180f), Random.Range(-180f, 180f), Random.Range(-180f, 180f));
            boid.GetComponent<BoidSingle>().SetController(gameObject);
            if (Target)
            {
                boid.GetComponent<BoidSingle>().SetTarget(Target);
            }
            boids[i] = boid;

            
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 tempCenter = Vector3.zero;

        foreach (GameObject boid in boids) {
        	tempCenter = tempCenter + boid.transform.localPosition;
        }

        flockCenter = tempCenter/(flockSize);

        //avoidMouse();

        avoidRay();
    }

    private void avoidMouse()
    {
        
        Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        Vector3 mousePos = Input.mousePosition;
        mousePos.z = Camera.main.nearClipPlane;
        worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        getNearBoids(worldPosition, 25f);
        Debug.Log("Position of Mouse = " + worldPosition);
        
    }

    private void avoidRay()
    {

        getNearBoids(Ray.transform.position, 15f);
        //Debug.Log("Position of Mouse = " + worldPosition);

    }

    private void getNearBoids(Vector3 position, float d)
    {
        foreach (GameObject boid in boids)
        {
            //if(boid.GetDistance(position) <= d)
            if(getScreenDistance(position, boid.transform.position) <= d)
            {
                //boid.steerAway(position);
                boid.GetComponent<BoidSingle>().steerAway(position);
                boid.GetComponent<BoidSingle>().rend.material.color = Color.red;
            } else
            {
                boid.GetComponent<BoidSingle>().rend.material.color = Color.blue;
            }
        }
    }

    private float getScreenDistance(Vector3 pos1, Vector3 pos2)
    {
        //accounts for 7 values, which it should not
        return Vector3.Distance(pos1, pos2);
    }
}

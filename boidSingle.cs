using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidSingle : MonoBehaviour
{
	private GameObject Controller;
	private bool init = false;
	private float minVelocity;
	private float maxVelocity;
	private float sightRadius;
	public int boidCount;
	public float cohesionStrength;
    public float targetStrength;
    public float seperationStrength;
    public float allignmentStrength;
    public float seperationRadiusPercent; //perecnt of sight radius that is seperation radius
    public bool boundries;

    private GameObject Target;
    private List<GameObject> LocalFlock;
    private bool hasTarget = false;
    public Renderer rend;

    

    // Start is called before the first frame update
    void Start()
    {
        rend = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
    	if(init)
    	{

            //get boids in sight. used in other methods, much faster than looping through boids in every method
            
            getLocalFlock();
            //getLocalFlockByCollider();
            //steer towards local average vector
            coherence();

            //seperation
            seperation();

            //alignment
            alignment();

            //steer towards target
            steer2Target();

            //function to add bounds to boids
            if (boundries)
            {
                //limByBounds(-50, 70, 0, 20, 10, 100);
                pacmanByBounds(-50, 70, 0, 20, 10, 100);
            }
            transform.Translate(Vector3.forward * Time.deltaTime * maxVelocity);

        }
    }

    //gets the boids that are within the sight radius by looping through all boids and adds it to global variable local flock.
    private void getLocalFlock()
    {
        LocalFlock = new List<GameObject>();
        //var i = 0;
        boidCount = 0;
        foreach (GameObject boid in Controller.GetComponent<BoidSpawner>().boids)
        {
            float dist = GetDistance(boid);
            if (dist != 0 && dist < sightRadius)
            {
                LocalFlock.Add(boid);
                boidCount++;
            }
        }
    }
    
    //another method of returning local flock, except it relies on collider spheres, suprisingly this is slower than getLocalFlock()
    private void getLocalFlockByCollider()
    {
        LocalFlock = new List<GameObject>();
        boidCount = 0;
        Collider[] hitColliders;
        hitColliders = Physics.OverlapSphere(GetComponent<Renderer>().bounds.center, sightRadius);
        foreach (var hitCollider in hitColliders)
        {
            float dist = GetDistance(hitCollider.gameObject);
            if (hitCollider.gameObject.GetComponent<BoidSingle>())
            {
                if (dist != 0 && dist < sightRadius)
                {
                    LocalFlock.Add(hitCollider.gameObject);
                    boidCount++;
                    //Debug.Log("Hit");
                }
            }
        }
    }
    //adds bounds to boids, boid is not allowed to leave screenspace.
    private void limByBounds(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        float xIndex = transform.position.x;
        float yIndex = transform.position.y;
        float zIndex = transform.position.z;

        /**
        if (yIndex < 0 || yIndex > 20) { rend.material.color = Color.green; }
        else { rend.material.color = Color.blue; }
        **/

        if (xIndex < xMin) { xIndex = xMin;}
        if (xIndex > xMax) { xIndex = xMax;}

        if (yIndex < yMin) { yIndex = yMin; }
        if (yIndex > yMax) { yIndex = yMax; }

        if (zIndex < zMin) { zIndex = zMin; }
        if (zIndex > zMax) { zIndex = zMax; }

        transform.position = new Vector3(xIndex, yIndex, zIndex);

        
    }

    // sets looped boounds like pacman, if boid exits screen, it is teleported to the opposite side of the screen
    private void pacmanByBounds(float xMin, float xMax, float yMin, float yMax, float zMin, float zMax)
    {
        float xIndex = transform.position.x;
        float yIndex = transform.position.y;
        float zIndex = transform.position.z;


        if (xIndex < xMin) { xIndex = xMax; }
        if (xIndex > xMax) { xIndex = xMin; }

        if (yIndex < yMin) { yIndex = yMin; }
        if (yIndex > yMax) { yIndex = yMax; }

        if (zIndex < zMin) { zIndex = zMax; }
        if (zIndex > zMax) { zIndex = zMin; }

        transform.position = new Vector3(xIndex, yIndex, zIndex);


    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("HIT!");
        init = false;
    }

    public void SetController (GameObject theController) 
    {
    	Controller = theController;
    	BoidSpawner Spawner = Controller.GetComponent<BoidSpawner>();
    	minVelocity = Spawner.minVelocity;
    	maxVelocity = Spawner.maxVelocity;
    	sightRadius = Spawner.sightRadius;
    	init = true;
    }


    public void SetTarget (GameObject obj)
    {
        Target = obj;
        hasTarget = true;
    }

    //Returns the distance between the current object, and another as a float.
    public float GetDistance(GameObject otherObject) {
    	float dist;
    	if(otherObject) {
    		dist = Vector3.Distance(otherObject.transform.position, transform.position);
    	} else {
    		dist = 0;
    	}
    	return dist;
    }

    //returns the amount of boids within a radius d of the current object.
    private int CountBoidsInDistance(float d)
    {
        int count = 0;

        foreach (GameObject boid in Controller.GetComponent<BoidSpawner>().boids)
        {
            float dist = GetDistance(boid);
            if (dist <= d && dist != 0)
            {
                count = count + 1;
            }
        }
        return count;
    }

    //conts the amount of boids within the pre defined sightRadius, and stores in boidCount Global Variable.
    private void countBoidsInSight(){
        boidCount = CountBoidsInDistance(sightRadius);
    }

    //calculates the average local position within the sight radius, stores in global variable averageLocalPosition.
    private Vector3 getAverageLocalPosition(){
    	Vector3 averageLocalPosition = Vector3.zero;
    	//int count = 0;
    	foreach (GameObject boid in LocalFlock) {
    			averageLocalPosition = averageLocalPosition + boid.transform.position;
    	}
    	return averageLocalPosition / (boidCount);
    }


    //coherence
    //each boid flies towards other boids gradually
    private void coherence() {
        if ((boidCount != 0) && (cohesionStrength != 0))
        {
            Quaternion targetRotation = Quaternion.LookRotation(getAverageLocalPosition() - transform.position);
            var str = Mathf.Min(cohesionStrength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
    }


    //seperation
    //each boid avoids running into other boids
    private void seperation()
    {
        if (seperationStrength != 0 && seperationRadiusPercent != 0 && boidCount != 0) {
            int count = 0;
            Vector3 averageLocalPosition = Vector3.zero;
            //for boids in certain radius
            foreach (GameObject boid in LocalFlock)
            {
                float dist = GetDistance(boid);
                if (dist < (sightRadius * seperationRadiusPercent) && dist != 0)
                {
                    averageLocalPosition = averageLocalPosition + boid.transform.position;
                    count++;
                }
            }
            if (count != 0)
            {
                averageLocalPosition = averageLocalPosition / count;
                Quaternion targetRotation = Quaternion.LookRotation(-averageLocalPosition + transform.position);
                var str = Mathf.Min(seperationStrength * Time.deltaTime, 1);
                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
            }
            //calculate heading of other boid
            //if boid is closing in? facing cur boid
            //add other boids reverse heading to sum
            //take average of vector sum
            //steer towards this average
        }
    }


    //alignment
    //each boid tries to match the speed and direction vector of other boids around it aka match average heading
    private void alignment()
    {
        if (allignmentStrength !=0 && boidCount !=0)
        {
            float avgx = 0;
            float avgy = 0;
            float avgz = 0;
            //float count = 0;
            foreach (GameObject boid in LocalFlock)
            {
                
                    avgx += boid.transform.rotation.eulerAngles.x;
                    avgy += boid.transform.rotation.eulerAngles.y;
                    avgz += boid.transform.rotation.eulerAngles.z;
                 
            }
            avgx = avgx / boidCount;
            avgy = avgy / boidCount;
            avgz = avgz / boidCount;


            //CAUSES GIMBAL LOCK
            Vector3 newAngles = new Vector3(avgx, avgy, avgz) * Time.deltaTime * allignmentStrength;
            transform.eulerAngles += newAngles;

        }
    }

    //target
    //steer towards a game object
    private void steer2Target()
    {
        if((hasTarget) && (targetStrength != 0))
        {
            Quaternion targetRotation = Quaternion.LookRotation(Target.transform.position - transform.position);
            var str = Mathf.Min(targetStrength * Time.deltaTime, 1);
            transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, str);
        }
    }

    //steers in the oppsoite direction of a vector, aka avoiding another object or boid
    public void steerAway(Vector3 Press)
    {
        transform.LookAt(Press);
        transform.Rotate(0, 180, 0);
        transform.Translate(Vector3.forward);
        //rend.material.color = Color.red;
    }


  
}

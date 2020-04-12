using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoidController2 : MonoBehaviour
{
    public int numBoids;
    public float minSpeed;
    public float maxSpeed;
    public float spredRange;
    public float maxRange = 0;
    public GameObject prefab_boid;
    private GameObject centerMark;
    public Camera cam;
    private float X, Y, Z;
    private GameObject[] boids;
    private Vector3 centerOfFlock = new Vector3(0,0,0);
    private Vector3 speedOfFlock;
    private Vector3 rotationOfFlock;

    // Start is called before the first frame update
    void Start()
    {
        // centerOfFlock = new Vector3(0, 0, 0);
        centerMark = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var RenderMark = centerMark.GetComponent<Renderer>();
        RenderMark.material.SetColor("_Color", Color.red);

        boids = new GameObject[numBoids];
        for (int i = 0; i < numBoids; i++)
        {
            GameObject boid = Instantiate(prefab_boid) as GameObject;
            X = Random.Range(-spredRange, spredRange) + boid.transform.position.x;
            Y = Random.Range(-spredRange, spredRange) + boid.transform.position.y;
            Z = Random.Range(-spredRange, spredRange) + boid.transform.position.z;
            boid.transform.position = new Vector3(X, Y, Z);

            RenderMark = boid.GetComponent<Renderer>();
            RenderMark.material.SetColor("_Color", Color.black);

            boid.GetComponent<boidSingle>().SetController(gameObject);

            boids[i] = boid;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 sumPos = new Vector3(0, 0, 0);
        Vector3 sumRot = new Vector3(0, 0, 0);
        //Vector3 sumSpd = new Vector3(0, 0, 0);
        for (int i = 0; i < numBoids; i++)
        {
            sumPos += boids[i].transform.position;
            sumRot += new Vector3(boids[i].transform.rotation.x, boids[i].transform.rotation.y, boids[i].transform.rotation.z);
            //sumSpd += boids[i].rigidbody.velocity;
        }
        centerOfFlock = sumPos / numBoids;
        rotationOfFlock = sumRot / numBoids;
        //speedOfFlock = sumSpd / numBoids;

        cam.transform.LookAt(centerOfFlock);
        centerMark.transform.position = centerOfFlock;
    }

    public Vector3 getCenter()
    {
        return centerOfFlock;
    }
    public Vector3 getRotation()
    {
        return rotationOfFlock;
    }
}

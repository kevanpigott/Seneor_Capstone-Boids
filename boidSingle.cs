using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boidSingle : MonoBehaviour
{
    private GameObject Controller;
    public float randTurnRangeMax;
    public float randTurnRangeMin;
    public float randTurnSpeedMin;
    public float randTurnSpeedMax;
    public float weight;
    private BoidController2 MasterController;
    private float minSpeed;
    private float maxSpeed;
    private float curSpeed;
    private float curTurnSpeed;
    private Vector3 rotVec;
    private bool instantiated = false;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //forward
        if (instantiated)
        {
            rotVec = getRotVec();
            curTurnSpeed = Random.Range(randTurnSpeedMin, randTurnSpeedMax);
            curSpeed = Random.Range(minSpeed, maxSpeed);

            if (isInRange())
            {
                transform.Rotate(rotVec * curTurnSpeed * Time.deltaTime);
            }
            else
            {
                transform.LookAt(MasterController.getCenter());
            }
            transform.Translate(Vector3.forward * curSpeed * Time.deltaTime);
        }

    }
    bool isInRange()
    {
        if (MasterController.maxRange == 0)
        {
            return true;
        }
        else
        {

            Vector3 distanceVec = (transform.position - MasterController.getCenter());
            float distance = distanceVec.magnitude;

            if (distance <= MasterController.maxRange) { return true; }
            else { return false; }
        }
        
    }
    private Vector3 getRotVec()
    {
        float X, Y, Z, i, j, k, a, b, c;
        i = Random.Range(randTurnRangeMin, randTurnRangeMax);
        j = Random.Range(randTurnRangeMin, randTurnRangeMax);
        k = Random.Range(randTurnRangeMin, randTurnRangeMax);

        a = MasterController.getRotation().x;
        b = MasterController.getRotation().y;
        c = MasterController.getRotation().z;

        if (weight != 0)
        {
            X = ((2 * (weight * rotVec.x + i)) + (2 * (1 - weight) * a)) / 2;
            Y = ((2 * (weight * rotVec.y + j)) + (2 * (1 - weight) * b)) / 2;
            Z = ((2 * (weight * rotVec.z + k)) + (2 * (1 - weight) * c)) / 2;
        } else
        {
            X = rotVec.x + i;
            Y = rotVec.y + j;
            Z = rotVec.z + k;
        }
        return new Vector3(X, Y, Z);
    }
    public void SetController (GameObject Master)
    {
        Controller = Master;
        MasterController = Controller.GetComponent<BoidController2>();
        minSpeed = MasterController.minSpeed;
        maxSpeed = MasterController.maxSpeed;
        instantiated = true;
    }
    public Vector3 getRotationVector()
    {
        return rotVec;
    }   
}

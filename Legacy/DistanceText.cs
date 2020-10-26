using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DistanceText : MonoBehaviour
{
    public GameObject master;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        int count = master.GetComponent<BoidSingle>().boidCount;
        GetComponent<TextMesh>().text = count.ToString();
    }
}

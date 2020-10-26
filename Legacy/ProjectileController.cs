using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private float speed;
    private bool init;
    
    // Start is called before the first frame update
    void Start()
    {
        init = false;
    }

    // Update is called once per frame
    void Update()
    {
        //if (init)
        //{
            transform.Translate(Vector3.up * Time.deltaTime * speed);

            if(this.transform.position.z > 100)
            {
                Destroy(this.gameObject);
            }
        //}
    }

    public void initiateProjectile(float this_speed)
    {
        speed = this_speed;
        transform.Rotate(90f, 0f, 0f);
        init = true;
    }
}

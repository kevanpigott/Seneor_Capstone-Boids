using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//[RequireComponent(typeof(AudioSource))]
public class ProjectileController : MonoBehaviour
{
    private float speed;
    //private bool init;
    //public AudioClip laser;
    //AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        //init = false;
       // audioSource = GetComponent<AudioSource>();
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
        //audioSource.PlayOneShot(laser, 0.7F);
        GetComponent<Renderer>().material.color = Color.red;
        speed = this_speed;
        transform.Rotate(90f, 0f, 0f);
        //init = true;
    }

   
}

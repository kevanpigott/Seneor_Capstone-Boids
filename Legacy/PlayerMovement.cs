using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public float speed;
    public float missleSpeed;
    public GameObject projectile;
    // Update is called once per frame
    void Update()
    {
        float x = Input.GetAxis("Horizontal");
        float z = Input.GetAxis("Vertical");

        Vector3 move = new Vector3(x, 0f, z);
        controller.Move(move * speed * Time.deltaTime);

        if(transform.position.x<-4)
        {
            Vector3 temp = Vector3.zero;
            temp.x = 39f;
            temp.y = transform.position.y;
            temp.z = transform.position.z;
            transform.position = temp;
        }

        if (transform.position.x > 40)
        {
            Vector3 temp = Vector3.zero;
            temp.x = -3f;
            temp.y = transform.position.y;
            temp.z = transform.position.z;
            transform.position = temp;
        }

        if (transform.position.z < 13)
        {
            Vector3 temp = Vector3.zero;
            temp.x = transform.position.z;
            temp.y = transform.position.y;
            temp.z = 49f;
            transform.position = temp;
        }

        if (transform.position.z > 50)
        {
            Vector3 temp = Vector3.zero;
            temp.x = transform.position.x;
            temp.y = transform.position.y;
            temp.z = 14f;
            transform.position = temp;
        }

        if (Input.GetKeyDown("space"))
        {
            GameObject missle = Instantiate(projectile, transform.position, transform.rotation) as GameObject;
            missle.GetComponent<ProjectileController>().initiateProjectile(missleSpeed);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseRayController : MonoBehaviour
{
    float zAxis = 60f;
    // Start is called before the first frame update
    void Start()
    {
        //this.GetComponent<MeshRenderer>().material.color = new Color(1.0f, 1.0f, 1.0f, 0.5f);
        Vector3 bias = new Vector3(0, 90, 90);
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward + bias, Camera.main.transform.up);
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = getMouseLocation();
        transform.position = target;//+ new Vector3(0.0f, 0.0f, 10f);

        Vector3 bias = new Vector3(0, 90, 90);
        transform.rotation = Quaternion.LookRotation(-Camera.main.transform.forward + bias, Camera.main.transform.up);
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    private Vector3 getMouseLocation()
    {   //attempt for mobile devices
        if (SystemInfo.deviceType == DeviceType.Handheld)
        {
            Touch touch = Input.GetTouch(0);
            /**
            return touch.position;
            **/
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);

            Vector3 mousePos = touch.position;
            mousePos.z = zAxis; //Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            return worldPosition;
        }
        else
        {

            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);

            Vector3 mousePos = Input.mousePosition;
            mousePos.z = zAxis; //Camera.main.nearClipPlane;
            worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

            return worldPosition;

        }
        /**
        Vector3 worldPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        worldPos.z = zAxis;

        return worldPos;
        **/
    }
}

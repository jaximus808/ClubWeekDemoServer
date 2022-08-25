using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    // Start is called before the first frame update

    public float translateMove; 

    private void camMovement()
    {
        if (Input.GetKey(KeyCode.I))
        {
            transform.Translate(Vector3.forward*translateMove*Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.K))
        {
            transform.Translate(Vector3.forward*-1*translateMove*Time.fixedDeltaTime);
        }

        if (Input.GetKey(KeyCode.J))
        {
            transform.Translate(Vector3.left*translateMove*Time.fixedDeltaTime);
        
        }

        if (Input.GetKey(KeyCode.L))
        {
            transform.Translate(Vector3.left*-1*translateMove*Time.fixedDeltaTime);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        camMovement();
    }
}

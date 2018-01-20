using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCam : MonoBehaviour
{
    //This is the first person cam controls
    public float speed;

    public float mouseSensX = 3.5f;//Sensativity x
    public float mouseSensY = 3.5f;//sensativity Y

    Transform camT;
    Rigidbody myRig;
    float vertLookRotation;

    // Use this for initialization
    void Start()
    {
        camT = Camera.main.transform;
        myRig = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {

        myRig.AddRelativeTorque(Vector3.up * Input.GetAxis("Mouse X") * mouseSensX);//do not multiply by time.deltatime as mouse input is frame independant

        myRig.AddRelativeTorque(Vector3.left * Input.GetAxis("Mouse Y") * mouseSensY);

        //vertLookRotation = Mathf.Clamp(vertLookRotation, -60, 60); I guess since this is a full 3D combat game, clamping the players view range is kinda pointless.

        //Movement controls
        if (Input.GetKey(KeyCode.W))
        {
            myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.S))
        {
            myRig.AddRelativeForce(new Vector3(0, 0, -speed) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.D))
        {
            myRig.AddRelativeForce(new Vector3(speed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.A))
        {
            myRig.AddRelativeForce(new Vector3(-speed, 0, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.Space))
        {
            myRig.AddRelativeForce(new Vector3(0, speed, 0) * Time.deltaTime);
        }
        if (Input.GetKey(KeyCode.C))
        {
            myRig.AddRelativeForce(new Vector3(0, -speed, 0) * Time.deltaTime);
        }


        //cam tilt controls
        if (Input.GetKey(KeyCode.E))
        {
            myRig.AddRelativeTorque(Vector3.forward * ((-speed / 3) * Time.deltaTime));
        }
        if (Input.GetKey(KeyCode.Q))
        {
            myRig.AddRelativeTorque(Vector3.forward * ((speed / 3) * Time.deltaTime));
        }
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public float SetSpeed;
    public float roll = 0.0f;
    public float pitch = 0.0f;
    public float yaw = 0.0f;

    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void pitchIncrement(Vector3 pitchInput, GameObject PlayerObject)
    {
        PlayerObject.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    void rollIncrement(Vector3 rotationInput, GameObject PlayerObject)
    {
        PlayerObject.transform.rotation = Quaternion.Euler(0,10,0);
    }

    void yawIncrement(float yaw)
    {

    }

    // Update is called once per frame
    void Update()
    {
   

        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        rb.AddForce(move * speed * Time.deltaTime);

    }
}
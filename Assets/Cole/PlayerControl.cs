using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControl : MonoBehaviour
{

    public Rigidbody rb;
    public float speed;
    public float SetSpeed;

    public float grow = 0;
    // Use this for initialization
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void pitchIncrement(float pitch)
    {
        
    }

    void rollIncrement(float roll)
    {
        gameObject.gameObject.rotation.y += float amount;
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
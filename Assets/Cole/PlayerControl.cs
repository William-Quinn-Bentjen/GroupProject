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

    void pitchIncrement(GameObject PlayerObject)
    {
        transform.Rotate(Vector3.forward * Time.deltaTime);
    }

    void rollIncrement(GameObject PlayerObject)
    {
        transform.Rotate(Vector3.right * Time.deltaTime);
    }

    void yawIncrement(GameObject PlayerObject)
    {
        transform.Rotate(Vector3.up * Time.deltaTime);
    }
    // Update is called once per frame
    void Update()
    {
        yawIncrement(gameObject);
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        Vector3 move = new Vector3(horizontal, 0, vertical);
        rb.AddForce(move * speed * Time.deltaTime);

    }
}
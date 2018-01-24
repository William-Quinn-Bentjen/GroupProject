using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissleAI : MonoBehaviour
{
    [Header("Core Variables")]
    public float speed;
    public float homingSensitivity;// 0 means less tracking, 1 is most tracking possible
    public float deathTimer = 30;
    public float speedLimmit = 200;
    public bool IsPlayerMIssile = true;

    [Header("RigidBody Target")]
    public Rigidbody target;

    [Header("Non-editable values")]
    public float metersPerSec;
    public float timer;

    Rigidbody myRig;
    
    Collider myCol;

    // Use this for initialization
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();

        if (IsPlayerMIssile)
        {
            target = GameObject.FindGameObjectWithTag("Enemy").GetComponent<Rigidbody>();
        }
        else
        {
            target = GameObject.FindGameObjectWithTag("Player").GetComponent<Rigidbody>();
        }

    }



    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            metersPerSec = myRig.velocity.magnitude;

            //Vector3 relativePos = target.position - transform.position;
            //relativePos = Quaternion.AngleAxis(-90, Vector3.up) * relativePos;//rotates 90 deg
            ////relativePos = Quaternion.AngleAxis(90, Vector3.right) * relativePos;//rotates 90 deg
            //relativePos = Quaternion.AngleAxis(180, Vector3.forward) * relativePos;//rotates 90 deg
            //Quaternion rotation = Quaternion.LookRotation(relativePos);
            //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);//the main bit of tracking.
            transform.right = Vector3.Slerp(transform.right, target.transform.position - transform.position, homingSensitivity);

            if (metersPerSec < speedLimmit)
            {
                myRig.AddRelativeForce((transform.right * speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
                                                                                   //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
            }

            if (timer >= deathTimer)
            {
                Destroy(gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void OnCollisionEnter(Collision col)
    {

        if (col.gameObject.name == "Target")
        {
            Debug.Log("detected Collision");
            Destroy(gameObject);
        }
    }
    
}
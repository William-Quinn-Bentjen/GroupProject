using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{

    [Header("Speed and sensitivity")]
    public float speed;
    public float homingSensitivity;// 0 means less tracking, 1 is most tracking possible

    [Header("RigidBody Target")]
    public Rigidbody target;

    [Header("Non-editable values")]
    public float metersPerSec;


    Rigidbody myRig;

    Collider myCol;

    // Use this for initialization
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();
    }

    public enum CurrentAction
    {
        Attack = 0, Advance = 1, Retreat = 2
    }

    public enum AttackType
    {
        Missile = 0, Railgun = 1, MachineGun = 2
    }

    public CurrentAction actionType = new CurrentAction();
    public AttackType attackActionType = new AttackType();
    // Update is called once per frame
    void Update()
    {
        //Measurments-----------------------------------------------------------------------------------------------------------------------------------------------------
        metersPerSec = myRig.velocity.magnitude;

        
        //----------------------------------------------------------------------------------------------------------------------------------------------------------------

        switch (actionType)
        {

        }

        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);//the main bit of tracking.

        myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate




    }

    void onCollisionEnter(Collider col)
    {

        if (col.gameObject.name == "Target")
        {
            Debug.Log("detected Collision");
            Destroy(this);
        }
    }
}

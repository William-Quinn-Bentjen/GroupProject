﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //enums--------------------------------------------------------------------------------
    public enum CurrentAction
    {
       IDLE = 0, ATTACK = 1, ADVANCE = 2, RETREAT = 4
    }

    public enum AttackType
    {
        Missile = 0, Railgun = 1, MachineGun = 2
    }

    public enum AImode
    {
        Agressive = 0, Reserved = 1
    }
    //Variables------------------------------------------------------------------------------

    [Header("Core Variables")]
    public float speed;
    public float homingSensitivity;// 0 means less tracking, 1 is most tracking possible
    public float minDist;//The Minimum dustance that the Ai tries to maintain between it and the player
    //Hp
    //AI setting: Agressive/Reserved
    

    [Header("RigidBody Target")]
    public Rigidbody target;

    [Header("Non-editable values")]
    public float metersPerSec;
    public float distfromTarg;
    
    [Header("Course of Actions")]
    public CurrentAction actionType = CurrentAction.IDLE;
    public AttackType attackActionType = new AttackType();
    public AImode mood = new AImode();

    Rigidbody myRig;

    Collider myCol;


    // Use this for initialization-----------------------------------------------------------------------
    void Start()
    {
        myRig = GetComponent<Rigidbody>();
        myCol = GetComponent<Collider>();

        //Select Ai mode!
        int temp = Random.Range(-5, 5);
        if (temp > 0)
        {
            mood = AImode.Agressive;
        }
        else
        {
            mood = AImode.Reserved;
        }
    }


    // Update is called once per frame
    void Update()
    {

        ////AI refresh- Needed in order to keep the Ai from getting confused
        //actionType = CurrentAction.IDLE;

        ////Measurments-----------------------------------------------------------------------------------------------------------------------------------------------------
        //metersPerSec = myRig.velocity.magnitude;
        //distfromTarg = (transform.position - target.position).magnitude;

        ////Logic--------------------------------------------------------------------------------------------------------------------------------------------------------
        //if (target == null)
        //{
        //    idle();
        //}
        //else
        //{
        //    /*
        //    TODO List:
        //    1. Set up weapons attack rneg logic
        //        ex: If target is "this dist" away then use "this gun"
        //    2. Set up AI mood
        //        ex: Random choose if the AI is agressive or reserved.
        //            Reserved: will use up all it's ammmo for each weapon type, by order of range, before advancing. Retreats if the player gets too close.
        //            Agressive: Will always advance towards the player fireing every availible weapon it can along the way. retreats only at low hp.

        //    */
        //    if (mood == AImode.Reserved)
        //    {



        //        //if(missileAmmo > 0 && distFromTarg > railgunRang)
        //        actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool
        //        attackActionType = AttackType.Missile;
        //        //else if(railgunAmmo > 25 && distFromTarg < railgunrange && distFromtarg > machineGunRange)
        //        actionType = actionType | CurrentAction.ATTACK;
        //        attackActionType = AttackType.Railgun;
        //        //else 
        //        actionType = actionType | CurrentAction.ATTACK;
        //        attackActionType = AttackType.MachineGun;


        //        //if(RailGunAmmo <= 0 || MissileAmmo <= 0)
        //        //if(RailgunAmmo <= 0 && MissileAmmo <=0 && distFromtarg > minDist)
        //        actionType = actionType | CurrentAction.ADVANCE;

        //        //else if(MissileAmmo <= 0 && RailGunAmmo >=0 && DistfromTarg > RailgunRange)
        //        actionType = actionType | CurrentAction.ADVANCE;

        //        //if(DistFromTarg < minDist)
        //        actionType = actionType = actionType | CurrentAction.RETREAT;
        //        actionType = actionType & ~CurrentAction.ADVANCE;
        //        //if((RailGunAmmo > 0 && distFromTarg < machinegunRange))
        //        actionType = actionType = actionType | CurrentAction.RETREAT;
        //        actionType = actionType & ~CurrentAction.ADVANCE;
        //        //if((MissileAmmo > 0) && distFromTarg < railGunRange)
        //        actionType = actionType = actionType | CurrentAction.RETREAT;
        //        actionType = actionType & ~CurrentAction.ADVANCE;




        //    }
        //    else
        //    {
        //        //if(Hp > maxHp)
        //            actionType = actionType | CurrentAction.ADVANCE;

        //            //if(missileAmmo > 0 && distFromTarg > railgunRang)
        //                actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool
        //                attackActionType = AttackType.Missile;
        //            //else if(railgunAmmo > 25 && distFromTarg < railgunrange && distFromtarg > machineGunRange)
        //                actionType = actionType | CurrentAction.ATTACK;
        //                attackActionType = AttackType.Railgun;
        //            //else 
        //                actionType = actionType | CurrentAction.ATTACK;
        //                attackActionType = AttackType.MachineGun;
        //        //else
        //            actionType = actionType = actionType | CurrentAction.RETREAT;
        //            //if(missileAmmo > 0 && distFromTarg > railgunRang)
        //                actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool
        //                attackActionType = AttackType.Missile;
        //            //else if(railgunAmmo > 25 && distFromTarg < railgunrange && distFromtarg > machineGunRange)
        //                actionType = actionType | CurrentAction.ATTACK;
        //                attackActionType = AttackType.Railgun;
        //            //else 
        //                actionType = actionType | CurrentAction.ATTACK;
        //                attackActionType = AttackType.MachineGun;

        //        //if(RailGunAmmo <= 0 || MissileAmmo <= 0)
        //        //if(RailgunAmmo <= 0 && MissileAmmo <=0 && distFromtarg > minDist)
        //        actionType = actionType | CurrentAction.ADVANCE;

        //        //else if(MissileAmmo <= 0 && RailGunAmmo >=0 && DistfromTarg > RailgunRange)
        //        actionType = actionType | CurrentAction.ADVANCE;
        //    }







        //    if (((actionType & ~CurrentAction.IDLE) == CurrentAction.IDLE) && ((actionType & ~CurrentAction.ADVANCE) != CurrentAction.ADVANCE) && ((actionType & ~CurrentAction.RETREAT) != CurrentAction.RETREAT))
        //    {
        //        idle();

        //    }

        //    if ((actionType & ~CurrentAction.ADVANCE) == CurrentAction.ADVANCE)
        //    {
        //        approachPlayer();
        //    }

        //    if ((actionType & ~CurrentAction.ATTACK) == CurrentAction.ATTACK)
        //    {
        //        atkType();
        //    }

        //    if ((actionType & ~CurrentAction.RETREAT) == CurrentAction.RETREAT)
        //    {
        //        avoidPlayer();
        //    }
        //}

        ////post logic----------------------------------------------------------------------------------------------------------------------------------------------------------





    }

    void idle()//Stops the enemy from moving in any meaningful direction.
    {
        Vector3 relativePos = target.position - new Vector3(Random.Range(-2,2), Random.Range(-2, 2), Random.Range(-2, 2));//picks a random direction
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);//the main bit of tracking.

        myRig.AddRelativeForce(new Vector3(0, 0, speed * 0.2f) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)//This version reduces speed to a little less than 1/2.
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void approachPlayer()
    {
        Vector3 relativePos = target.position - transform.position;
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);//the main bit of tracking.

        myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void avoidPlayer()
    {
        Vector3 relativePos = transform.position - target.position;//opposite of the approachPlayer function
        Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity * 0.5f);//the main bit of tracking.

        myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void atkType()
    {
        switch (attackActionType)
        {
            case AttackType.MachineGun:
                //get machine gun component
                break;
            case AttackType.Railgun:
                //get railgun component
                break;
            case AttackType.Missile:
                //get missile component
                break;
            default:
                //get machinegun component
                
                break;
        }
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
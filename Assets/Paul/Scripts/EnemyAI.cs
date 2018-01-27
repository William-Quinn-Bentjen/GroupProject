using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    //enums--------------------------------------------------------------------------------
    public enum CurrentAction//bitmapping
    {
       IDLE = 0, ATTACK = 1, ADVANCE = 2, RETREAT = 4//0000
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
    public Ship myShip; //Hp
    

    //AI setting: Agressive/Reserved
    

    [Header("RigidBody Target")]
    public Rigidbody target;

    [Header("Non-editable values")]
    public float metersPerSec;
    public float distfromTarg;
    Gun machineGun;
    Gun machineGun2;
    Gun machineGun3;
    Gun railGun;
    Gun missile;
    
    [Header("Course of Actions")]
    public CurrentAction actionType = CurrentAction.IDLE;//0000
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
        int temp = Random.Range(1, 5);//Problem with Reserved mode occured. Had to set this value so that it would always be more than 0. Any value less than 0 will result in the reserved behavior type.
        if (temp > 0)
        {
            mood = AImode.Agressive;
        }
        else
        {
            mood = AImode.Reserved;
        }

        foreach (Transform child in myShip.transform)
        {
            if (child.name == "MachineGun")
            {

                if (machineGun == null)
                {
                    machineGun = child.GetComponent<Gun>();
                }
                else if (machineGun2 == null)
                {
                    machineGun2 = child.GetComponent<Gun>();
                }
                else
                {
                    machineGun3 = child.GetComponent<Gun>();
                }

            }
            else if (child.name == "Railgun")
            {
                railGun  = child.GetComponent<Gun>();
            }
            else if (child.name == "MissileLauncher")
            {
                missile = child.GetComponent<Gun>();
            }
        }





    }


    // Update is called once per frame
    void Update()
    {
        Debug.Log(myShip.GetHP() + "HP");
        //AI refresh- Needed in order to keep the Ai from getting confused
        actionType = CurrentAction.IDLE;

        //Measurments-----------------------------------------------------------------------------------------------------------------------------------------------------
        metersPerSec = myRig.velocity.magnitude;
        distfromTarg = (transform.position - target.position).magnitude;

        //Logic--------------------------------------------------------------------------------------------------------------------------------------------------------
        if (target == null)
        {
            idle();
        }
        else
        {
            
            /*
            TODO List:
            1. Set up weapons attack rneg logic
                ex: If target is "this dist" away then use "this gun"
            2. Set up AI mood
                ex: Random choose if the AI is agressive or reserved.
                    Reserved: will use up all it's ammmo for each weapon type, by order of range, before advancing. Retreats if the player gets too close.
                    Agressive: Will always advance towards the player fireing every availible weapon it can along the way. retreats only at low hp.

            */
            if (mood == AImode.Reserved)
            {
                Debug.Log("decision made");
                //railGun range = 400
                //Machinegun range = 75

                if (missile.AmmoReserve > 0 && distfromTarg > 400)
                {
                    actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool//0100
                    attackActionType = AttackType.Missile;
                }
                else if (railGun.AmmoReserve > 25 && distfromTarg < 400 && distfromTarg > 75)
                {
                    actionType = actionType | CurrentAction.ATTACK;
                    attackActionType = AttackType.Railgun;
                }
                else
                {
                    actionType = actionType | CurrentAction.ATTACK;
                    attackActionType = AttackType.MachineGun;
                }

                if (railGun.AmmoReserve <= 0 || missile.AmmoReserve <= 0)
                {
                    if (railGun.AmmoReserve <= 0 && missile.AmmoReserve <= 0 && distfromTarg > minDist)
                    {
                        actionType = actionType | CurrentAction.ADVANCE;
                    }
                    else if (missile.AmmoReserve <= 0 && railGun.AmmoReserve >= 0 && distfromTarg > 400)
                    {
                        actionType = actionType | CurrentAction.ADVANCE;
                    }


                    if (distfromTarg < minDist)
                    {
                        actionType = actionType | CurrentAction.RETREAT;
                        actionType = actionType & ~CurrentAction.ADVANCE;
                    }

                    if ((railGun.AmmoReserve > 0 && distfromTarg < 75))
                    {
                        actionType = actionType | CurrentAction.RETREAT;
                        actionType = actionType & ~CurrentAction.ADVANCE;//0010  0010
                    }

                    if ((missile.AmmoReserve > 0) && distfromTarg < 400)
                    {
                        actionType = actionType | CurrentAction.RETREAT;
                        actionType = actionType & ~CurrentAction.ADVANCE;
                    }

                }





            }
            else
            {
                Debug.Log("Better decision made");
                if (myShip.GetHP() > myShip.GetMaxHP() / 6)
                {
                    
                    actionType = actionType | CurrentAction.ADVANCE;
                    if (missile.AmmoReserve > 0 && distfromTarg > 400)
                    {
                        actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool
                        attackActionType = AttackType.Missile;
                    }
                    else if (railGun.AmmoReserve > 25 && distfromTarg < 400 && distfromTarg > 75)
                    {
                        actionType = actionType | CurrentAction.ATTACK;
                        attackActionType = AttackType.Railgun;
                    }

                    else
                    {
                        actionType = actionType | CurrentAction.ATTACK;
                        attackActionType = AttackType.MachineGun;
                    }

                }
                else
                {
                    actionType = actionType | CurrentAction.RETREAT;
                    if (missile.AmmoReserve > 0 && distfromTarg > 400)
                    {
                        actionType = actionType | CurrentAction.ATTACK;//adds attack to the action pool
                        attackActionType = AttackType.Missile;
                    }
                    else if (railGun.AmmoReserve > 25 && distfromTarg < 400 && distfromTarg > 75)
                    {
                        actionType = actionType | CurrentAction.ATTACK;
                        attackActionType = AttackType.Railgun;
                    }
                    else
                    {
                        actionType = actionType | CurrentAction.ATTACK;
                        attackActionType = AttackType.MachineGun;
                    }


                    if (railGun.AmmoReserve <= 0 || missile.AmmoReserve <= 0)
                    {
                        if (railGun.AmmoReserve <= 0 && missile.AmmoReserve <= 0 && distfromTarg > minDist)
                        {
                            actionType = actionType | CurrentAction.ADVANCE;
                        }
                        else if (missile.AmmoReserve <= 0 && railGun.AmmoReserve >= 0 && distfromTarg > 400)
                        {
                            actionType = actionType | CurrentAction.ADVANCE;
                        }

                    }

                }

            }



            Debug.Log((int)actionType);



            if (((actionType & ~CurrentAction.IDLE) == CurrentAction.IDLE) && ((actionType & ~CurrentAction.ADVANCE) != CurrentAction.ADVANCE) && ((actionType & ~CurrentAction.RETREAT) != CurrentAction.RETREAT))
            {
                idle();
                Debug.Log("idle");
            }

            if ((actionType & CurrentAction.ADVANCE) == CurrentAction.ADVANCE)
            {
                Debug.Log("approachPlayer");
                approachPlayer();
            }

            if ((actionType & CurrentAction.ATTACK) == CurrentAction.ATTACK)
            {
                Debug.Log("AtkType");
                atkType();
            }

            if ((actionType & CurrentAction.RETREAT) == CurrentAction.RETREAT)
            {
                Debug.Log("retreat");
                avoidPlayer();
            }
        }

        //post logic----------------------------------------------------------------------------------------------------------------------------------------------------------
        Debug.Log(machineGun.AmmoReserve.ToString());
        Debug.Log(myShip.GetHP());



    }

    void idle()//Stops the enemy from moving in any meaningful direction.
    {
       // Vector3 relativePos = target.position - new Vector3(Random.Range(-2,2), Random.Range(-2, 2), Random.Range(-2, 2));//picks a random direction
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.right = Vector3.Slerp(transform.right, target.position - new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), Random.Range(-2, 2)), homingSensitivity);//the main bit of tracking.

        myRig.AddRelativeForce(new Vector3(0, 0, speed * 0.2f) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)//This version reduces speed to a little less than 1/2.
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void approachPlayer()
    {
        //Vector3 relativePos = target.position - transform.position;
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        //transform.rotation = Quaternion.Slerp(transform.rotation, rotation, homingSensitivity);//the main bit of tracking.

        //myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)



        transform.right = Vector3.Slerp(transform.right, target.transform.position - transform.position, homingSensitivity);


        myRig.AddRelativeForce((transform.right * speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
                                                                               //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
        


        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void avoidPlayer()
    {
       // Vector3 relativePos = transform.position - target.position;//opposite of the approachPlayer function
        //Quaternion rotation = Quaternion.LookRotation(relativePos);
        transform.right = Vector3.Slerp(transform.right, transform.position - target.position, homingSensitivity * 0.5f);//the main bit of tracking.

        myRig.AddRelativeForce((transform.right * speed) * Time.deltaTime);
        //myRig.AddRelativeForce(new Vector3(0, 0, speed) * Time.deltaTime);//This uses rigidbody and looks more real. Best setting for drag and mass is 0.5(drag) to 1(mass)
        //transform.Translate(0, 0, speed * Time.deltaTime, Space.Self);//This option does not use rigidbody but is far more accurate
    }

    void atkType()
    {
        switch (attackActionType)
        {
            case AttackType.MachineGun:
                //Debug.Log("Fireing Point Defense");
                machineGun.Fire();
                machineGun2.Fire();
                machineGun3.Fire();
                //get machine gun component
                break;
            case AttackType.Railgun:
                railGun.Fire();
                //get railgun component
                break;
            case AttackType.Missile:
                missile.Fire();
                //get missile component
                break;
            default:
                //get machinegun component
                
                break;
        }
    }

    public Gun GetSelectedWeapon()
    {
        foreach (Transform child in myShip.transform)
        {
            if (child.name == "MachineGun")
            {
                return child.GetComponent<Gun>();
            }
            else if (child.name == "Railgun")
            {
                return child.GetComponent<Gun>();
            }
            else if (child.name == "MissileLauncher")
            {
                return child.GetComponent<Gun>();
            }
        }
        //couldn't find gun
        Debug.Log("GetSelected called from gun selector ui and no gun could be found\nSelectedWeapon was ");
        return null;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretAi : MonoBehaviour
{
    public float turretRange;
    Transform turret;
    //Gun myGun;


    public float reactionTime;
    float timer;
    public GameObject Target;
    // Use this for initialization
    void Start()
    {
        turret = GetComponent<Transform>();
        //myGun = GetComponent<Gun>();
    }


    GameObject CheckMissileInRange()
    {
        Collider[] neighbours = Physics.OverlapSphere(transform.position, turretRange);
        foreach (Collider obj in neighbours)
        {
            if (obj.tag == "missile")
            {
                return obj.gameObject;
            }
            else if (obj.tag == "Player")
            {
                return obj.gameObject;
            }
            else if (obj.tag == "Asteroid")
            {
                return obj.gameObject;
            }


        }
        return null;
    }

    // Update is called once per frame
    void Update()
    {
        //listOfTargets.Add(GameObject.FindGameObjectsWithTag("missile"));
        if (timer <= 0)
        {
            Target = CheckMissileInRange();
            timer = reactionTime;//set to 0.2 for realistic delay
        }
        else
        {
            timer -= Time.deltaTime;
        }

        if (Target != null)
        {
            transform.LookAt(Target.transform.position);
        }

        //myGun.fire();
    }
}

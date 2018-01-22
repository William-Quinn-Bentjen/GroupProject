using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour
{
    public Rigidbody TheEnemy;
    public float repellDist; //Distance at which the asteroid repels the enemy; Should usually be greater than repulsoionStr. 100:15 is a good ratio
    public float repulsoionStr; //stength of the repulsion

    public float dropOffrepulsionMod;

    float distFromEnemy;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        distFromEnemy = (transform.position - TheEnemy.transform.position).magnitude;

        dropOffrepulsionMod = Mathf.Pow((repulsoionStr / distFromEnemy),2);//force over distance. Greater distance = less force. Vice versa.


        if (distFromEnemy < repellDist)
        {
            TheEnemy.AddForce(((TheEnemy.transform.position - transform.position) * (repulsoionStr * dropOffrepulsionMod)) * Time.deltaTime);
        }
    }
}

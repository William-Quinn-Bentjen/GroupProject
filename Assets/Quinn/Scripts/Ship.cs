using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Ship : MonoBehaviour {
    public ShipComponent Reactor;
    public ShipComponent Engines;
    public ShipComponent Bridge;
    public Gun MachineGun;
    public Gun Railgun;
    public Gun MissileLauncher;

    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnShipDestoyed;
    //value between 0 and 1
    private float Throttle = 0;
    private float HP;
    private float MaxHP;


    // Use this for initialization
    void Start () {
        UpdateHP();
	}
	
	// Update is called once per frame
	void Update () {
        Debug.Log(Throttle);
	}
    //used internally to make sure a value is between 0 and 1
    private float ThrottleValueCheck(float value)
    {
        //max
        if (value > 1)
        {
            return 1;
        }
        //min
        if (value < 0)
        {
            return 0;
        }
        //value was ok to be passed
        return value;
    }
    //used to easiely see the speed of a ship's rigidbody
    public float GetSpeed()
    {
        return gameObject.GetComponent<Rigidbody>().velocity.magnitude;
    }
    //sets the value of the throttle to a value between 0 and 1
    public void SetThrottle(float value)
    {
        Throttle = ThrottleValueCheck(value);
    }
    //increments the value of the throttle by ab value
    public void IncrementThrottle(float value)
    {
        Throttle = ThrottleValueCheck(Throttle + value);
    }
    //returns the throttle value
    public float GetThrottleValue()
    {
        return Throttle;
    }
    //updates the HP and max HP of the ship
    public void UpdateHP()
    {
        HP = Reactor.HealthPoints + Engines.HealthPoints + Bridge.HealthPoints;
        MaxHP = Reactor.MaxHealthPoints + Engines.MaxHealthPoints + Bridge.MaxHealthPoints;
    }
    //returns the max HP of the ship after refreshing the value
    public float GetMaxHP()
    {
        UpdateHP();
        return MaxHP;
    }
    //return current hp of the ship after refreshing the value
    public float GetHP()
    {
        UpdateHP();
        return HP;
    }
}

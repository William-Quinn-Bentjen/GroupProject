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

    private float HP;
    private float MaxHP;
    /*reactor
     * engines
     * bridge
     * weapons
     * /juice
     * lifesupport
     */


    // Use this for initialization
    void Start () {
        UpdateHP();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void UpdateHP()
    {
        HP = Reactor.HealthPoints + Engines.HealthPoints + Bridge.HealthPoints;
        MaxHP = Reactor.MaxHealthPoints + Engines.MaxHealthPoints + Bridge.MaxHealthPoints;
    }
    public float GetMaxHP()
    {
        return HP;
    }
    public float GetHP()
    {
        return MaxHP;
    }
}

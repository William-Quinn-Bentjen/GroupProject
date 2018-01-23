using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour {
    public float damage;
    //missile mesh
    public GameObject MissileObject;
    //hitbox for missile
    public TriggerZone MissileHitBox;
    //damage zone for missile explosion
    public TriggerZone SplashHitBox;

    public void Explode()
    {
        Debug.Log("BOOM");
        foreach (GameObject interactor in SplashHitBox.GetAllInteractors())
        {
            interactor.GetComponent<ShipComponent>().IncrementHealthPoints(damage);
        }
        Destroy(MissileObject);
    }

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (MissileHitBox.GetAllInteractors().Count > 0)
        {
            Explode();
        }
	}
}

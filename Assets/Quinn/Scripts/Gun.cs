using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
public enum ProjectileType
{
    Projectile,
    //Raycast,
    TriggerZone
}
public class Gun : MonoBehaviour {
    //public
    
    public int InMag = 5;
    public int MaxInMag = 5;
    public int AmmoReserve = 50;
    public bool InfiniteAmmoReserve = false;
    public int MaxAmmoReserve = 50;
    public float ReloadTime = 5; 
    public float FireRPM = 60; 
    public GameObject Projectile;
    public ProjectileType HitType;
    public Transform SpawnPos;
    public float TriggerZoneDamage = 0; //if using triggerzone will tell how much damage to do on hit
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnFire;
    public MyEvent OnEmptyMag;
    public MyEvent OnDryFire;
    public MyEvent OnReloadStart;
    public MyEvent OnReloadComplete;
    //private
    private float ReadyToFireTime; //chambered at 0 (used for RPM control)
    private bool Reloading; //reloading ammo?
    private float ReloadProgress; // seconds since the reload stared

    //functions
    public bool ChamberedCheck()
    {
        //gun mag in, had bullets and the gun has chambered a shot 
        if (Reloading == false && InMag > 0 && ReadyToFireTime >= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    public void Fire()
    {
        if (ChamberedCheck())
        {
            //fire
            //triggerzone
            if (HitType == ProjectileType.TriggerZone)
            {
                List<GameObject> Hit = Projectile.GetComponent<TriggerZone>().GetAllInteractors();
                foreach (GameObject Interactor in Hit)
                {
                    Interactor.gameObject.GetComponent<ShipComponent>().IncrementHealthPoints(TriggerZoneDamage);
                }
            }
            //projectile *(NEEDS TO BE DONE)
            if (HitType == ProjectileType.Projectile)
            {
                GameObject justFired = Instantiate(Projectile);
                justFired.transform.position = SpawnPos.transform.position;
                justFired.GetComponent<Rigidbody>().AddRelativeForce(Vector3.forward * 1250);
            }
            //fire
            InMag--;
            ReadyToFireTime = -1 / (FireRPM / 60);
            OnFire.Invoke();
            if (InMag <= 0)
            {
                OnEmptyMag.Invoke();
            }

        }
        else if (Reloading == false && InMag <= 0)
        {
            OnDryFire.Invoke();
        }
    }
    //starts the reload process and calls OnReloadStart's events
    public void ReloadStart()
    {
        if (Reloading != true && AmmoReserve > 0)
        {
            OnReloadStart.Invoke();
            ReloadProgress = 0;
            Reloading = true;
        }
    }
    private void ReadyToFireUpdate()
    {
        if (ChamberedCheck() == false)
        {
            ReadyToFireTime += Time.deltaTime;
        }
    }

    //called to keep track of time for realod 
    private void ReloadUpdate()
    {
        if (Reloading == true)
        {
            ReloadProgress += Time.deltaTime;
            if (ReloadProgress >= ReloadTime)
            {
                Reload();
            }
        }
    }
    //reloads the gun and calls OnReloadComplete's events
    private void Reload()
    {
        ReadyToFireTime = 0;
        Reloading = false;
        if (!InfiniteAmmoReserve)
        {
            //reload the ammo for non infinite guns
            if (AmmoReserve < MaxInMag)
            {
                InMag = AmmoReserve;
                AmmoReserve = 0;
            }
            else
            {
                InMag = MaxInMag;
                AmmoReserve -= MaxInMag;
            }
        }
        else
        {
            //reload ammo for infinite ammo gun
            InMag = MaxInMag;
        }
        OnReloadComplete.Invoke();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {
        //Fire();
        ReloadUpdate();
        ReadyToFireUpdate();
	}
}

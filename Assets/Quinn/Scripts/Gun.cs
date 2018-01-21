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
    public float TriggerZoneDamage = 0; //if using triggerzone will tell how much damage to do on hit
    public float ProjectileSpeed;
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
                List<GameObject> Hit = new List<GameObject>();
                foreach (GameObject Interactor in Projectile.GetComponent<TriggerZone>().GetInteractors(TriggerState.Enter))
                {
                    Hit.Add(Interactor.gameObject);
                }
                foreach (GameObject Interactor in Projectile.GetComponent<TriggerZone>().GetInteractors(TriggerState.Stay))
                {
                    Hit.Add(Interactor);
                }
                foreach (GameObject Interactor in Projectile.GetComponent<TriggerZone>().GetInteractors(TriggerState.Exit))
                {
                    Hit.Add(Interactor);
                }
                foreach (GameObject Interactor in Hit)
                {
                    Interactor.GetComponent<ShipComponent>().IncrementHealthPoints(TriggerZoneDamage);
                    Debug.Log(Interactor.GetComponent<ShipComponent>().HealthPoints);
                }
            }
            //projectile *(NEEDS TO BE DONE)

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
    private void ReadyToFireUpdate()
    {
        if (ChamberedCheck() == false)
        {
            ReadyToFireTime += Time.deltaTime;
        }
    }
    //starts the reload process and calls OnReloadStart's events
    public void ReloadStart()
    {
        if (Reloading != true)
        {
            OnReloadStart.Invoke();
            ReloadProgress = 0;
            Reloading = true;
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
        OnReloadComplete.Invoke();
    }

    // Use this for initialization
    void Start () {
	}
	
	// Update is called once per frame
	void Update () {

        Fire();
        ReloadUpdate();
        ReadyToFireUpdate();
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
public enum WeaponChoice
{
    Machine,
    Railgun,
    Missile
}
public class GunSelectorUI : MonoBehaviour
{
    public Ship PlayerShip;
    public WeaponChoice WeaponSelected = WeaponChoice.Machine;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnMachineGunSelect;
    public MyEvent OnRailgunSelect;
    public MyEvent OnMissileSelect;

    private bool firstUpdate = true;
    // Use this for initialization
    void Start()
    {
        firstUpdate = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (firstUpdate)
        {
            firstUpdate = false;
            SelectWeapon(WeaponSelected);
        }
    }

    public void SelectWeapon(WeaponChoice choice)
    {
        WeaponSelected = choice;
        if (choice == WeaponChoice.Machine)
        {
            OnMachineGunSelect.Invoke();
        }
        else if (choice == WeaponChoice.Railgun)
        {
            OnRailgunSelect.Invoke();
        }
        else if (choice == WeaponChoice.Missile)
        {
            OnMissileSelect.Invoke();
        }
    }
    public void SelectWeapon(string choice)
    {
        if (choice == "MachineGun")
        {
            SelectWeapon(WeaponChoice.Machine);
        }
        else if (choice == "Railgun")
        {
            SelectWeapon(WeaponChoice.Railgun);
        }
        else if (choice == "MissileLauncher")
        {
            SelectWeapon(WeaponChoice.Missile);
        }
        else
        {
            Debug.Log("Unrecognized string\n" + choice);
        }
    }

    public Gun GetSelectedWeapon()
    {
        foreach(Transform child in PlayerShip.transform)
        {
            if (child.name == "MachineGun" && WeaponSelected == WeaponChoice.Machine)
            {
                return child.GetComponent<Gun>();
            }
            else if (child.name == "Railgun" && WeaponSelected == WeaponChoice.Railgun)
            {
                return child.GetComponent<Gun>();
            }
            else if (child.name == "MissileLauncher" && WeaponSelected == WeaponChoice.Missile)
            {
                return child.GetComponent<Gun>();
            }
        }
        //couldn't find gun
        Debug.Log("GetSelected called from gun selector ui and no gun could be found\nSelectedWeapon was " + WeaponSelected);
        return null;
    }
}
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using System.Linq;

public enum WeaponChoice
{
    None,
    Unchanged,
    Machine,
    Railgun,
    Missile
}
public class GunSelectorUI : MonoBehaviour {
    public GameObject MachineGun;
    public GameObject RailGun;
    public GameObject MissileLauncher;
    public Button SelectMachineGun;
    public Button SelectRailGun;
    public Button SelectMissileLauncher;
    public WeaponChoice WeaponSelected = WeaponChoice.None;
	// Use this for initialization
	void Start () {
		if (WeaponSelected == WeaponChoice.Unchanged)
        {
            
            WeaponSelected = WeaponChoice.None;
        }
        WeaponSelection();
	}
	
	// Update is called once per frame
	void Update () {

	}
    public void WeaponSelection(WeaponChoice choice = WeaponChoice.None)
    {
        if (choice != WeaponChoice.Unchanged)
        {
            if (choice != WeaponSelected)
            {
                if (choice == WeaponChoice.Machine)
                {
                    
                }
                else if (choice == WeaponChoice.Railgun)
                {

                }
                else if (choice == WeaponChoice.Missile)
                {

                }
                else if (choice == WeaponChoice.None)
                {

                }
                WeaponSelected = choice;
            }
        }
    }
}

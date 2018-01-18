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
    public List<GameObject> SelectionList;
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
            WeaponSelected = choice;
            if (choice == WeaponChoice.Machine)
            {
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class PlayerControls : MonoBehaviour {
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnFire;
    public MyEvent OnReload;
    public MyEvent OnMachineGun;
    public MyEvent OnRailgun;
    public MyEvent OnMissileLauncher;
    public MyEvent OnScrollUp;
    public MyEvent OnScrollDown;
    public MyEvent OnYawUp;
    public MyEvent OnYawDown;
    public MyEvent OnPitchUp;
    public MyEvent OnPitchDown;
    public MyEvent OnRollUp;
    public MyEvent OnRollDown;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        ////buttons
        //Debug.Log("Fire " + Input.GetButton("Fire"));
        //Debug.Log("\nReload " + Input.GetButton("Reload"));
        //Debug.Log("\nMG " + Input.GetButton("MachineGun"));
        //Debug.Log("\nRG " + Input.GetButton("Railgun"));
        //Debug.Log("\nML " + Input.GetButton("MissileLauncher"));
        ////axis
        //Debug.Log("Mouse wheel " + Input.GetAxis("Mouse ScrollWheel"));
        //Debug.Log("\nYaw " + Input.GetAxis("Yaw"));
        //Debug.Log("\nPitch " + Input.GetAxis("Pitch"));
        //Debug.Log("\nRoll " + Input.GetAxis("Roll"));
        //buttons
        if (Input.GetButton("Fire"))
        {
            OnFire.Invoke();
        }
        //weapon
        if (Input.GetButton("Reload"))
        {
            OnReload.Invoke();
        }
        if (Input.GetButton("MachineGun"))
        {
            OnMachineGun.Invoke();
        }
        if (Input.GetButton("Railgun"))
        {
            OnRailgun.Invoke();
        }
        if (Input.GetButton("MissileLauncher"))
        {
            OnMissileLauncher.Invoke();
        }
        //speed
        if (Input.GetAxis("Mouse ScrollWheel") > 0)
        {
            OnScrollUp.Invoke();
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0)
        {
            OnScrollDown.Invoke();
        }
        //yaw
        if (Input.GetAxis("Yaw") > 0)
        {
            OnYawUp.Invoke();
        }
        else if (Input.GetAxis("Yaw") < 0)
        {
            OnYawDown.Invoke();
        }
        //pitch
        if (Input.GetAxis("Pitch") > 0)
        {
            OnPitchUp.Invoke();
        }
        else if (Input.GetAxis("Pitch") < 0)
        {
            OnPitchDown.Invoke();
        }
        //roll
        if (Input.GetAxis("Roll") > 0)
        {
            OnRollUp.Invoke();
        }
        else if (Input.GetAxis("Roll") < 0)
        {
            OnRollDown.Invoke();
        }
    }
}

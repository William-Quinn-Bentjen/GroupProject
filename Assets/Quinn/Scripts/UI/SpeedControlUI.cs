using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlUI : MonoBehaviour {
    //public
    public GameObject PlayerShip;
    public Slider Throttle;
    public Text Speedometer;
    public string SpeedometerPrefix = "Speed: ";
    public string SpeedometerSuffix = " m/s";
    //private
    private float SpeedometerValue;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetThrottle()
    {
        PlayerShip.GetComponent<Ship>().SetThrottle(Throttle.value);
    }

    //Sets the speedometer text to that speed
    public void SpeedometerSet()
    {
        Speedometer.text = PlayerShip.GetComponent<Ship>().GetSpeed().ToString("F2");
    }

    public float SpeedometerGet()
    {
        return PlayerShip.GetComponent<Ship>().GetSpeed();
    }
}

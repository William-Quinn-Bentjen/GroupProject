using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeedControlUI : MonoBehaviour {
    //public
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

    //Sets the speedometer text to that speed
    public void SpeedometerSet()
    {
        Speedometer.text = SpeedometerPrefix + Throttle.value + SpeedometerSuffix;
    }
}

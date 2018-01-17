using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadAnimationUI : MonoBehaviour {
    //getting tired (sorry for names)
    public Slider Slidelerp;
    public float Duration;
    private float currentTime;
    private bool lerping = false;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping)
        {
            currentTime -= Time.deltaTime;
            if (currentTime <= 0)
            {
                lerping = false;
            }
        }
	}
    public void on
}

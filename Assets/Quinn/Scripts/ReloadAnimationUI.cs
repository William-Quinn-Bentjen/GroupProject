using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadAnimationUI : MonoBehaviour {
    //getting tired (sorry for names)
    public Slider ReloadBar;
    public float Duration = 1;
    public Image readyImage;
    public Image fill;
    public Image background;
    //private
    private float currentTime;
    private bool lerping = false;
	// Use this for initialization
	void Start () {
        StartLerp();
	}
	
	// Update is called once per frame
	void Update () {
		if (lerping)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= Duration)
            {
                lerping = false;
                Fill();
            }
            else
            {
                ReloadBar.value = Mathf.Lerp(ReloadBar.minValue, ReloadBar.maxValue, currentTime / Duration);
            }
        }
	}
    //start reload animation with specified duratuion set to 0/leave to use the duration that we currently have
    public void StartLerp(float duration = 0)
    {
        Empty();
        if (duration > 0)
        {
            Duration = duration;
        }
        currentTime = 0;
        ReloadBar.value = ReloadBar.minValue;
        lerping = true;
    }
    private void Empty()
    {
        readyImage.enabled = false;
        fill.enabled = true;
        background.enabled = true;
    }
    private void Fill()
    {
        readyImage.enabled = true;
        fill.enabled = false;
        background.enabled = false;
    }
    
}

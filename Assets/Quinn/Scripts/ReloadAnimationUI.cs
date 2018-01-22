using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadAnimationUI : MonoBehaviour
{
    //getting tired (sorry for names)
    public Slider ReloadBar;
    public float Duration = 1;
    //private
    private float currentTime;
    private bool lerping = false;
    // Use this for initialization
    void Start()
    {
        StartLerp();
    }

    // Update is called once per frame
    void Update()
    {
        if (lerping)
        {
            currentTime += Time.deltaTime;
            if (currentTime >= Duration)
            {
                lerping = false;
                ReloadBar.value = ReloadBar.maxValue;
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
        if (duration > 0)
        {
            Duration = duration;
        }
        currentTime = 0;
        ReloadBar.value = ReloadBar.minValue;
        lerping = true;
    }
   // public void
        

}
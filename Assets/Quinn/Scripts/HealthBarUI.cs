using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarUI : MonoBehaviour {
    public Slider Bar;
    public string BarText = "Default";

    //private
    private Image FullImage;
    private Image fill;
    private Image background;
    private bool full;
    // Use this for initialization
    void Start () {
        foreach (Transform child in Bar.transform)
        {
            if (child.name == "Background")
            {
                background = child.GetComponent<Image>();
            }
            if (child.name == "Fill Area")
            {
                foreach (Transform childFill in child)
                {
                    if (childFill.name == "Fill")
                    {
                        fill = childFill.GetComponent<Image>();
                    }
                }
            }
            if (child.name == "FullImage")
            {
                FullImage = child.GetComponent<Image>();
            }
            if (child.name == "Text")
            {
                child.GetComponent<Text>().text = BarText;
            }
        }
        FullCheck();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    // a function that may be called after changing bar values
    public void FullCheck()
    {
        Debug.Log("check happened");
        if (Bar.value >= Bar.maxValue)
        {
            full = true;
            FullImage.enabled = true;
            fill.GetComponent<CanvasRenderer>().SetAlpha(0);
            background.GetComponent<CanvasRenderer>().SetAlpha(0);
        }
        else
        {
            if (full)
            {
                FullImage.enabled = false;
                fill.GetComponent<CanvasRenderer>().SetAlpha(1);
                background.GetComponent<CanvasRenderer>().SetAlpha(1);
            }
            full = false;
        }
    }
    public float GetValue()
    {
        return Bar.value;
    }
    public void SetValue(float value)
    {
        if (Bar.maxValue>= value && Bar.minValue <= value)
        {
            Debug.Log("i happened");
            Bar.value = value;
            FullCheck();
        }
    }
    public void IncrementValue(float value)
    {
        if (Bar.maxValue >= value && Bar.minValue <= value)
        {
            Debug.Log("i happened");
            Bar.value = value;
            FullCheck();
        }
    }
}

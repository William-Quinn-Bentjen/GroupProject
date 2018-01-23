using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ShipHealthBarUI : MonoBehaviour
{
    public Ship PlayerShip;
    public Slider Bar;
    public HPDisplayTypeBehavior HPDisplayType;
    public string PrefixText = "";
    public string BarText = "Default";
    public string SuffixText = "";
    [System.Serializable]
    public class MyEvent : UnityEvent { }

    public MyEvent OnEmpty;
    public MyEvent OnFull;

    //private
    private bool full;
    private Image FullImage;
    private Image fill;
    private Image background;
    private Text TextBar;

    // Use this for initialization
    void Start()
    {
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
                TextBar = child.GetComponent<Text>();
            }

        }
        Bar.maxValue = PlayerShip.GetMaxHP();
        RefreshValue();
        FullCheck();
    }

    // Update is called once per frame
    void Update()
    {
    }

    //sets value of bar
    public void RefreshValue()
    {
        Bar.value = PlayerShip.GetHP();
        FullCheck();
    }

    //sets textbar text if the bar (called in end of FullCheck)
    private void DisplayHp()
    {

        if (HPDisplayType == HPDisplayTypeBehavior.BarText)
        {
            TextBar.text = BarText;
        }
        else if (HPDisplayType == HPDisplayTypeBehavior.ValueBarText)
        {
            TextBar.text = Bar.value + BarText;
        }
        else if (HPDisplayType == HPDisplayTypeBehavior.ValueBarTextBarMaxValue)
        {
            TextBar.text = Bar.value + BarText + Bar.maxValue;
        }
        else if (HPDisplayType == HPDisplayTypeBehavior.ValueAsPercent)
        {
            TextBar.text = (Bar.value / Bar.maxValue * 100).ToString("F2");
        }
        TextBar.text = PrefixText + TextBar.text + SuffixText;
    }

    // a function that may be called after changing bar values
    private void FullCheck()
    {
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
            if (Bar.value <= Bar.minValue)
            {
                fill.GetComponent<CanvasRenderer>().SetAlpha(0);
            }
            else if (fill.GetComponent<CanvasRenderer>().GetAlpha() == 0)
            {
                fill.GetComponent<CanvasRenderer>().SetAlpha(1);
            }
            full = false;
        }
        DisplayHp();
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum TotalAmmoDisplayType
{
    Hide,
    TotalAmmo,
    AmmoNotInMag,
    Infinity
}

public class ReloadAnimationUI : MonoBehaviour {
    //getting tired (sorry for names)
    public Gun Weapon;
    public Slider ReloadBar;
    public TotalAmmoDisplayType TotalAmmo;
    //replace with gun later and change to private
    private float Duration = 1; // set to 1 in case no weapon is equiped
    private Image readyImage;
    private Image fill;
    private Image background;
    private Text totalAmmo;
    private Text magAmmo;
    //private
    private float currentTime;
    private bool lerping = false;
    private string infinity = "∞";

    public void SetAlpha(float value)
    {
        fill.GetComponent<CanvasRenderer>().SetAlpha(value);
        background.GetComponent<CanvasRenderer>().SetAlpha(value);
        readyImage.GetComponent<CanvasRenderer>().SetAlpha(value);
        totalAmmo.GetComponent<CanvasRenderer>().SetAlpha(value);
        magAmmo.GetComponent<CanvasRenderer>().SetAlpha(value);
    }
    public void RefreshGunInfo()
    {
        magAmmo.text = Weapon.InMag.ToString() + "/" + Weapon.MaxInMag.ToString();
        if (TotalAmmo == TotalAmmoDisplayType.Hide)
        {
            totalAmmo.text = "";
        }
        else if (TotalAmmo == TotalAmmoDisplayType.TotalAmmo)
        {
            totalAmmo.text = (Weapon.InMag + Weapon.AmmoReserve).ToString();
        }
        else if (TotalAmmo == TotalAmmoDisplayType.AmmoNotInMag)
        {
            totalAmmo.text = Weapon.AmmoReserve.ToString();
        }
        else if (TotalAmmo == TotalAmmoDisplayType.Infinity)
        {
            totalAmmo.text = infinity;
        }
        Duration = Weapon.ReloadTime;
    }

    // Use this for initialization
    void Start () {
        
        foreach (Transform child in gameObject.transform)
        {
            if (child.name == "Background")
            {
                background = child.GetComponent<Image>();
            }
            else if (child.name == "Fill Area")
            {
                foreach (Transform childFill in child)
                {
                    if (childFill.name == "Fill")
                    {
                        fill = childFill.GetComponent<Image>();
                    }
                }
            }
            else if (child.name == "ReadyImage")
            {
                readyImage = child.GetComponent<Image>();
            }
            else if (child.name == "TotalAmmo")
            {
                totalAmmo = child.GetComponent<Text>();
            }
            else if (child.name == "MagAmmo")
            {
                magAmmo = child.GetComponent<Text>();
            }
        }
        Fill();
        RefreshGunInfo();
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
        if (lerping == false)
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

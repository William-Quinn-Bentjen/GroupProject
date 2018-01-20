using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipComponent : MonoBehaviour
{
    public TriggerZone Hitbox;
    public bool Working = true;
    public float HealthPoints = 100;
    public float MaxHealthPoints = 100;
    [System.Serializable]
    public class MyEvent : UnityEvent { }
    public MyEvent OnHPChange;
    public MyEvent OnComponentDestroyed;
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
    }

    public void IncrementHealthPoints(float IncrementValue)
    {
        if(Working == true)
        {
            SetHealthPoints(HealthPoints+IncrementValue);
        }
    }
    public void SetHealthPoints(float SetValue)
    {
        HealthPoints = SetValue;
        if (HealthPoints > MaxHealthPoints)
        {
            HealthPoints = MaxHealthPoints;
        }
        if (HealthPoints <= 0)
        {
            HealthPoints = 0;
        }
        if (HealthPoints == 0 && Working == true)
        {
            Working = false;
        }
        else if (HealthPoints > 0 && Working == false)
        {
            Working = true;
        }
        OnHPChange.Invoke();
    }

}
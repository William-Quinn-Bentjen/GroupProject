using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ShipComponent : MonoBehaviour
{
    public TriggerZone Hitbox;
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
        SetHealthPoints(HealthPoints + IncrementValue);
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
        OnHPChange.Invoke();
    }

}
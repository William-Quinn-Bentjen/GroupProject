using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum TriggerState
{
    Untriggered,
    Enter,
    Stay,
    Exit
}
public enum OnOffToggle
{
    On,
    Off,
    Toggle
}
public class TriggerZone : MonoBehaviour
{

    //public 
    public bool VisibleAtStart = false;
    public List<string> InteractsWithTags;
    //private
    private List<GameObject> Entered;
    private List<GameObject> Stayed;
    private List<GameObject> Exited;
    private Renderer rend;

    // Use this for initialization
    void Start()
    {
        //set renderer to game objects renderer
        rend = GetComponent<Renderer>();
        //make invisible if invisible is checked.
        if (VisibleAtStart == false)
        {
            isVisible(OnOffToggle.Off);
        }
    }
    public void isVisible(OnOffToggle set)
    {
        if (set == OnOffToggle.On)
        {
            rend.enabled = true;
        }
        else if (set == OnOffToggle.Off)
        {
            rend.enabled = false;
        }
        else if (set == OnOffToggle.Toggle)
        {
            rend.enabled = !rend.enabled;
        }
        else
        {
            Debug.Log(set + " Not a supported visibility setting for a triggerzone");
        }
    }
    // Update is called once per frame
    void Update()
    {
    }

    //resets the lists when lateUpdate is called
    private void LateUpdate()
    {
        Entered = new List<GameObject>();
        Stayed = new List<GameObject>();
        Exited = new List<GameObject>();
    }

    //used for other things to access the object that triggered it
    public List<GameObject> GetInteractors(TriggerState triggerState)
    {
        if (triggerState == TriggerState.Enter)
        {
            return Entered;
        }
        else if (triggerState == TriggerState.Stay)
        {
            return Stayed;
        }
        else if (triggerState == TriggerState.Exit)
        {
            return Exited;
        }
        else
        {
            Debug.Log(triggerState + " Not supported a triggerstate was passed\nNeeds to be enter stay or exit");
            return new List<GameObject>();
        }
    }
    public List<GameObject> GetAllInteractors()
    {
        List<GameObject> retval = Entered;
        foreach (GameObject interactor in Stayed)
        {
            if (!retval.Contains(interactor))
            {
                retval.Add(interactor);
            }
        }
        foreach (GameObject interactor in Exited)
        {
            if (!retval.Contains(interactor))
            {
                retval.Add(interactor);
            }
        }
        return retval;
    }
    //on enter
    void OnTriggerEnter(Collider other)
    {
        foreach (string tagToCheck in InteractsWithTags)
        {
            if (other.tag == tagToCheck)
            {
                Entered.Add(other.gameObject);
            }
        }
    }
    //inside
    void OnTriggerStay(Collider other)
    {
        //button or currently in
        foreach (string tagToCheck in InteractsWithTags)
        {
            if (other.tag == tagToCheck)
            {
                Stayed.Add(other.gameObject);
            }
        }
    }
    //on exit
    void OnTriggerExit(Collider other)
    {
        //exit
        foreach (string tagToCheck in InteractsWithTags)
        {
            if (other.tag == tagToCheck)
            {
                Exited.Add(other.gameObject);
            }
        }
    }
}
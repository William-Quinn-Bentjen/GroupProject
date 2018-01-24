using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {
    public bool CursorLockedAtStart = false;

	// Use this for initialization
	void Start () {
        CursorLock(CursorLockedAtStart);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void CursorLock(bool enabled = false)
    {
        //locks or unlocks the cursor
        if (enabled)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        else
        {
            Cursor.lockState = CursorLockMode.None;
        }
    }

    //Load scenes
    public void LoadMainMenu()
    {
        LoadScene("MainMenu");
    }
    public void LoadWin()
    {
        LoadScene("Victory");
    }
    public void LoadLoss()
    {
        LoadScene("Defeat");
    }
    public void LoadCredits()
    {
        LoadScene("Credits");
    }
    public void LoadPlay()
    {
        LoadScene("ShipUITest");
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }
    //close game
    public void QuitGame()
    {
        Application.Quit();
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

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
        LoadScene("MainMenu");
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
    public void QuitGame()
    {
        Application.Quit();
    }
}

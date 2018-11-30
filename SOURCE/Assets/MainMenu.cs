using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("Loading", LoadSceneMode.Single);
        }
    }

    public void StartGame()
    {
        SceneManager.LoadScene("Loading", LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}

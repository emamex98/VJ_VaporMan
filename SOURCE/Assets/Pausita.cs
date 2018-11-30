using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pausita : MonoBehaviour {

    public bool isPaused;
    public GameObject canvasMenu;
   	// Use this for initialization
	void Start () {

        isPaused = false;
		
	}
	
	// Update is called once per frame
	void Update () {

        if (isPaused)
        {
            canvasMenu.SetActive(true);
            Time.timeScale = 0f;
        }
        else
        {
            canvasMenu.SetActive(false);
            Time.timeScale = 1f;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            isPaused = !isPaused;
        }
	}
    public void Resume()
    {
        isPaused = false;
    }
    public void Quit()
    {
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
    }
}

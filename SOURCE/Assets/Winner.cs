using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Winner : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(carga());
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);
        }
    }

    IEnumerator carga()
    {
        yield return new WaitForSeconds(10.0f);
        SceneManager.LoadScene("MainMenu", LoadSceneMode.Single);

    }
}

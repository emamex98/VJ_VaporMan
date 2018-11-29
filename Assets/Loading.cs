﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Loading : MonoBehaviour {

	// Use this for initialization
	void Start () {
        StartCoroutine(loadScene());

    }
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator loadScene(){
        yield return new WaitForSeconds(3);
        SceneManager.LoadScene("SampleScene");
    }
}
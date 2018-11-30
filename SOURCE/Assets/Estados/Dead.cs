using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dead : MonoBehaviour {

    private GameObject pared;
	// Use this for initialization
	void Start () {
        
        pared = GameObject.Find("wall");
        Destroy(pared);
        Destroy(gameObject);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}

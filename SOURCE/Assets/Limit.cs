using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Limit : MonoBehaviour {

    Vector3 original = new Vector3(-8.99f, -2.92f, 0);
    Vector3 cero = new Vector3(0, 0, 0);

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 9){
            Movimiento.vidas = 5;
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}

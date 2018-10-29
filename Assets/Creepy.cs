using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creepy : MonoBehaviour {

    private GameObject target;
    private Vector2 posOriginal;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("womba");
        posOriginal = gameObject.transform.position;
	}
	
	// Update is called once per frame
	void Update () {

        transform.position = Vector2.MoveTowards(transform.position,
               target.transform.position,
               5 * Time.deltaTime);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 13)
        {
            transform.position = posOriginal;
        }
    }
}

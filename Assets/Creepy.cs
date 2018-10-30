using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Creepy : MonoBehaviour {

    private GameObject target;
    private Vector2 posOriginal;
    private IEnumerator ie, ie2;
    private Coroutine c;
    private bool frozen;

	// Use this for initialization
	void Start () {
        target = GameObject.Find("womba");
        posOriginal = gameObject.transform.position;
        ie2 = froze();
        frozen = false;
	}
	
	// Update is called once per frame
	void Update () {
        if(frozen == false)
        {
            transform.position = Vector2.MoveTowards(transform.position,
               target.transform.position,
               5 * Time.deltaTime);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 13)
        {
            transform.position = posOriginal;
        }
        if(collision.gameObject.layer == 15 && frozen == false)
        {
            
            StartCoroutine(ie2);
            frozen = true;
        }
        if (collision.gameObject.layer == 15 )
        {
            print(frozen);
        }
    }

    IEnumerator froze()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            print("hola");
            frozen = false;
            StopCoroutine("froze");
        }
    }

}

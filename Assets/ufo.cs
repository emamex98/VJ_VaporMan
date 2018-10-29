using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour {

    public GameObject u;
    public Transform fo;
    private GameObject targetUfo;
    private int count, countrut;
    private IEnumerator coroutine;
    private IEnumerator ie, ie2;
    private Coroutine c;
    private bool frozen;

    private bool visible = false;

	// Use this for initialization
	void Start () {
        fo = GetComponent<Transform>();
        targetUfo = GameObject.Find("womba");
        ie = shoot();
        ie2 = froze();
        frozen = false;
    }

    // Update is called once per frame
    void Update() {
        if (frozen == false)
        {
            Vector3 dir = targetUfo.transform.position - fo.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }

    }

    private void OnBecameVisible()
    {
        //if(Camera.current.name == "MainCamera"){
        StartCoroutine(shoot());
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15 && frozen == false)
        {

            StartCoroutine(ie2);
            frozen = true;
        }
    }


    IEnumerator shoot()
    {
        while (true)
        {
            
            yield return new WaitForSeconds(3);
            if (!frozen) {
                Instantiate<GameObject>(u, fo.position, transform.rotation);
            }
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

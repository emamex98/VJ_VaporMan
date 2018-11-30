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
    private bool frozen,disp;

    private bool visible = false;

	// Use this for initialization
	void Start () {
        fo = GetComponent<Transform>();
        targetUfo = GameObject.Find("womba");
        ie = shoot();
        ie2 = froze();
        frozen = false;
        disp = false;
    }

    // Update is called once per frame
    void Update() {
        if (frozen == false)
        {
            float d = Vector2.Distance(targetUfo.transform.position, transform.position);
            if (d < 20)
            {
                Vector3 dir = targetUfo.transform.position - fo.position;
                float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
                transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
                if (!disp)
                {
                    StartCoroutine(ie);
                    disp = true;
                }
            }
            else
            {
                StopCoroutine(ie);
                disp = false;
            }
        }

    }

    private void OnBecameVisible()
    {
        //if(Camera.current.name == "MainCamera"){
        //
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

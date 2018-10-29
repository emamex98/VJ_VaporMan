using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ufo : MonoBehaviour {

    public GameObject u;
    public Transform fo;
    private GameObject targetUfo;
    private int count, countrut;
    private IEnumerator coroutine;

    private bool visible = false;

	// Use this for initialization
	void Start () {
        fo = GetComponent<Transform>();
        targetUfo = GameObject.Find("womba");
	}

    // Update is called once per frame
    void Update() {
        Vector3 dir = targetUfo.transform.position - fo.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg + 270;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);

    }

    private void OnBecameVisible()
    {
        //if(Camera.current.name == "MainCamera"){
        StartCoroutine(shoot());
    }


    IEnumerator shoot()
    {
        while (true)
        {
            yield return new WaitForSeconds(3);
            Instantiate<GameObject>(u, fo.position, transform.rotation);
        }
    }


}

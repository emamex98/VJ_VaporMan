using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour {

    private GameObject fire;
    private GameObject bossIns;
    private IEnumerator ie;
    private Jefe jefe;

	// Use this for initialization
	void Start () {
        //fire = GameObject.Find("FireBoss");
        //jefe = new Jefe();
        //bossIns = GameObject.Find("BossIns");
        //ie = attacking();
        //StartCoroutine(ie);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    IEnumerator attacking()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            //fire = GameObject.Find("FireBoss(Clone)");
            //Instantiate<GameObject>(fire, bossIns.transform.position, bossIns.transform.rotation);
        }
    }
}

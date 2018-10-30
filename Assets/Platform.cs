using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour {

    private int cambiar;
    private Vector2 posOriginal;
    private IEnumerator ie;
	// Use this for initialization
	void Start () {
        posOriginal = transform.position;
        cambiar = 1;
        ie = checarD();
        StartCoroutine(ie);
	}
	
	// Update is called once per frame
	void Update () {
        transform.Translate(cambiar*Time.deltaTime * 6, 0, 0, Space.World);

	}

    IEnumerator checarD()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            float d = Vector2.Distance(transform.position, posOriginal);
            if (d > 6)
            {
                cambiar *= -1; 
            }
        }
    }
}

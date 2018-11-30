using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformV : MonoBehaviour {

    private int cambiar;
    private Vector2 posOriginal;
    private IEnumerator ie;
    public int change;
    // Use this for initialization
    void Start()
    {
        //change = 6;
        posOriginal = transform.position;
        cambiar = 1;
        ie = checarD();
        StartCoroutine(ie);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0, cambiar * Time.deltaTime * 6, 0, Space.World);

    }

    IEnumerator checarD()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            float d = Vector2.Distance(transform.position, posOriginal);
            if (d > change)
            {
                cambiar *= -1;
            }
        }
    }
}

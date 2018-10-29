using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyT : MonoBehaviour {

    public Transform[] path;
    public float threshold;

    private Transform target;
    private int current;
    private Coroutine c;
    private IEnumerator ie;
    private IEnumerator ie2;
    private Coroutine c2;
    private bool frozen;

    private bool visible = false;

	// Use this for initialization
	void Start () {
        target = path[0];
        current = 0;
        ie = verificarDistancia();
        c = StartCoroutine(ie);
        ie2 = froze();
        frozen = false;
    }
	
	// Update is called once per frame
	void Update () {

        if(visible && frozen ==false){
            transform.position = Vector2.MoveTowards(transform.position,
               path[current].transform.position,
               5 * Time.deltaTime);
        }

    }

    IEnumerator verificarDistancia()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.35f);
            float d = Vector3.Distance(transform.position, path[current].position);
            if (d < threshold)
            {
                current++;
                current %= path.Length;
                target = path[current];
            }
        }
    }

    private void OnBecameVisible()
    {
        visible = true;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 15 && frozen == false)
        {

            StartCoroutine(ie2);
            frozen = true;
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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Freeze : MonoBehaviour {

    private Rigidbody2D re;
    IEnumerator ie;
   
    // Use this for initialization
    void Start()
    {
        re = GetComponent<Rigidbody2D>();
        re.AddForce(transform.up * 1, ForceMode2D.Impulse);
        ie = desaparecer();
        StartCoroutine(ie);
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator desaparecer()
    {
        yield return new WaitForSeconds(4);
        Destroy(gameObject);
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 11)
        {
            //Destroy(collision.gameObject);
            
        }
        if (collision.gameObject.layer == 9)
        {
            Destroy(gameObject);
        }
        Destroy(gameObject);
    }
}

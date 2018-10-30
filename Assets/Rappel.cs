using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rappel : MonoBehaviour {

    public static Vector2 puntoRappel;
    private Rigidbody2D re;
    public static bool rappelCont;
    // Use this for initialization
    void Start()
    {
        //transform.Rotate(0, 0, 270, Space.World);
        re = GetComponent<Rigidbody2D>();
        re.AddForce(transform.right * 1, ForceMode2D.Impulse);
        StartCoroutine(desaparecer());
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator desaparecer()
    {
        yield return new WaitForSeconds(2.0f);
        Destroy(gameObject);
        Movimiento.rappelExist = false;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 20)
        {
            puntoRappel = collision.transform.position;
            rappelCont = true;
        }
        Movimiento.rappelExist = false;
        Destroy(gameObject);

    }
}

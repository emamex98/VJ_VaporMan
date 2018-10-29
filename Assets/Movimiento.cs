using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour {

    private Rigidbody2D rb;
    private float countJump, h, v, h2;
    public GameObject rain,freeze,rappel;
    public Transform bow;
    public Text textito;
    private float j = 0;
    private IEnumerator coroutine, returnTo, ieRappel;
    public static bool rappelExist;

    public static int vidas;
    private int shoot;

	// Use this for initialization
	void Start () {
        rappelExist = false;
        rb = GetComponent<Rigidbody2D>();
        countJump = 0;
        coroutine = disparo();
        returnTo = returnToNormal();
        ieRappel = rappelMove();
        StartCoroutine(ieRappel);
        vidas = 3;
        shoot = 0;

	}
	
	// Update is called once per frame
	void Update () {

        textito.text = ("Vidas :" + vidas);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        h2 = Input.GetAxis("Horizontal2");
        transform.Translate(h * Time.deltaTime * 6, 0, 0, Space.World);

        if (Input.GetKeyDown(KeyCode.Space) && countJump==0)
        {
            rb.AddForce(transform.up * 30, ForceMode2D.Impulse);
            countJump += 1;
        }
        float jActual = Input.GetAxis("Fire1");
        if(j==0 && jActual==1)
        {
            StartCoroutine(coroutine);
        }
        else if (j==1 && jActual == 0)
        {
            StopCoroutine(coroutine);
        }
        j = jActual;
        if(!rappelExist && Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate<GameObject>(rappel, bow.position, bow.rotation);
            rappelExist = true;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 center = collider.bounds.center;

        if (collision.gameObject.layer == 8)
        {
            countJump = 0;
        }
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 12)
        {
            print("colision uwu");
            vidas -= 1;
            if (vidas == 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if (collision.gameObject.layer == 14)
        {
            shoot = 1;
            StartCoroutine(returnToNormal());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 16)
        {
            shoot = 2;
            StartCoroutine(returnToNormal2());
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 17)
        {
            vidas++;
            Destroy(collision.gameObject);
        }
        if(collision.gameObject.layer == 18 && contactPoint.x < center.x)
        {
            rb.gravityScale *= -1;
            transform.Rotate(180, 0, 0, Space.World);
        }
    }

    IEnumerator disparo()
    {
        while (true)
        {
            yield return new WaitForSeconds(.5f);
            if (shoot == 0)
            {
                if (h2 > 0 && v == 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                }
                else if (h2 > 0 && v > 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 315));
                }
                else if (h2 == 0 && v > 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 0));
                }
                else if (h2 < 0 && v > 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 45));
                }
                else if (h2 < 0 && v == 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 90));
                }
                else if (h2 < 0 && v < 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 135));
                }
                else if (h2 == 0 && v < 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 180));
                }
                else if (h2 > 0 && v < 0)
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 225));
                }
                else
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                }
            }
            else if(shoot == 1)
            {

                if (h2 > 0 && v == 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 270));
                }
                else if (h2 > 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 315));
                }
                else if (h2 == 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 0));
                }
                else if (h2 < 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 45));
                }
                else if (h2 < 0 && v == 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 90));
                }
                else if (h2 < 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 135));
                }
                else if (h2 == 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 180));
                }
                else if (h2 > 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 225));
                }
                else
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 270));
                }
            }
            if(shoot == 2)
            {
                
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 315));
                
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 0));
               
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 45));
                
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 90));
                
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 135));
               
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 180));
               
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 225));
             
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                
            }
        }
    }
    IEnumerator returnToNormal()
    {
        yield return new WaitForSeconds(10.0f);
        shoot = 0;

    }
    IEnumerator returnToNormal2()
    {
        yield return new WaitForSeconds(4.0f);
        shoot = 0;

    }

    IEnumerator rappelMove()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if (Rappel.rappelCont == true)
            {
                transform.position = Vector2.Lerp(transform.position, Rappel.puntoRappel, 1.0f);
                Rappel.rappelCont = false;
            }
        }
    }
}

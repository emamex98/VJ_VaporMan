﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Movimiento : MonoBehaviour {

    private Rigidbody2D rb;
    private float countJump, h, v, h2;
    public GameObject rain;
    public Transform bow;
    public Text textito;
    private float j = 0;
    private IEnumerator coroutine;

    private Animator animator;
    private AudioSource audioData;

    public static int vidas;

	// Use this for initialization
	void Start () {
        rb = GetComponent<Rigidbody2D>();

        animator = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();

        countJump = 0;
        coroutine = disparo();
        vidas = 3;
	}
	
	// Update is called once per frame
	void Update () {

        textito.text = ("Vidas :" + vidas);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        h2 = Input.GetAxis("Horizontal2");

        transform.Translate(h * Time.deltaTime * 6, 0, 0, Space.World);

        if(h > 0){
            animator.SetTrigger("CaminarDerecha");
        } else if (h < 0){
            animator.SetTrigger("CaminarIzquierda");
        } else {
            animator.SetTrigger("RegresaIdle");
        }

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
            animator.SetTrigger("StopDisparos");
        }
        j = jActual;
        
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            countJump = 0;
        }
        if (collision.gameObject.layer == 11 || collision.gameObject.layer == 12)
        {
            print("colision uwu");
            vidas -= 1;
            audioData.Play(0);

            if (vidas == 0)
            {
                Application.LoadLevel(Application.loadedLevel);
            }
        }
    }

    IEnumerator disparo()
    {
        while (true)
        {
            if (h2 > 0 && v == 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                animator.SetTrigger("DisparoDerecha");
            }
            else if (h2 > 0 && v > 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 315));
                animator.SetTrigger("DisparoNE");
            }
            else if (h2 == 0 && v > 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 0));
                animator.SetTrigger("DisparoArriba");
            }
            else if (h2 < 0 && v > 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 45));
                animator.SetTrigger("DisparoNW");
            }
            else if (h2 < 0 && v == 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 90));
                animator.SetTrigger("DisparoIzquierda");
            }
            else if (h2 < 0 && v < 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 135));
                animator.SetTrigger("DisparoSW");
            }
            else if (h2 == 0 && v < 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 180));
                animator.SetTrigger("DisparoAbajo");
            }
            else if (h2 > 0 && v < 0)
            {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 225));
                animator.SetTrigger("DisparoSE");
            }
            else {
                Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                animator.SetTrigger("DisparoDerecha");
            }
            yield return new WaitForSeconds(.5f);
        }

    }

}
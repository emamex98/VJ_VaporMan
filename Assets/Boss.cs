using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour {

    private GameObject player;
    public GameObject fire;
    public Transform bossIns;
    private Estado idle, attack, agressive, dead;
    private Simbolo seePlayer, lowLife, noLife;
    private Estado actual, anterior;
    private Component scriptActual;
    private IEnumerator ie,ie2,ie3, jump;
    private float threshold, velocidad, dir;
    private int vidaBoss, cont, noSalto;
    private bool noLoss, ground;
    private Rigidbody2D rb;
    
	// Use this for initialization
	void Start () {
        noLoss = true;
        ground = false;
        vidaBoss = 10;
        noSalto = 0;
        player = GameObject.Find("womba");
        idle = new Estado("idle", typeof(Idle));
        attack = new Estado("attack", typeof(Attack));
        agressive = new Estado("agressive", typeof(Agressive));
        dead = new Estado("dead", typeof(Dead));

        seePlayer = new Simbolo("seePlayer");
        lowLife = new Simbolo("lowLife");
        noLife = new Simbolo("noLife");

        idle.definirTransicion(seePlayer, attack);

        attack.definirTransicion(lowLife, agressive);

        agressive.definirTransicion(noLife, dead);

        actual = idle;
        anterior = actual;
        scriptActual = gameObject.AddComponent(actual.Tipo);
        threshold = 11.0f;

        ie = checarDistancia();
        ie2 = checarVida();
        ie3 = disparar();
        jump = salta();
        StartCoroutine(ie);
        StartCoroutine(ie2);
        StartCoroutine(ie3);
        cont = 0;
        velocidad = 5;
        dir = 1;
        rb = GetComponent<Rigidbody2D>();
        //System.Type tipo = typeof(Idle);
    }

    void Transitar(Simbolo simbolo)
    {
        actual = actual.aplicarSimbolo(simbolo);
        if(actual == anterior)
        {
            return;
        }
        anterior = actual;
        Destroy(scriptActual);
        scriptActual = gameObject.AddComponent(actual.Tipo);
    }
	
	// Update is called once per frame
	void Update () {

        transform.Translate(dir * velocidad * Time.deltaTime, 0, 0, Space.World);

	}

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10)
        {
            if (!noLoss)
            {
                vidaBoss--;
            }
        }
        if (collision.gameObject.layer == 18 || collision.gameObject.layer == 23)
        {
            dir *= -1;
        }
        if (collision.gameObject.layer == 8)
        {
            ground = true;
        }
    }

    IEnumerator checarDistancia()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            float d = Vector2.Distance(transform.position, player.transform.position);
            if (d < threshold && cont == 0)
            {
                noLoss = false;
                Transitar(seePlayer);
                cont++;
            }
        }
    }

    IEnumerator checarVida()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(vidaBoss < 5)
            {
                Transitar(lowLife);
                velocidad = 10;
                //StartCoroutine(jump);
                yield return new WaitForSeconds(2.0f);
                if (ground)
                {
                    rb.AddForce(transform.up * 25.0f, ForceMode2D.Impulse);
                    print("salta");
                    ground = false;
                }
            }
            if(vidaBoss < 1)
            {
                Transitar(noLife);
                //Destroy(pared);
            }
        }
    }

    IEnumerator disparar()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            if(actual == attack || actual == agressive)
            {
                Instantiate<GameObject>(fire, bossIns.transform.position, bossIns.transform.rotation);
            }
        }
    }

    IEnumerator salta()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            if (ground)
            {
                rb.AddForce(transform.up * 30.0f, ForceMode2D.Impulse);
                print("salta");
                ground = false;
            }
        }
    }
}

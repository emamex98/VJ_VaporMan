using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinalBoss : MonoBehaviour {

    private float dir, vidas;
    public GameObject disparo,shield;
    public Transform[] trans;
    public Transform posIn;
    private IEnumerator ie, ie2, ie3,escudo;
    private bool escudado;
    private int estado;
    private Animator ani;

	// Use this for initialization
	void Start () {
        ani = GetComponent<Animator>();
        dir = 1;
        ie = disparoBoss();
        ie2 = checarEstado();
        ie3 = teletransportar();
        escudo = escudito();
        StartCoroutine(ie);
        StartCoroutine(ie2);
        shield.SetActive(false);
        vidas = 30;
        escudado = false;
        estado = 1;
	}
	
	// Update is called once per frame
	void Update () {
        if (estado != 4)
        {
            transform.Translate(6 * Time.deltaTime * dir, 0, 0, Space.World);

            if (Mathf.Abs(transform.position.x) > 8)
            {
                dir *= -1;
            }
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("Ganaste", LoadSceneMode.Single);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == 10 && !escudado)
        {
            vidas--;
            print(vidas);
        }
    }

    IEnumerator disparoBoss()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.5f);
            Instantiate<GameObject>(disparo, posIn.position, Quaternion.Euler(0, 0, 180));
            Instantiate<GameObject>(disparo, posIn.position, Quaternion.Euler(0, 0, 135));
            Instantiate<GameObject>(disparo, posIn.position, Quaternion.Euler(0, 0, 225));
        }
    }
    IEnumerator escudito()
    {
        while (true)
        {
            if (escudado)
            {
                shield.SetActive(false);
                escudado = false;
            }
            else
            {
                shield.SetActive(true);
                escudado = true;
            }
            yield return new WaitForSeconds(4.0f);

        }
    }
    IEnumerator teletransportar()
    {
        while (true)
        {
            yield return new WaitForSeconds(2.0f);
            int lugar = Random.Range(0, 5);
            transform.position = trans[lugar].position;

        }
    }
    IEnumerator checarEstado()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.5f);
            if (vidas < 20 && estado==1)
            {
                estado = 2;
                StartCoroutine(escudo);
            }
            if (vidas < 10 && estado == 2)
            {
                estado = 3;
                ani.SetBool("FinalAngry", true);
                StartCoroutine(ie3);
            }
            if(vidas < 1 && estado ==3)
            {
                estado = 4;
                Movimiento.ganaste = true;
                StopCoroutine(escudo);
                StopCoroutine(ie);
                StopCoroutine(ie3);
                shield.SetActive(false);
                ani.SetBool("Dead", true);
                yield return new WaitForSeconds(3.0f);
                Destroy(gameObject);
                
            }
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Movimiento : MonoBehaviour
{

    //[SerializeField] private GameObject pausePanel;
    private Rigidbody2D rb;
    private float countJump, h, v, h2;
    public GameObject rain, freeze, rappel, goBeyond;
    public Transform bow;
    public Text textito;
    private float j = 0;
    private IEnumerator coroutine, returnTo, ieRappel, invin, win, damage;
    public static bool rappelExist, ganaste;

    public static int vidas = 5;
    private int shoot;

    private Animator animator;
    private AudioSource audioData;
    public float threshold;
    private bool rappeling, invincible, fall;
    //private int cont;

    // Use this for initialization
    void Start()
    {
        rappelExist = false;
        ganaste = false;
        fall = false;
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        audioData = GetComponent<AudioSource>();
        countJump = 0;
        coroutine = disparo();
        returnTo = returnToNormal();
        ieRappel = rappelMove();
        invin = invincibility();
        win = ganar();
        damage = fallDamage();
        StartCoroutine(ieRappel);
        StartCoroutine(win);
        StartCoroutine(damage);
        //vidas = 3;
        shoot = 0;
        //threshold = 0.4f;
        rappeling = false;
        invincible = false;
        goBeyond.SetActive(false);
        animator.SetFloat("GravityInv", rb.gravityScale);
        //cont = 0;
        //pausePanel.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        textito.text = ("Vidas: " + vidas);

        h = Input.GetAxis("Horizontal");
        v = Input.GetAxis("Vertical");
        h2 = Input.GetAxis("Horizontal2");

        animator.SetFloat("Run",h*5);
        if(!rappeling && !invincible){
            transform.Translate(h * Time.deltaTime * 6, 0, 0, Space.World);
        }
        else if(!rappeling && invincible)
        {
            transform.Translate(h * Time.deltaTime * 13, 0, 0, Space.World);
        }

        if (Input.GetKeyDown(KeyCode.Space) && countJump == 0 && !rappeling)
        {
            rb.AddForce(transform.up * 30, ForceMode2D.Impulse);
            countJump += 1;
            print("SpaceBar");
            animator.SetBool("IsJumpingD", true);
        }

        float jActual = Input.GetAxis("Fire1");

        if (j == 0 && jActual == 1)
        {
            StartCoroutine(coroutine);
        }
        else if (j == 1 && jActual == 0)
        {
            StopCoroutine(coroutine);

            animator.SetTrigger("StopDisparos");
            animator.ResetTrigger("DisparoDerecha");
            animator.ResetTrigger("DisparoIzquierda");
            animator.ResetTrigger("DisparoArriba");
            animator.ResetTrigger("DisparoAbajo");
            animator.ResetTrigger("DisparoSE");
            animator.ResetTrigger("DisparoNE");
            animator.ResetTrigger("DisparoSW");
            animator.ResetTrigger("DisparoNW");

        }

        j = jActual;

        if (!rappelExist && Input.GetKeyDown(KeyCode.Z))
        {
            Instantiate<GameObject>(rappel, bow.position, bow.rotation);
            rappelExist = true;
        }

        if (Rappel.rappelCont == true)
        {
            transform.position = Vector2.Lerp(transform.position, Rappel.puntoRappel, 13 * Time.deltaTime / Vector2.Distance(transform.position, Rappel.puntoRappel));
            //cont = 1;
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            Application.Quit();
        }
        if (Input.GetKeyUp(KeyCode.N))
        {
            SceneManager.LoadScene("FinalBoss", LoadSceneMode.Single);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Collider2D collider = collision.collider;
        Vector2 contactPoint = collision.contacts[0].point;
        Vector2 center = collider.bounds.center;

        if (collision.gameObject.layer == 8 || collision.gameObject.layer == 20)
        {
            animator.SetBool("IsJumpingD", false);
            print("choca");
            countJump = 0;
            if (fall)
            {
                vidas--;
                fall = false;
                if (vidas < 1)
 
                {
                    vidas = 5;
                    Application.LoadLevel(Application.loadedLevel);
                }
            }
            if(collision.transform.tag == "MovingPlat")
            {
                transform.parent = collision.transform;
            }
        }
        if ((collision.gameObject.layer == 11 || collision.gameObject.layer == 12 || collision.gameObject.layer == 19) && !invincible)
        {
            print("colision uwu");
            vidas -= 1;
            audioData.Play(0);
            if (vidas < 1)
            {
                vidas = 5;
                Application.LoadLevel(Application.loadedLevel);
            }
        }
        if ((collision.gameObject.layer == 11 || collision.gameObject.layer == 12) && invincible)
        {
            Destroy(collision.gameObject);
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
        if (collision.gameObject.layer == 18 && contactPoint.x < center.x)
        {
            rb.gravityScale *= -1;
            Bounds.uyx *= -1;
            transform.Rotate(180, 0, 0, Space.World);
            animator.SetFloat("GravityInv", rb.gravityScale);
        }
        if (collision.gameObject.layer == 21)
        {
            transform.position = (collision.gameObject.transform.GetChild(0).position);
        }
        if (collision.gameObject.layer == 22)
        {
            invincible = true;
            goBeyond.SetActive(true);
            StartCoroutine(invin);
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.layer == 24)
        {
            SceneManager.LoadScene("FinalBoss", LoadSceneMode.Single);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.transform.tag == "MovingPlat")
        {
            transform.parent = null;
        }
    }

    IEnumerator disparo()
    {
        while (true)
        {
            
            if (shoot == 0)
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
                else
                {
                    Instantiate<GameObject>(rain, bow.position, Quaternion.Euler(0, 0, 270));
                    animator.SetTrigger("DisparoDerecha");
                    //yield return new WaitForSeconds(1.0f);
                    //animator.ResetTrigger("DisparoDerecha");
                }
            }

            else if (shoot == 1)
            {
                if (h2 > 0 && v == 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 270));
                    animator.SetTrigger("DisparoDerecha");
                }
                else if (h2 > 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 315));
                    animator.SetTrigger("DisparoNE");
                }
                else if (h2 == 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 0));
                    animator.SetTrigger("DisparoArriba");
                }
                else if (h2 < 0 && v > 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 45));
                    animator.SetTrigger("DisparoNW");
                }
                else if (h2 < 0 && v == 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 90));
                    animator.SetTrigger("DisparoIzquierda");
                }
                else if (h2 < 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 135));
                    animator.SetTrigger("DisparoSW");
                }
                else if (h2 == 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 180));
                    animator.SetTrigger("DisparoAbajo");
                }
                else if (h2 > 0 && v < 0)
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 225));
                    animator.SetTrigger("DisparoSE");
                }
                else
                {
                    Instantiate<GameObject>(freeze, bow.position, Quaternion.Euler(0, 0, 270));
                    animator.SetTrigger("DisparoDerecha");
                }
            }
            if (shoot == 2)
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

                animator.SetTrigger("DisparoDerecha");

            }

            yield return new WaitForSeconds(.5f);
        }

    }

    IEnumerator ganar()
    {
        while (true)
        {
            yield return new WaitForSeconds(4.0f);
            if (ganaste)
            {
                SceneManager.LoadScene("Ganaste", LoadSceneMode.Single);
            }
        }
    }

    IEnumerator fallDamage()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.1f);
            if(Mathf.Abs(rb.velocity.y) > 13 && !fall)
            {
                fall = true;
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
            float d = Vector2.Distance(transform.position, Rappel.puntoRappel);
            if (d < threshold)
            {
                Rappel.rappelCont = false;
                //cont = 0;
            }
        }
    }
    IEnumerator invincibility()
    {
            yield return new WaitForSeconds(10.0f);
            invincible = false;
            goBeyond.SetActive(false);
        
    }

}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour {

    private float posX, posY;

    private Vector2 velocity;
    public float SmoothTimeY, SmoothTimeX;

    public GameObject player;

    public bool bounds;
    public Vector3 minCamPos,maxCamPos;

    // Use this for initialization
    void Start () {
        player = GameObject.Find("womba");
    }

    void Update()
    {
        posX = Mathf.SmoothDamp(transform.position.x, player.transform.position.x, ref velocity.x, SmoothTimeX);
        posY = Mathf.SmoothDamp(transform.position.y, player.transform.position.y, ref velocity.y, SmoothTimeY);

        transform.position = new Vector3(posX, posY,transform.position.z);

        if (bounds)
        {
            transform.position = new Vector3(Mathf.Clamp(transform.position.x, minCamPos.x, maxCamPos.x),
                Mathf.Clamp(transform.position.y,minCamPos.y,maxCamPos.y),
                Mathf.Clamp(transform.position.z,minCamPos.z,maxCamPos.z));
        }
    }
}

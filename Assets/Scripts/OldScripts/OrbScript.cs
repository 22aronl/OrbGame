using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbScript : MonoBehaviour
{
    public Vector2 dir;
    Movement pScript;
    public GameObject player;
    CircleCollider2D cxc;

    // Start is called before the first frame update
    void Start()
    {
        pScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        cxc = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        dir = transform.position;
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        //Debug.Log("WORKING");   
        if(collision.gameObject.tag == "Enemy") 
        {
            //Debug.Log("UP:DATE");
            pScript.Respawn();
            destroy();
        }
        if(collision.gameObject.tag != "Player")
        {
            //Debug.Log("TELEPORTING");
            pScript.TeleportToLocation(collision.GetContact(0).normal, transform.position, cxc.radius * 2);
            destroy();
        }
        
    }
}

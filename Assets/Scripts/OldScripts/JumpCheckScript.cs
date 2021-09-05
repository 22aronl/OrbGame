using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpCheckScript : MonoBehaviour
{
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        player = gameObject.transform.parent.gameObject;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("ON GROUND");
        if(collision.collider.tag == "ground" || collision.collider.tag == "block")
        {
            Debug.Log("ON GROUND 2");
            player.GetComponent<Movement>().isGrounded = true;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.collider.tag == "ground" || collision.collider.tag == "block")
        {
            player.GetComponent<Movement>().isGrounded = false;
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VineScript : MonoBehaviour
{
    Movement mScript;
    public GameObject player;
    //Rigidbody2D playerRB;
    //public float gravOrig;
    // Start is called before the first frame update
    void Start()
    {
        //playerRB = player.GetComponent<Rigidbody2D>();
        //gravOrig = playerRB.gravityScale;
    }

    // Update is called once per frame
    void Update()
    {
        // if(mScript != null)
        // {
        //     Debug.Assert((playerRB.gravityScale == 0 && mScript.onVine) || (!mScript.onVine && playerRB.gravityScale == gravOrig), "Desync");
        // }
        
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {

            mScript = collision.gameObject.GetComponent<Movement>();
            mScript.onVine = true;
            //Physics2D.gravity = new Vector2(0, 0);
            //playerRB.gravityScale = 0;
            //Debug.Log(gravOrig);
        }

    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            mScript = collision.gameObject.GetComponent<Movement>();
            mScript.onVine = false;
            //Physics2D.gravity = new Vector2(0, -9.8f);
            //playerRB.gravityScale = gravOrig;
            //Debug.Log("OFF VINE" + playerRB.gravityScale);
        }
    }
}

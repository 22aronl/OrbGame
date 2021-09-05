using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallBounce : MonoBehaviour
{
    // Start is called before the first frame update
    MagicOrbScript oScript;
    Vector2 vel;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "orb")
        {
            oScript = collision.gameObject.GetComponent<MagicOrbScript>();
            vel = oScript.GetVelocity();
            oScript.SetVelocity(new Vector2(-vel.x, vel.y));
        }
    }
}

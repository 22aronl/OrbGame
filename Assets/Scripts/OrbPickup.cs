using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbPickup : MonoBehaviour
{
    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            col.gameObject.GetComponent<Movement>().pickUpOrb();
            SoundManager.Instance.playOrbDiscover();
            Destroy(gameObject);

        }
    }
}

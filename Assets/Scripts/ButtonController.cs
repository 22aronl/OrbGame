using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonController : ResetObjectController
{
    BoxCollider2D bxc;
    public GameObject door;
    private int thingsOnBox = 0;
    public AudioClip buttonPress;
    public float vol = 0.3f;
    public AudioSource SoundEffects;
    // Start is called before the first frame update
    void Start()
    {
        bxc = GetComponent<BoxCollider2D>();
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            thingsOnBox++;
            door.GetComponent<DoorController>().activate();
            SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
            SoundEffects.clip = buttonPress;
            SoundEffects.Play();
        }
    }

    void OnCollisionExit2D(Collision2D col)
    {
        if(col.gameObject.layer != LayerMask.NameToLayer("Ground"))
        {
            thingsOnBox--;
        }

        if(thingsOnBox <= 0)
        {
            door.GetComponent<DoorController>().deactivate();
            SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
            SoundEffects.clip = buttonPress;
            SoundEffects.Play();
        }
    }

    public override void reset()
    {
        door.GetComponent<DoorController>().reset();
    }
}

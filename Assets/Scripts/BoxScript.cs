using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxScript : ResetObjectController
{
    public GameObject player;
    public float offset = 3.0f;
    private bool carry = false;
    private Vector2 start;
    private Quaternion rot;
    public AudioClip boxDrop;
    public AudioClip boxPickUp;
    public float vol = 0.8f;
    AudioSource SoundEffects;
    // Start is called before the first frame update
    void Start()
    {
        start = transform.position;
        rot = transform.rotation;
        SoundEffects = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log("HI");
        if(carry)
        {
            transform.position = player.transform.position + new Vector3(0, offset, 0);
            GetComponent<Rigidbody2D>().angularVelocity = 0;
            transform.rotation = rot;
            //Debug.Log(transform.position);
        }
    }

    public override void reset()
    {
        GetDropped();
        transform.position = start;
        transform.rotation = rot;
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
        GetComponent<Rigidbody2D>().angularVelocity = 0;
    }

    void FixedUpdate()
    {
        
    }

    public void GetPickedUp()
    {
        Debug.Log("SoundEffects" + SoundEffects + " " + SoundManager.Instance);
        SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
        SoundEffects.clip = boxPickUp;
        SoundEffects.Play();
        this.GetComponent<BoxCollider2D>().enabled = false;
        carry = true;
    }

    public void GetDropped()
    {
        SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
        SoundEffects.clip = boxDrop;
        SoundEffects.Play();
        this.GetComponent<BoxCollider2D>().enabled = true;
        carry = false;
    }
}

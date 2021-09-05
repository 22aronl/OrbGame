using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    // Start is called before the first frame update
    Animator anim;
    BoxCollider2D bxc;
    private float TimePlayed;
    public float endSize;
    public float endOffset;
    private float startSize;
    private float startOffset;
    private bool opening = false;

    public AudioSource SoundEffects;
    public AudioClip door;
    public float vol = 0.5f;
    void Start()
    {
        anim = GetComponent<Animator>();
        TimePlayed = Time.time;
        bxc = GetComponent<BoxCollider2D>();
        startSize = bxc.size.y;
        startOffset = bxc.offset.y;
    }

    void Update()
    {
        float t = 1.0f;
        if(anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            t = (Time.time - TimePlayed)/anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        }

        if(t >= 1)
            t = 1.0f;

        float dif = startSize - endSize;
        dif *= t;
        if(opening)
        {
            bxc.size = new Vector2(bxc.size.x, startSize - dif);
            bxc.offset = new Vector2(bxc.offset.x, startOffset + (dif /2));

        }
        else
        {
            bxc.size = new Vector2(bxc.size.x, endSize + dif);
            bxc.offset = new Vector2(bxc.offset.x, endOffset - (dif /2));
        }
        
    }

    public void activate()
    {
        float t = 0.0f;
        //Debug.Log(Time.time - TimePlayed);
        SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
        SoundEffects.clip = door;
        SoundEffects.Play();
        // anim.enabled = false;
        // anim.enabled = true;
        if(anim.GetCurrentAnimatorClipInfo(0).Length > 0)
        {
            t = (Time.time - TimePlayed)/anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
            if(t >= 1)
                t = 1.0f;
            anim.Play("OpenDoor2", 0, 1.0f - t);
        }
        else
            anim.Play("OpenDoor", 0,1.0f);
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0).normalizedTime);
        TimePlayed = Time.time;
        opening = true;
        
    
    }

    public void deactivate()
    {
        SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
        SoundEffects.clip = door;
        SoundEffects.Play();
        float t = 0.0f;
        //Debug.Log(Time.time - TimePlayed);
        // anim.enabled = false;
        // anim.enabled = true;
        if(anim.GetCurrentAnimatorClipInfo(0).Length > 0)
            t = (Time.time - TimePlayed)/anim.GetCurrentAnimatorClipInfo(0)[0].clip.length;
        else
            t = 1;
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0)[0].clip.length);
        //Debug.Log(anim.GetCurrentAnimatorClipInfo(0).normalizedTime);
        TimePlayed = Time.time;
        if(t >= 1)
            t = 1.0f;
        anim.Play("CloseDoor", 0, 1.0f - t);
        opening = false;
        
    }

    public void reset()
    {
        anim.enabled = false;
        anim.enabled = true;
        anim.Play("Idle", 0, 0.0f);
        opening = false;
        TimePlayed = 1f;
    }
} 

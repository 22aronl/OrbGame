using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class MagicOrbScript : MonoBehaviour
{
    public GameObject spriteRe;
    public GameObject lightChild;
    Animator anim;
    public Vector2 dir;
    //Movement pScript;
    //public GameObject player;
    CircleCollider2D cxc;
    Rigidbody2D rb;
    Light2D li;

    public AudioClip orbLand;
    public AudioSource SoundEffects;
    public float vol = 0.4f;
    bool respawn = false;

    public float initialGravity = 0f;
    public float endingGravity = 2f;
    public float startGrav = 2f;
    private float enableFire;
    private bool contact;
    public float hoverDist = 2f;
    public float negativeGravity = 2f;
    public float errorRange = 0.1f;

    Vector2 normal;

    // Start is called before the first frame update
    void Start()
    {  
        SoundEffects = GetComponent<AudioSource>();
        li = lightChild.GetComponent<Light2D>();
        anim = spriteRe.GetComponent<Animator>();
        //pScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Movement>();
        cxc = GetComponent<CircleCollider2D>();
        enableFire = Time.time;
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = initialGravity;
        contact = false;
    }

    // Update is called once per frame
    void Update()
    {

        anim.SetBool("isHovering", false);
        if(!respawn)
        {
            Vector2 direction = Vector2.down;
            RaycastHit2D hit = Physics2D.Raycast(transform.position,direction, hoverDist + 1f);
            if(contact && hit.collider!= null)
            {
                //Debug.Log("contact " + rb.gravityScale + hit.distance);
                if(hit.distance <= hoverDist - errorRange)
                {
                    rb.gravityScale = (hit.distance - hoverDist) / hoverDist * negativeGravity;
                }
                else if(hit.distance >= hoverDist)
                {
                    anim.SetBool("isHovering", true);
                    rb.velocity = new Vector2(rb.velocity.x, 0);
                    rb.gravityScale = 0;
                }
            }



            dir = transform.position;
            if(!contact && Time.time - enableFire > startGrav)
                rb.gravityScale = endingGravity;
        }
    }

    public float playRespawn()
    {
        anim.Play("FloatingOrb", 0, 0.0f);
        respawn = true;
        rb.gravityScale = 0;
        rb.velocity = new Vector2(0, 0);
        cxc.enabled = false;
        StartCoroutine(reduceLightToZero(1.45f));
        return 1.5f;

    }

    public IEnumerator reduceLightToZero(float time)
    {
        //float dif = li.intensity / time;
        float elapsed = 0;
        float org = li.pointLightOuterRadius;
        while(elapsed < time)
        {
            elapsed += Time.deltaTime * Time.timeScale;
            li.pointLightOuterRadius = (org - ((org - li.pointLightInnerRadius) * elapsed / time));
            
            yield return 0;
        }
    }

    public GameObject getLight()
    {
        return lightChild;
    }

    public void destroy()
    {
        Destroy(this.gameObject);
    }

    public bool GetContact()
    {
        return contact;
    }

    public Vector2 GetNormal()
    {
        return normal;
    }

    public float GetSize()
    {
        return cxc.radius * 2;
    }

    public Vector2 GetVelocity()
    {
        return rb.velocity;
    }

    public void SetVelocity(Vector2 vel)
    {
        rb.velocity = vel;
    }

    public void SetPosition(Vector2 pos)
    {
        transform.position = pos;
        respawn = true;
        collided();
        cxc.enabled = false;
    }

    public void Play(string s)
    {
        anim.Play(s, 0, 0.0f);
    }

    public void collided()
    {
        contact = true;
        rb.gravityScale = 0f;
        rb.velocity = new Vector2(0, 0);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag != "Player")
        {
            contact = true;
            rb.gravityScale = 0f;
            rb.velocity = new Vector2(0, 0);
            normal = collision.GetContact(0).normal;

            SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
            SoundEffects.clip = orbLand;
            SoundEffects.Play();

        }
        
    }
}

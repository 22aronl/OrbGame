using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingBlock : ResetObjectController
{
    Rigidbody2D rb;
    public float safeTimer = 0.7f;
    private float shakePower, shakeTimer;
    private float counter;
    private Vector3 pos;
    private Quaternion q;
    private bool set = true;

    public AudioClip dropBox;
    public float vol = 0.4f;
    public AudioSource SoundEffects;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        shakeTimer = 0;
        pos = transform.position;
        q = transform.localRotation;
    }

    public override void reset()
    {
        set = false;
        StopCoroutine("setStartShake");
        StopCoroutine("StopMovement");
        shakeTimer = 0;
        set = true;
        rb.velocity = new Vector2(0, 0);
        rb.angularVelocity = 0f;
        transform.position = pos;
        transform.localRotation = q;
        //gameObject.SetActive(false);
        rb.isKinematic = true;
        gameObject.SetActive(true);
    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            set = false;
            StartCoroutine("setStartShake");
            StartCoroutine("StopMovement");
        }
    }

    IEnumerator StopMovement()
    {
        yield return new WaitForSeconds(4f);
        if(!set)
        {
            gameObject.SetActive(false);
            rb.isKinematic = true;
        }
    }

    void DropBlock()
    {
        rb.isKinematic = false;
    }

    IEnumerator setStartShake()
    {
        yield return new WaitForSeconds(safeTimer);
        StartShake(0.5f, 0.01f);
        SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
        SoundEffects.clip = dropBox;
        SoundEffects.Play();
    }

    public void StartShake(float length, float power)
    {
        shakeTimer = length;
        shakePower = power;
    }

    private void LateUpdate()
    {
        if(shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            counter++;
            if(counter%100 < 50)
            {
                transform.Translate(Vector3.right * shakePower);
            } else
            {
                transform.Translate(Vector3.left * shakePower);
            }
            if(shakeTimer <= 0)
            {
                DropBlock();
                shakeTimer = -1f;
            }
        }
    }
}

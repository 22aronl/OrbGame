using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    Rigidbody2D enemy;
    // Start is called before the first frame update
    public bool left = false;
    public float distance = 1.3f;
    public float speed = 2.0f;
    public float yOffset = -0.5f;
    public Camera cam;
    Vector3 pointOnScreen;
    public AudioClip squish;
    private AudioSource sound;

    public AudioClip buttonPress;
    public float vol = 0.4f;
    public AudioSource SoundEffects;

    private bool isPlaying = false;
    void Start()
    {
        enemy = GetComponent<Rigidbody2D>();
        sound = GetComponent<AudioSource>();
        sound.loop = true;
        sound.clip = squish;
    }

    // Update is called once per frame
    void Update()
    {
        pointOnScreen = cam.WorldToScreenPoint(this.GetComponent<Renderer>().bounds.center); 
        if (!isPlaying && ((pointOnScreen.x > 0) || (pointOnScreen.x < Screen.width) || (pointOnScreen.y > 0) || (pointOnScreen.y < Screen.height)))
        {
            isPlaying = true;
            //sound.Play();
        } else
        {
            sound.Stop();
            //isPlaying = false;
        }
    }

    void FixedUpdate()
    {
        Vector2 dir;
        if(left)
            dir = transform.TransformDirection(Vector2.left);
        else
            dir = transform.TransformDirection(Vector2.right);
        //Debug.Log("DIR" + dir);
        RaycastHit2D hit = Physics2D.Raycast(this.gameObject.transform.position+new Vector3(0, yOffset, 0), dir);
        
        //Debug.DrawRay(a, dir * distance, Color.red, 0, false);
        if (hit.collider != null)
        {
            //Debug.Log(hit + " " + hit.distance + "  " + hit.collider + " " + distance);
            //Debug.DrawRay(transform.position + new Vector3(0, -yOffset, 0), dir * 10, Color.red, 0, false);
            //If the object hit is less than or equal to 6 units away from this object.
            if (hit.collider.tag != "Player" && hit.distance <= distance)
            {
                left = !left;
            }
        }
        // if(Physics.Raycast(transform.position, dir, out hit))
        // {
        //     Debug.Log("HIT " + hit);
        //     Debug.DrawRay(a, dir * hit.distance, Color.red, 0, false);
        //     Debug.Log("DISTANCE" + hit.distance);
        //     Debug.DrawRay(a, dir * 100, Color.white, 0, false);
        //     if(hit.distance <= distance)
        //         left = !left;
        // }
        transform.position = transform.position + new Vector3(speed * (left ? -1 : 1), 0, 0) * Time.deltaTime;
        //Debug.Log(enemy.velocity);
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.tag == "Player")
        {
            col.gameObject.GetComponent<Movement>().Respawn();
            SoundEffects.volume = SoundManager.Instance.getSFXVolume() * vol;
            SoundEffects.clip = buttonPress;
            SoundEffects.Play();
        }
    }
}
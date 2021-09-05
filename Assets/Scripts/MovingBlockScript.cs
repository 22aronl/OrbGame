using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingBlockScript : MonoBehaviour
{
    public Vector3 p1;
    public Vector3 p2;
    public float lerpSpeed = 3.0f;
    private float t = 0.0f;
    private Vector3 startingPos;
    private Vector3 endingPos;


    void Start()
    {
        p1 = transform.position;
        p2 = this.gameObject.transform.GetChild(0).gameObject.transform.position;
        startingPos = p1;
        endingPos = p2;
    }

    // Update is called once per frame
    void FixedUpdate()
    {

        
        t += Time.deltaTime * (Time.timeScale / lerpSpeed);
        transform.position = Vector3.Lerp(startingPos, endingPos, t);
        
        if(t > 1.0f)
        {
            t = 0f;
            Vector3 temp = startingPos;
            startingPos = endingPos;
            endingPos = temp;
        }

    }

    // void OnCollisionEnter2D(Collision2D col)
    // {
    //     Debug.Log(col.gameObject.tag);
    //     if(col.gameObject.tag == "Jump")
    //     {
    //         col.transform.SetParent(transform);
    //     }
    // }

    // void OnCollisionExit2D(Collision2D col)
    // {
    //     if(col.gameObject.tag == "Jump")
    //     {
    //         col.transform.SetParent(null);
    //     }
    // }
}
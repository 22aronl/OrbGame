using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void moveCam(float camPanDuration, Vector3 pos)
    {
        if(pos.x == transform.position.x && pos.y == transform.position.y && pos.z == transform.position.z)
        {

        }
        else
        {
            Movement.freezeMovement = true;
            StartCoroutine(LerpToPosition(camPanDuration, pos));    
        }
    }


    IEnumerator LerpToPosition(float lerpSpeed, Vector3 newPosition)
    {   
        float t = 0.0f;
        Vector3 startingPos = transform.position;
        while (t < 1.0f)
        {
            t += Time.deltaTime * (Time.timeScale / lerpSpeed);
 
            transform.position = Vector3.Lerp(startingPos, newPosition, t);
            if(t >= 1.0f)
                Movement.freezeMovement = false;
            yield return 0;
        }
        //if(t >= 1.0f)
            //Movement.freezeMovement = false;
    }
}

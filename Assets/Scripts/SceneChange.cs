using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChange : MonoBehaviour
{
    public Camera cam;
    public float camDuration;
    private Vector3 respawnLocation;
    public GameObject[] objectsInScene;
    // Start is called before the first frame update

    void Start()
    {
        respawnLocation = this.gameObject.transform.GetChild(0).position;
        camDuration = 1.5f;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        //Debug.Log(col.tag);
        //Debug.Log("ENETER SCENE");
        if(col.tag == "Player")
        {
            //Debug.Log("PALYER");
            Movement mn = col.gameObject.GetComponent<Movement>();
            cam.GetComponent<CameraScript>().moveCam(camDuration, gameObject.transform.position);
            mn.SetRespawnLocation(respawnLocation);
            mn.SetObjectsInScene(objectsInScene);
            mn.resetOrb();
            //BoxCollider2D bx = GetComponent<BoxCollider2D>();
            //Debug.Log("trans" + transform.position.x + " " + transform.localScale.x);
            mn.SetBackLimit(transform.position.x - transform.localScale.x/2, transform.position.y);
        }
    }

    void OnTriggerExit2D(Collider2D col)
    {
        if(col.tag == "orb")
        {
            col.gameObject.GetComponent<MagicOrbScript>().collided();
        }
    }
}

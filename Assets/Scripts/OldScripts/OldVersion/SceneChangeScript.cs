using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneChangeScript : MonoBehaviour
{
    private GameObject sceneStart;
    private GameObject sceneEnd;

    public Camera cam;
    private GameObject scEnter;
    private GameObject scExit;

    void Start()
    {
        //Make sure the camera scene is at the start indexx
        scEnter = this.gameObject.transform.GetChild(0).gameObject;
        scExit = this.gameObject.transform.GetChild(1).gameObject;

        sceneStart = this.gameObject.transform.GetChild(2).GetChild(1).gameObject;
        sceneEnd = this.gameObject.transform.GetChild(3).GetChild(1).gameObject;
    }
    
    public void enterSceneHit(GameObject col)
    {
        col.transform.position = sceneEnd.transform.position;
        Vector3 pos = scExit.transform.position;
        cam.transform.position = new Vector3(pos.x, pos.y, cam.transform.position.z);
    }

    public void exitSceneHit(GameObject col)
    {
        col.transform.position = sceneStart.transform.position;
        Vector3 pos = scEnter.transform.position;
        cam.transform.position = new Vector3(pos.x, pos.y, cam.transform.position.z);
    }
}

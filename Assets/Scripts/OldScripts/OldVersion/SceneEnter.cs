using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneEnter : MonoBehaviour
{
    private GameObject parent;
    // Start is called before the first frame update
    void Start()
    {
        parent = gameObject.transform.parent.parent.gameObject;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.tag == "Player")
        {
            Debug.Log("UT");
            parent.GetComponent<SceneChangeScript>().enterSceneHit(col.gameObject);
        }
    }
}

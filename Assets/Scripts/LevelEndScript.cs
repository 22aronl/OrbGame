using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelEndScript : MonoBehaviour
{
    public string levelEnd;
    public GameObject end;
    changeLevel levelScript;
    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.tag == "Player")
            levelScript = end.GetComponent<changeLevel>();
            //SceneManager.LoadScene("Scenes/" + levelEnd);
            levelScript.FadeToLevel(levelEnd);
    }
}

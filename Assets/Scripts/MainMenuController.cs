using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    public AudioClip buttonPress;
    void Start()
    {
        
    }

    public void PlayButton()
    {
        SoundManager.Instance.PlaySound(buttonPress);
    }


    public void PlayGame()
    {
        SoundManager.Instance.StopAllSound();
        SceneManager.LoadScene("Scenes/NPC TALK"); 
    }



    public void Quit()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}

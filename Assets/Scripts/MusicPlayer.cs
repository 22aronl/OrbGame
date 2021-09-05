using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicPlayer : MonoBehaviour
{

    public AudioClip bgm;
    // Start is called before the first frame update
    void Start()
    {
        SoundManager.Instance.PlayMusic(bgm);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        transform.Find("MasterVolume").Find("Slider").GetComponent <Slider> ().value = SoundManager.Instance.getMaster();
        transform.Find("SFXVolume").Find("Slider").GetComponent <Slider> ().value = SoundManager.Instance.getSFX();
        transform.Find("MusicVolume").Find("Slider").GetComponent <Slider> ().value = SoundManager.Instance.getMusic();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("Sound Manager");
        float vol = transform.Find("MasterVolume").Find("Slider").GetComponent <Slider> ().value;
        SoundManager.Instance.SetMasterVolume(vol);

        float sfx = transform.Find("SFXVolume").Find("Slider").GetComponent <Slider> ().value;
        Debug.Log("SFX" + sfx);
        SoundManager.Instance.SetSFXVolume(sfx);

        float music = transform.Find("MusicVolume").Find("Slider").GetComponent <Slider> ().value;
        Debug.Log("music" + music);
        SoundManager.Instance.SetMusicVolume(music);
    }
}

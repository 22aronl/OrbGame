
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;


public class TextNPC : MonoBehaviour
{
    public TextMeshProUGUI currentText;
    string fullText;

    [SerializeField]
    GameObject npc;

    [SerializeField]
    string text1;
    [SerializeField]
    string text2;
    [SerializeField]
    string text3;
    [SerializeField]
    string text4;
    [SerializeField]
    string text5;
    [SerializeField]
    string text6;
    [SerializeField]
    string text7;
    [SerializeField]
    string text8;
    [SerializeField]
    string text9;
    [SerializeField]
    string text10;
    [SerializeField]
    string text11;
    [SerializeField]
    string text12;
    [SerializeField]
    string text13;
    [SerializeField]
    string text14;
    [SerializeField]
    string text15;
    [SerializeField]
    string text16;
    [SerializeField]
    string text17;
    [SerializeField]
    string text18;
    [SerializeField]
    string text19;
    [SerializeField]
    string text20;

    string[] texts;
    int index;
    public AudioClip txtSound;

    // Start is called before the first frame update
    void Start()
    {
        currentText = GetComponent<TextMeshProUGUI>();
        fullText = text1;
        currentText.text = "";
        texts = new string[] { text1, text2, text3, text4, text5, text6, text7, text8, text9, text10, text11, text12, text13, text14, text15, text16, text17, text18, text19, text20 };
        index = 1;

        StartCoroutine("TextType");
    }

    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.C) && currentText.text.Equals(fullText))
        {
            if (texts[index] == "")
            {
                currentText.text = "";
                fullText = "";
                index = 0;
                //if (SceneManager.GetActiveScene().name.Equals("NPC TALK"))
                //{
                //    currentText.text = "";
                //    fullText = "";
                //    index = 0;
                //    npc.GetComponent<NPC>().setTextBox();
                //}
                //else
                //{
                //    Debug.Log("Load scene");
                //    SceneManager.LoadScene("DungeonScene");
                //}
            }
            else
            {
                currentText.text = "";
                fullText = texts[index];
                index += 1;
                StartCoroutine("TextType");
            }
        }
        if(Input.GetKeyDown(KeyCode.K))
        {
            SceneManager.LoadScene("Scenes/Level1");
        }
    }

    IEnumerator TextType()
    {
        if (SceneManager.GetActiveScene().name.Equals("NPC TALK"))
        {
            SoundManager.Instance.PlaySound(txtSound);
        }
        else
        {
            GetComponent<AudioSource>().Play();
        }
        for (int i = 0; i < fullText.Length; i++)
        {
            currentText.text += fullText[i];
            yield return new WaitForSeconds(0.025f);
        }
        if (SceneManager.GetActiveScene().name.Equals("NPC TALK"))
        {
            SoundManager.Instance.StopAllSound();
        }
        else
        {
            GetComponent<AudioSource>().Stop();
        }

        yield return null;
    }
}
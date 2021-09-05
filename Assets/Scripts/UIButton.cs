using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIButton : MonoBehaviour
{
    Animator anim;
    public float startFadeTime = 1.0f;
    public float endFadeTime = 1.0f;
    public Image im;
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        //yield return new WaitForSeconds(startFadeTime);
        //
        yield return new WaitForSeconds(5f);
        //img.color.a
        anim.SetBool("FadeIn", true);
        float elapsed = 0f;
        while (elapsed < endFadeTime)
        {
            elapsed += Time.deltaTime * Time.timeScale;

            im.color = new Color(im.color.r, im.color.g, im.color.b, elapsed / endFadeTime);
            yield return 0;
        }
    }

}

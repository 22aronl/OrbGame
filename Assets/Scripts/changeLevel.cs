using UnityEngine;
using UnityEngine.SceneManagement;

public class changeLevel : MonoBehaviour
{
    public Animator anim;
    private string lvl;

    public void FadeToLevel(string levelIndex)
    {
        lvl = levelIndex;
        anim.SetTrigger("FadeOut");
    }

    public void OnFadeComplete()
    {
        SceneManager.LoadScene("Scenes/" + lvl);
    }
}

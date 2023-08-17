using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public Animator transition;
    public float duration;

   
    private void OnEnable()
    {
        //GameManager.instance.buttonNavigation.sceneLoader = this;
        Debug.Log("PLAY");
    }

    public void PlayAnimation(string s)
    {
        StartCoroutine(Anim(s));
    }
    
    IEnumerator Anim(string s)
    {
        yield return new WaitForSecondsRealtime(0.1f);
        transition.SetTrigger("Start");
        yield return new WaitForSecondsRealtime(duration);
        SceneManager.LoadScene(s);
    }
}

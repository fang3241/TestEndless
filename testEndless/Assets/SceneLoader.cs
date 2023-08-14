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
        Debug.Log("PLAY");
    }

    public void PlayAnimation(string s)
    {
        
        StartCoroutine(Anim(s));
    }

    IEnumerator Anim(string s)
    {
        transition.SetTrigger("Start");
        yield return new WaitForSeconds(duration);
        SceneManager.LoadScene(s);
    }
}

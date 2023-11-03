using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private bool isCountRunning;
    //public GameObject ui;

    public void StartCountdown()
    {
        this.transform.GetChild(0).gameObject.SetActive(true);

    }

    private void OnEnable()
    {
        isCountRunning = false;

        Count(3);
    }
    
    private void Count(int sec)
    {
        if (!isCountRunning)
        {
            StartCoroutine(Counter(sec));
        }
    }

    IEnumerator Counter(int sec)
    {
        AudioManager.instance.StopAll();
        Time.timeScale = 0;
        isCountRunning = true;
        for(int i = sec; i > 0; i--)
        {
            countText.text = i.ToString();
            yield return new WaitForSecondsRealtime(1);
        }
        countText.text = "MULAI...";
        yield return new WaitForSecondsRealtime(0.8f);
        isCountRunning = false;
        Time.timeScale = 1;

        this.transform.GetChild(0).gameObject.SetActive(false);
        if(GameManager.instance.buttonNavigation.getCurrentSceneName() == "LevelLand")
        {
            AudioManager.instance.Play("lagu1");
        }else if (GameManager.instance.buttonNavigation.getCurrentSceneName() == "LevelSea")
        {
            AudioManager.instance.Play("lagu2");
        }else if (GameManager.instance.buttonNavigation.getCurrentSceneName() == "LevelAir")
        {
            AudioManager.instance.Play("lagu3");
        }
    }

    public bool getStatus()
    {
        return isCountRunning;
    }
}

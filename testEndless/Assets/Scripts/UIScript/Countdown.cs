using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Countdown : MonoBehaviour
{
    public TextMeshProUGUI countText;
    private bool isCountRunning;

    public void StartCountdown()
    {
        this.gameObject.SetActive(true);

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

        this.gameObject.SetActive(false);

    }

    public bool getStatus()
    {
        return isCountRunning;
    }
}

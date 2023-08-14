using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomQController : MonoBehaviour
{
    public TextMeshProUGUI title, tBenar, tSalah, qTotal, question, questionWImage;

    public GameObject panelWOutImage, panelWithImage, pausePanel, panelEnd;//enable disable

    public Countdown countdown;

    public Image qImage;
    public Button[] options;

    public Slider timerSlider;
    public TextMeshProUGUI timerText;

    public int benar, salah, total, current;

    public bool isLevelEnd;

    private bool isLevelPaused;

    // Start is called before the first frame update
    void Start()
    {
        isLevelEnd = false;
        isLevelPaused = false;
        benar = 0;
        salah = 0;
        total = GameManager.instance.selectedMaxQuestion;
        current = 0;

        title.text = GameManager.instance.customTitle;
        timerSlider.maxValue = GameManager.instance.selectedMaxCounter;
        timerSlider.value = timerSlider.maxValue;
        
        UpdateUI();

        countdown.StartCountdown();

        LoadNextQuestion();
    }

    public void ResetTimer()
    {
        timerSlider.value = GameManager.instance.selectedMaxCounter;
    }

    private void Update()
    {
        if (!isLevelEnd)
        {
            timerSlider.value -= 1 * Time.deltaTime;
            timerText.text = "Sisa Waktu : " + ((int)timerSlider.value);
            if (timerSlider.value == timerSlider.minValue)
            {
                //salah+
                //resettimer
                Debug.Log("Waktu habis");
                salah++;
                ResetTimer();
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                ShowPause();
            }
        }

    }

    public void ShowPause()
    {
        isLevelPaused = !isLevelPaused;

        Debug.Log("paused " + isLevelPaused + " " + pausePanel == null);

        if (!countdown.getStatus())
        {
            if (isLevelPaused)
            {
                Time.timeScale = 0;

            }
            else
            {
                Time.timeScale = 1;
                countdown.StartCountdown();
            }

            pausePanel.SetActive(isLevelPaused);
        }
        
    }

    public void Answer(int i)
    {
        char[] opt = { 'A', 'B', 'C', 'D' };


        Debug.Log(GameManager.instance.customSoal[current].answer + " " + opt[i]);
        if(GameManager.instance.customSoal[current].answer == opt[i])
        {
            benar++;
            Debug.Log("Benar");
        }
        else
        {
            salah++;
            Debug.Log("Salah");
        }

        current++;
        if(current == total)
        {
            Finish();
        }
        else
        {
            ResetTimer();
            UpdateUI();
            LoadNextQuestion();
            //load next soal
        }
        
    }

    public void UpdateUI()
    {
        tBenar.text = "Benar : " + benar.ToString();
        tSalah.text = "Salah : " + salah.ToString();
        qTotal.text = "Total : " + current + " / " + total;
    }

    public void Finish()
    {
        //start panel
        isLevelEnd = true;
        Debug.Log("END");
        TextMeshProUGUI benarText, salahText, totalText;

        benarText = panelEnd.transform.GetChild(1).GetChild(1).GetChild(0).GetComponent<TextMeshProUGUI>();
        salahText = panelEnd.transform.GetChild(1).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        totalText = panelEnd.transform.GetChild(1).GetChild(1).GetChild(2).GetComponentInChildren<TextMeshProUGUI>();

        benarText.text = "Benar : " + benar.ToString();
        salahText.text = "Salah : " + salah.ToString();
        totalText.text = "Nilai : " + ((float)benar / total)*100;

        Debug.Log(benar / total);

        panelEnd.SetActive(true);

    }

    public void LoadNextQuestion()
    {
        QuestionClass q = GameManager.instance.customSoal[current];

        if(q.questionSprite == null)
        {
            panelWOutImage.SetActive(true);
            panelWithImage.SetActive(false);
            question.text = q.question;
            //pake yg ada gambarnya
        }
        else
        {
            panelWOutImage.SetActive(false);
            panelWithImage.SetActive(true);
            questionWImage.text = q.question;
            qImage.sprite = q.questionSprite;
            //pake no gambar
        }

        for(int i = 0; i < 4; i++)
        {
            options[i].GetComponentInChildren<TextMeshProUGUI>().text = q.options[i];
        }

    }

    /*
     * pas start
     * (countdown opsional)
     * - set soal
     * - jalankan timer
     * - tangkap jawaban 
     * - kalo udah kejawab, cek jawaban dan reset timer
     * - kalo blom kejawab sampe waktu habis, salah +1, reset timer
     * 
     * - load next soal
     * 
     * - kalo udah selesai, nyalakan panel selesai, isinya
     * - benar, salah, total soal, nilai, tombol kembali ke menu pilih custom
     * 
     */
}

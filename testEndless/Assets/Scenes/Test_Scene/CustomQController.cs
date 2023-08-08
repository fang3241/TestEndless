using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CustomQController : MonoBehaviour
{
    public TextMeshProUGUI title, tBenar, tSalah, qTotal, question, questionWImage;

    public GameObject panelWOutImage, panelWithImage, panelEnd;//enable disable

    public Image qImage;
    public Button[] options;

    public Slider timerSlider;
    public TextMeshProUGUI timerText;

    public int benar, salah, total, current;

    public bool isLevelEnd;

    // Start is called before the first frame update
    void Start()
    {
        benar = 0;
        salah = 0;
        total = GameManager.instance.selectedMaxQuestion;
        current = 0;

        title.text = GameManager.instance.customTitle;
        timerSlider.maxValue = GameManager.instance.selectedMaxCounter;
        timerSlider.value = timerSlider.maxValue;

        UpdateUI();
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
            if(timerSlider.value == timerSlider.minValue)
            {
                //salah+
                //resettimer
                Debug.Log("Waktu habis");
                salah++;
                ResetTimer();
            }
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
            //finish
            
            
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
        tBenar.text = benar.ToString();
        tSalah.text = salah.ToString();
        qTotal.text = current + " / " + total;
    }

    public void Finish()
    {
        //start panel
        isLevelEnd = true;
        Debug.Log("END");
        TextMeshProUGUI benarText, salahText, totalText;

        benarText = panelEnd.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
        salahText = panelEnd.transform.GetChild(2).GetComponent<TextMeshProUGUI>();
        totalText = panelEnd.transform.GetChild(3).GetComponent<TextMeshProUGUI>();

        benarText.text = "Benar : " + benar.ToString();
        salahText.text = "Salah : " + salah.ToString();
        totalText.text = "Nilai : " + ((float)benar / total)*100;

        Debug.Log(benar / total);

        panelEnd.SetActive(true);

    }

    public void LoadNextQuestion()
    {
        QuestionClass q = GameManager.instance.customSoal[current];

        if(q.questionSprite != null)
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

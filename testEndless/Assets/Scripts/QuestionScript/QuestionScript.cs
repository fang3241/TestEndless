using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class QuestionScript : MonoBehaviour
{
    //jangan lupa tambahin question reader
    //read pertanyaan sama read jawaban
    //untuk sekarang kemungkinan ada 3 tipe pertanyaan untuk 3 stage
    //masing" ada x soal, dan nanti yg ditampilin random
    //misal ada 2 pertanyaan per level, dan ada 5 level per stage, jadi ada min 10 pertanyaan di stage itu yg diambil random

    public LevelController levelController;

    public Animator questionAnim;

    public GameObject QuestionPanel;
    public TextMeshProUGUI questionText, questionNumber;
    public List<TextMeshProUGUI> optionText;


    //private List<int> questions;//pertanyaan per stage dipisah aja variabelnya
    //private List<int,int> options;//index, index isi
    //private List<int> answers;
    

    public LevelQuestion[] a;


    public List<LevelQuestion> questions;
    
    private int selectedQuestion;
    private char selectedAnswer;

    
    // Start is called before the first frame update
    void Start()
    {
        questions = new List<LevelQuestion>();
        levelController = GameManager.instance.levelController;
        if(levelController == null)
        {
            Debug.Log("Kosong");
            levelController = GameObject.FindObjectOfType<LevelController>();
        }
        //StartCoroutine(ForceLoadQuestion());
        LoadQuestion();
    }

    public void LoadQuestion()
    {
        if (levelController.questionPools.Length != 0)
        {
            foreach (SoalBab bab in levelController.questionPools)
            {
                //Debug.Log(bab.bab);
                //Debug.Log(bab.levelQuestions.Length);
                foreach (LevelQuestion lq in bab.levelQuestions)
                {
                    //Debug.Log(lq);
                    questions.Add(lq);
                }
            }

            a = new LevelQuestion[questions.Count];
            int i = 0;
            foreach (LevelQuestion t in questions)
            {
                a[i] = t;
                i++;
            }

            Debug.Log("LOADED");
        }
        else
        {
            Debug.Log("POOL SOAL KOSONG");
        }
    }

    //IEnumerator ForceLoadQuestion()
    //{
        

        

    //    if (questions.Count == 0)
    //    {
    //        Debug.Log("RELOADING ");
    //        StartCoroutine(ForceLoadQuestion());
    //    }
    //    else
    //    {
    //        Debug.Log("QUESTION LOADED");
    //    }

    //    yield return null;
    //}

    public void SetQuestion()
    {
        questionNumber.text = "Soal " + levelController.questionCounter + " / " + levelController.maxQuestion;
        selectedQuestion = Random.Range(0, questions.Count);
        Debug.Log("q = " + selectedQuestion);
        Debug.Log("qc = " + questions.Count);
        levelController.correctAnswer = (char)questions[selectedQuestion].answer;

        Debug.Log((char)questions[selectedQuestion].answer);

        questionText.text = questions[selectedQuestion].question;

        char[] ops = { 'A', 'B', 'C', 'D' }; 

        for (int i = 0; i < 4; i++)
        {
            optionText[i].text = ops[i] + ". " + questions[selectedQuestion].options[i];
        }

        OpenPanel();

    }

    public void OpenPanel()
    {
        QuestionPanel.SetActive(true);
        questionAnim.SetTrigger("QuestionStart");
    }


    public void ClosePanel()
    {
        StartCoroutine(CloseAnimation());
    }
    
    IEnumerator CloseAnimation()
    {
        //yield return new WaitUntil(() => !AnimatorIsPlaying("QuestionFinish"));
        //questionAnim.SetTrigger("QuestionReset");
        //yield return new WaitUntil(() => !AnimatorIsPlaying("QuestionReset"));
        //QuestionPanel.SetActive(false); QuestionPanel.SetActive(false);

        questionAnim.SetTrigger("QuestionFinish");
        yield return new WaitForSeconds(1);
        questionAnim.SetTrigger("QuestionReset");
        yield return new WaitForSeconds(0.5f);
        QuestionPanel.SetActive(false);
    }

    
}

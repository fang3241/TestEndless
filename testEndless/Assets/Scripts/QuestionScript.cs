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

    private LevelController levelController;

    public GameObject QuestionPanel;
    public TextMeshProUGUI questionText;
    public List<TextMeshProUGUI> optionText;


    //private List<int> questions;//pertanyaan per stage dipisah aja variabelnya
    //private List<int,int> options;//index, index isi
    //private List<int> answers;
    

    public LevelQuestion[] a;


    private List<LevelQuestion> questions;
    
    private int selectedQuestion;
    private char selectedAnswer;
    


    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        //questionText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        //QuestionPanel = transform.GetChild(0).gameObject;

        questionText = QuestionPanel.transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();

        GameObject temp = QuestionPanel.transform.GetChild(0).GetChild(1).gameObject;
        foreach(TextMeshProUGUI a in temp.GetComponentsInChildren<TextMeshProUGUI>())
        {
            optionText.Add(a);
        }
        
        questions = new List<LevelQuestion>();

        if (levelController.questionPools.Length != 0)
        {
            Debug.Log(levelController.questionPools.Length);
            foreach (SoalBab bab in levelController.questionPools)
            {
                //Debug.Log(bab.bab);
                Debug.Log(bab.levelQuestions.Length);
                foreach (LevelQuestion lq in bab.levelQuestions)
                {
                    questions.Add(lq);
                }
            }
        }

        a = new LevelQuestion[questions.Count];

        int i = 0;
        foreach (LevelQuestion t in questions)
        {
            a[i] = t;
            i++;
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        //questions = new List<QuestionClass>(levelController.gameManager.qReader.questions);

       
    }


    public void SetQuestion()
    {
        
        selectedQuestion = Random.Range(0, questions.Count);
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
    }


    public void ClosePanel()
    {
        QuestionPanel.SetActive(false);
    }

    
}

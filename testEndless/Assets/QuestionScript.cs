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

    public TextMeshProUGUI questionText;
    public List<TextMeshProUGUI> optionText;

    private GameObject QuestionPanel;

    //private List<int> questions;//pertanyaan per stage dipisah aja variabelnya
    //private List<int,int> options;//index, index isi
    //private List<int> answers;

    private List<QuestionClass> questions;
    
    private int selectedQuestion;
    private char selectedAnswer;
    


    private void Awake()
    {
        levelController = GameObject.FindObjectOfType<LevelController>();
        questionText = transform.GetChild(0).GetChild(0).GetComponent<TextMeshProUGUI>();
        QuestionPanel = transform.GetChild(0).gameObject;
        
        foreach (TextMeshProUGUI txt in transform.GetChild(0).GetChild(1).GetComponentsInChildren<TextMeshProUGUI>())
        {
            optionText.Add(txt);
        }

    }


    // Start is called before the first frame update
    void Start()
    {
        questions = new List<QuestionClass>(levelController.gameManager.qReader.questions);

    }


    public void OpenPanel()
    {

        //selectedQuestion = Random.Range(0, 4);//misal doang
        //selectedAnswer = 'A';//misal juga
        //LevelController.selectedAnswer = selectedAnswer;
        //levelController.SetActiveAnswerSpawner(true);

        //levelController.isQuestionTriggered = true;
        levelController.QuestionMode();
        SetQuestion();
    }

    public void SetQuestion()
    {
        selectedQuestion = Random.Range(0, questions.Count);
        levelController.correctAnswer = questions[selectedQuestion].Answer;

        questionText.text = questions[selectedQuestion].Question;

        for(int i = 0; i < 4; i++)
        {
            optionText[i].text = questions[selectedQuestion].Options[i];
        }
        QuestionPanel.SetActive(true);


    }

    public void ClosePanel()
    {
        //levelController.isQuestionTriggered = false;
        //levelController.answerSpawner.isSpawned = true;
        QuestionPanel.SetActive(false);
    }

    
}

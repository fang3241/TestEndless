using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    /* - mengatur level loop level
     * - menjadi penghubung semua script di level (reference)
     * 
     */
    [Header("Reference(AUTOMATIC)")]
    public GameManager gameManager;
    public PlayerController player;

    //hide semua
    [HideInInspector]
    public ObjectiveController objectiveController;

    [HideInInspector]
    public QuestionScript QuestionScript;

    [HideInInspector]
    public AnswerSpawner answerSpawner;

    [HideInInspector]
    public ObstacleSpawner obstacleSpawner;

    [HideInInspector]
    public CollectableSpawner collectableSpawner;

    [HideInInspector]
    public GameObject ObsContainer;

    [HideInInspector]
    public SliderScript slider;

    public GameObject spawnerParent;
    public GameObject hpContainer;
    public GameObject LevelEndPanel;


    [Header("Level Manager Variables")]
    public float maxSliderCounter;
    public int maxQuestion;
    public SoalBab[] questionPools;

    
    [HideInInspector]
    public List<Image> hpIcoImg;

    [Header("Level Controller Variables")]
    public int hp;
    public float speedScaling;
    
    public int questionCounter;
    public int countCorrectAnswer;

    public char selectedAnswer;
    public char correctAnswer;

    public bool isQuestionSpawned;
    public bool isAnswerSpawned;
    public bool isAnswered;

    public bool isLevelEnd;

    private int ctr;
    
    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.levelController = this;

        //kalo mau, untuk menambah performa, kurangin penggunaan find
        QuestionScript = GameObject.FindObjectOfType<QuestionScript>();
        player = GameObject.FindObjectOfType<PlayerController>();
        slider = GameObject.FindObjectOfType<SliderScript>();
        objectiveController = GetComponent<ObjectiveController>();


        answerSpawner = spawnerParent.transform.GetChild(0).GetComponent<AnswerSpawner>();
        collectableSpawner = spawnerParent.transform.GetChild(1).GetComponent<CollectableSpawner>();
        obstacleSpawner = spawnerParent.transform.GetChild(2).GetComponent<ObstacleSpawner>();
        ObsContainer = spawnerParent.transform.GetChild(3).gameObject;

        hp = 3;
        speedScaling = 1;

        questionCounter = 0;
        countCorrectAnswer = 0;

        ctr = 0;
        
        for(int i = 0; i < hpContainer.transform.childCount; i++)
        {
            hpIcoImg.Add(hpContainer.transform.GetChild(i).GetComponent<Image>());
        }

    }

    void Start()
    {
        isLevelEnd = false;
        isAnswerSpawned = false;
        isQuestionSpawned = false;
        isAnswered = false;
        //answerSpawner.gameObject.SetActive(false);

    }

    private void Update()
    {
        

    }

    public void StartQuestion()
    {
        ctr = 0;
        isQuestionSpawned = !isQuestionSpawned;
        isAnswered = false;

        obstacleSpawner.canSpawn = false;
        collectableSpawner.canSpawn = false;

        if (isQuestionSpawned)
        {
            foreach (Transform a in ObsContainer.transform)
            {
                Destroy(a.gameObject);
            }
        }

        QuestionScript.SetQuestion();
        questionCounter++;
        Debug.Log("isQuestionSpawned : " + isQuestionSpawned);
    }
    
    public void StartAnswer()
    {
        answerSpawner.SpawnAnswer();
        isAnswerSpawned = true;
        Debug.Log("isAnswerSpawned : " + isAnswerSpawned);
    }

    public void AnswerChecker()
    {
        ctr++;
        if (isAnswered && ctr == 1)
        {
            if(selectedAnswer != correctAnswer)
            {
                player.Hit(1);
                Debug.Log("Jawaban Salah");
            }
            else
            {
                objectiveController.objectives[1].addProgress();
                Debug.Log("Jawaban Benar");
                countCorrectAnswer++;
            }
            isAnswered = false;
            isAnswerSpawned = false;
            slider.Switch();
        }
       

        LevelEndChecker();
        QuestionScript.ClosePanel();
        

        Debug.Log(selectedAnswer + " " + ctr);
        
    }

    public void ResetQuestionStatus()
    {
        isAnswerSpawned = false;
        isQuestionSpawned = false;

        obstacleSpawner.canSpawn = true;
        collectableSpawner.canSpawn = true;
    }

   
    public void LevelEndChecker()
    {
        if(questionCounter == maxQuestion)
        {
            Debug.Log("Level Complete");
            speedScaling = 0;
            isLevelEnd = true;
            OpenEndPanel();
        }
        
    }

    public void OpenEndPanel()
    {
        LevelEndPanel.SetActive(true);
    }

    public void ResetAll()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void Lose()
    {
        Debug.Log("Lose");
        speedScaling = 0;
        player.gameObject.SetActive(false);
        //tampilkan panel lose
    }


    //public void QuestionMode()
    //{
    //    isQuestionSpawned = !isQuestionSpawned;

    //    if (isQuestionSpawned)
    //    {
    //        foreach (Transform a in ObsHolder.transform)
    //        {
    //            Destroy(a.gameObject);
    //        }
    //    }

    //    Debug.Log("isSpawned : " + isQuestionSpawned);
    //}

    //public void AnswerMode()
    //{
    //    isAnswerSpawned = !isAnswerSpawned;
    //    Debug.Log("isSpawned : " + isAnswerSpawned);
    //}

}

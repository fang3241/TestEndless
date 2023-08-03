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
    
    public QuestionScript QuestionScript;

    [HideInInspector]
    public AnswerSpawner answerSpawner;

    [HideInInspector]
    public ObstacleSpawner obstacleSpawner;

    [HideInInspector]
    public CollectableSpawner collectableSpawner;

    [HideInInspector]
    public GameObject ObsContainer;
    
    public SliderScript slider;

    public GameObject spawnerParent;
    public GameObject hpContainer;
    public GameObject LevelLosePanel;


    [Header("Level Manager Variables")]
    public float maxSliderCounter;
    public int maxQuestion;
    public int maxReadingTime;
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

    public bool ISLOADED = false;

    private int ctr;
    
    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.levelController = this;

        maxSliderCounter = gameManager.selectedMaxCounter;
        maxQuestion = gameManager.selectedMaxQuestion;
        maxReadingTime = gameManager.selectedMaxReadingTime;

        questionPools = new SoalBab[gameManager.selectedBabs];

        for (int i = 0; i < gameManager.selectedBabs; i++)
        {
            questionPools[i] = gameManager.kumpulanSoal[i];
        }

        


        //kalo mau, untuk menambah performa, kurangin penggunaan find
        QuestionScript = GameObject.FindObjectOfType<QuestionScript>();
        player = GameObject.FindObjectOfType<PlayerController>();
        slider = GameObject.FindObjectOfType<SliderScript>();
        objectiveController = GetComponent<ObjectiveController>();


        answerSpawner = spawnerParent.transform.GetChild(0).GetComponent<AnswerSpawner>();
        collectableSpawner = spawnerParent.transform.GetChild(1).GetComponent<CollectableSpawner>();
        obstacleSpawner = spawnerParent.transform.GetChild(2).GetComponent<ObstacleSpawner>();
        ObsContainer = spawnerParent.transform.GetChild(3).gameObject;


        QuestionScript.levelController = this;
        slider.levelController = this;
        slider.slider.maxValue = maxSliderCounter;



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

    private IEnumerator WaitForGM()
    {
        yield return new WaitUntil(() =>
        {

            gameManager = GameManager.instance;
            gameManager.levelController = this;

            maxSliderCounter = gameManager.selectedMaxCounter;
            maxQuestion = gameManager.selectedMaxQuestion;
            maxReadingTime = gameManager.selectedMaxReadingTime;

            questionPools = new SoalBab[gameManager.selectedBabs];

            for (int i = 0; i < gameManager.selectedBabs; i++)
            {
                questionPools[i] = gameManager.kumpulanSoal[i];
            }

            QuestionScript.levelController = this;
            slider.levelController = this;
            slider.slider.maxValue = maxSliderCounter;

            return gameManager != null;

            
        });

        if(gameManager == null)
        {
            StartCoroutine(WaitForGM());
        }
        else
        {
            Debug.Log("LOADED");
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
   

    public void SpawnQuestion()
    {
        Debug.Log("spawnQuestion");

        questionCounter++;

        isQuestionSpawned = true;
        isAnswerSpawned = false;
        isAnswered = false;

        Debug.Log("isquestionSpawned : " + isQuestionSpawned);
        Debug.Log("isanswerSpawned : " + isAnswerSpawned);
        Debug.Log("isanswered : " + isAnswered);

        DisableOrEnableSpawned();
        QuestionScript.SetQuestion();
    }

    public void SpawnAnswer()
    {
        Debug.Log("answerSpawned");
        isQuestionSpawned = true;
        isAnswerSpawned = true;
        isAnswered = false;

        Debug.Log("isquestionSpawned : " + isQuestionSpawned);
        Debug.Log("isanswerSpawned : " + isAnswerSpawned);
        Debug.Log("isanswered : " + isAnswered);

        answerSpawner.SpawnAnswer();
    }

    public void Answered()
    {
        Debug.Log("Answered");
        isQuestionSpawned = true;
        isAnswerSpawned = true;
        isAnswered = true;

        Debug.Log("isquestionSpawned : " + isQuestionSpawned);
        Debug.Log("isanswerSpawned : " + isAnswerSpawned);
        Debug.Log("isanswered : " + isAnswered);

        AnswerChecker();
        
    }

    public void StateReset()
    {
        Debug.Log("Resetting");
        isQuestionSpawned = false;
        isAnswerSpawned = false;
        isAnswered = false;
        
        Debug.Log("isquestionSpawned : " + isQuestionSpawned);
        Debug.Log("isanswerSpawned : " + isAnswerSpawned);
        Debug.Log("isanswered : " + isAnswered);

        LevelEndChecker();
        QuestionScript.ClosePanel();
        DisableOrEnableSpawned();
    }

    public void DisableOrEnableSpawned()
    {
        obstacleSpawner.canSpawn = !isQuestionSpawned;
        collectableSpawner.canSpawn = !isQuestionSpawned;

        ctr = 0;

        if (isQuestionSpawned)
        {
            foreach (Transform a in ObsContainer.transform)
            {
                Destroy(a.gameObject);
            }
        }
        
    }

    public void AnswerChecker()
    {
        ctr++;
        if (ctr == 1)
        {
            if (selectedAnswer != correctAnswer)
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

            //slider.Switch();
        }


        //Debug.Log(selectedAnswer + " " + ctr);

    }

    public void LevelEndChecker()
    {
        if(questionCounter == maxQuestion)
        {
            Debug.Log("Level Complete");
            speedScaling = 0;
            isLevelEnd = true;
            OpenEndPanel();//win panel
        }
        
    }

    public void OpenEndPanel()
    {
        objectiveController.ShowWinPanel();
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
        isLevelEnd = true;
        objectiveController.ShowLosePanel();
        //tampilkan panel lose
    }




    //public void StartQuestion()
    //{
    //    ctr = 0;
    //    isQuestionSpawned = !isQuestionSpawned;
    //    isAnswered = false;

    //    obstacleSpawner.canSpawn = false;
    //    collectableSpawner.canSpawn = false;

    //    if (isQuestionSpawned)
    //    {
    //        foreach (Transform a in ObsContainer.transform)
    //        {
    //            Destroy(a.gameObject);
    //        }
    //    }

    //    QuestionScript.SetQuestion();
    //    questionCounter++;
    //    Debug.Log("isQuestionSpawned : " + isQuestionSpawned);
    //}

    //public void StartAnswer()
    //{
    //    answerSpawner.SpawnAnswer();
    //    isAnswerSpawned = true;
    //    Debug.Log("isAnswerSpawned : " + isAnswerSpawned);
    //}

    

    //public void ResetQuestionStatus()
    //{
    //    isAnswerSpawned = false;
    //    isQuestionSpawned = false;

    //    obstacleSpawner.canSpawn = true;
    //    collectableSpawner.canSpawn = true;
    //}

}

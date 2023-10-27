using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    public ObjectiveController objectiveController;
    
    public QuestionScript QuestionScript;
    
    [HideInInspector]
    public AnswerSpawner answerSpawner;
    
    public ObstacleSpawner obstacleSpawner;
    
    public CollectableSpawner collectableSpawner;

    [HideInInspector]
    public GameObject ObsContainer;
    
    public SliderScript slider;

    public GameObject spawnerParent;
    public GameObject hpContainer;


    [Header("Level Manager Variables")]
    public float maxSliderCounter;
    public int maxQuestion;
    public int maxReadingTime;
    public SoalBab[] questionPools;

    
    [HideInInspector]
    public List<Image> hpIcoImg;


    [Header("Level Controller Variables")]
    public GameObject pausePanel;
    public TextMeshProUGUI levelName;
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
    public bool isLevelPaused;

    public bool ISLOADED = false;

    public int ctr;
    
    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.levelController = this;

        maxSliderCounter = gameManager.selectedMaxCounter;
        maxQuestion = gameManager.selectedMaxQuestion;
        maxReadingTime = gameManager.selectedMaxReadingTime;

        questionPools = new SoalBab[gameManager.selectedBab];

        for (int i = 0; i < gameManager.selectedBab; i++)
        {
            questionPools[i] = gameManager.kumpulanSoal[i];
        }
        
        QuestionScript = GameObject.FindObjectOfType<QuestionScript>();
        player = GameObject.FindObjectOfType<PlayerController>();
        slider = GameObject.FindObjectOfType<SliderScript>();
        objectiveController = GetComponent<ObjectiveController>();
        
        answerSpawner = spawnerParent.transform.GetChild(0).GetComponent<AnswerSpawner>();
        collectableSpawner = spawnerParent.transform.GetChild(1).GetComponent<CollectableSpawner>();
        obstacleSpawner = spawnerParent.transform.GetChild(2).GetComponent<ObstacleSpawner>();
        ObsContainer = spawnerParent.transform.GetChild(3).gameObject;
        
        slider.levelController = this;
        slider.slider.maxValue = maxSliderCounter;

        slider.pointRate = 1;

        hp = 3;
        speedScaling = GameManager.instance.selectedSpeedScaling;

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
        isLevelPaused = false;
        isLevelEnd = false;
        isAnswerSpawned = false;
        isQuestionSpawned = false;
        isAnswered = false;

        levelName.text = "Level " + (GameManager.instance.selectedChapter) + "-" + (GameManager.instance.selectedLevel);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isLevelEnd && !objectiveController.countdown.getStatus())
        {
            ShowPause();
        }

    }

    public void ShowPause()
    {
        isLevelPaused = !isLevelPaused;
        
        Debug.Log("paused " + isLevelPaused + " " + pausePanel == null);

        if (isLevelPaused)
        {
            objectiveController.UpdateObjectiveCounter();
            Time.timeScale = 0;

        }
        else
        {
            Time.timeScale = 1;
            objectiveController.countdown.StartCountdown();
        }

        pausePanel.SetActive(isLevelPaused);

    }
    
    public void SpawnQuestion()
    {
        Debug.Log("spawnQuestion");

        questionCounter++;

        objectiveController.objectives[0].addProgress();
        isQuestionSpawned = true;
        isAnswerSpawned = false;
        isAnswered = false;
        
        DisableOrEnableSpawned(false);
        QuestionScript.SetQuestion();
    }

    public void SpawnAnswer()
    {
        Debug.Log("answerSpawned");
        isQuestionSpawned = true;
        isAnswerSpawned = true;
        isAnswered = false;
        
        answerSpawner.SpawnAnswer();
    }

    public void Answered()
    {
        Debug.Log("Answered");
        isQuestionSpawned = true;
        isAnswerSpawned = true;
        isAnswered = true;
        
        AnswerChecker();
        
    }

    public void StateReset()
    {
        Debug.Log("Resetting");
        isQuestionSpawned = false;
        isAnswerSpawned = false;
        isAnswered = false;
        
        LevelEndChecker();
        QuestionScript.ClosePanel();
        DisableOrEnableSpawned(true);
    }

    public void DisableOrEnableSpawned(bool stat)
    {
        if (!isLevelEnd)
        {
            ctr = 0;

            if (!stat)
            {
                obstacleSpawner.canSpawn = false;
                collectableSpawner.canSpawn = false;
                foreach (Transform a in ObsContainer.transform)
                {
                    Destroy(a.gameObject);
                }
            }
            else
            {
                obstacleSpawner.canSpawn = true;
                collectableSpawner.canSpawn = true;
            }
        }
        

        Debug.Log("RESETED");
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
            //StateReset();
        }

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
        //GameManager.instance.SaveProgress(objectiveController.objectives);
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
        DisableOrEnableSpawned(false);
        objectiveController.ShowLosePanel();
    }




}

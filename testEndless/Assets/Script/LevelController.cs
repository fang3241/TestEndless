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
     * 
     */ 


    public GameObject hpParentGO;
    public GameObject ObsHolder;
    public List<Image> hpIcoImg;
    public GameObject LevelEndPanel;

    public float speedScaling;
    public float maxCounter;

    public int questionCounter;
    public int maxQuestion;

    public int hp;
    public char selectedAnswer;

    public char correctAnswer;

    public GameManager gameManager;
    public PlayerController player;
    public ObjectiveController objectiveController;
    public QuestionScript QuestionScript;
    public AnswerSpawner answerSpawner;
    public SliderScript slider;

    public bool isAnswerSpawned;
    public bool isQuestionSpawned;

    public bool isLevelEnd;
    
    private void Awake()
    {
        gameManager = GameManager.instance;
        gameManager.levelController = this;

        QuestionScript = GameObject.FindObjectOfType<QuestionScript>();
        
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<SliderScript>();
        hp = 3;
        speedScaling = 1;
        maxCounter += 0.1f;

        questionCounter = 0;
        maxQuestion = 2;


        isLevelEnd = false;
        isAnswerSpawned = false;
        isQuestionSpawned = false;
    }

    void Start()
    {
        objectiveController = GetComponent<ObjectiveController>();
        foreach (Image image in hpParentGO.GetComponentsInChildren<Image>())
        {
            if(image.name != hpParentGO.name)
                hpIcoImg.Add(image);
        }
        //answerSpawner.gameObject.SetActive(false);

    }

    private void Update()
    {
        

    }
    
    public void AnswerChecker()
    {
        if(selectedAnswer != correctAnswer)
        {
            player.Hit();
        }
        else
        {
            objectiveController.objectives[1].addProgress();
        }

        questionCounter++;
        LevelEnd();
        QuestionMode();
        AnswerMode();
        slider.Switch();
        QuestionScript.ClosePanel();
    }

    public void QuestionMode()
    {
        isQuestionSpawned = !isQuestionSpawned;

        if (isQuestionSpawned)
        {
            foreach (Transform a in ObsHolder.transform)
            {
                Destroy(a.gameObject);
            }
        }
        
        Debug.Log("isSpawned : " + isQuestionSpawned);
    }

    public void AnswerMode()
    {
        isAnswerSpawned = !isAnswerSpawned;
        Debug.Log("isSpawned : " + isAnswerSpawned);
    }

    public void LevelEnd()
    {
        if(questionCounter == maxQuestion)
        {
            Debug.Log("Level Complete");
            speedScaling = 0;
            isLevelEnd = true;
            openPanelEnd();
        }
        
    }

    public void openPanelEnd()
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
    
}

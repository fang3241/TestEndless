using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ObjectiveController : MonoBehaviour
{
    public LevelController levelController;
    public GameObject pausePanel;
    public GameObject winPanel;
    public GameObject losePanel;
    public TextMeshProUGUI[] objText;
    public TextMeshProUGUI[] objProgress;
    public Objective[] objectives;

    public List<Image> levelWinStars;
    public Sprite emptyStar, FilledStar;
    public TextMeshProUGUI Qtotal;
    public TextMeshProUGUI QCorrect;


    public enum ObjectiveName
    {
        bar, hp, hit
    }


    public int[] objCounter;

    private void Awake()
    {
        objText = new TextMeshProUGUI[3];
        objProgress = new TextMeshProUGUI[3];

        int t = 0;
        Debug.Log(pausePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).gameObject.name);
        foreach (TextMeshProUGUI a in pausePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(0).GetComponentsInChildren<TextMeshProUGUI>())
        {
            objText[t] = a;
            t++;
        }

        //Debug.Log(pausePanel.transform.GetChild(0).GetChild(1).GetChild(1).gameObject.name);

        t = 0;
        foreach (TextMeshProUGUI a in pausePanel.transform.GetChild(0).GetChild(1).GetChild(0).GetChild(1).GetComponentsInChildren<TextMeshProUGUI>())
        {
            objProgress[t] = a;
            t++;
        }

        foreach (Image i in winPanel.transform.GetChild(0).GetChild(1).GetChild(0).GetComponentsInChildren<Image>())
        {
            levelWinStars.Add(i);
        }


        Qtotal = winPanel.transform.GetChild(0).GetChild(1).GetChild(1).GetComponent<TextMeshProUGUI>();
        QCorrect = winPanel.transform.GetChild(0).GetChild(1).GetChild(2).GetComponent<TextMeshProUGUI>();


    }


    private void Start()
    {
        levelController = GetComponent<LevelController>();
        objCounter = new int[3] { 0, 0, 0 };
        objectives = new Objective[3];

        //brp x bar terisi => brp kali pertanyaan
        //disamain sama max question, anggap +1 star kalo menyelesaikan level
        objectives[0] = new Objective_Bar(levelController, levelController.maxQuestion);

        int minAnswer = (levelController.maxQuestion / 2) + 1;
        objectives[1] = new Objective_Answer(minAnswer);//brp x jawab benar
        objectives[2] = new Objective_Hit(2);//brp x boleh ke hit

       
        for (int i = 0; i < 3; i++)
        {
            objText[i].text = objectives[i].getNama();
        }

       

    }

    private void Update()
    {
        //sementara
        if (Input.GetKeyDown(KeyCode.Escape) && !levelController.isLevelEnd)
        {
            ShowPause();
        }
                
    }

    public void ShowPause()
    {
        updateObjectiveCounter();
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        if (pausePanel.activeSelf)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }

    public void updateObjectiveCounter()
    {
        for (int i = 0; i < 3; i++)
        {
            objCounter[i] = objectives[i].getProgress();
            objProgress[i].text = objectives[i].getProgressStatus();
        }
    }

    public void ShowWinPanel()
    {
        for(int i = 0; i < 3; i++)
        {
            if (objectives[i].statusChecker())
            {
                levelWinStars[i].sprite = FilledStar;
            }
            else
            {
                levelWinStars[i].sprite = emptyStar;
            }
        }
        Qtotal.text = "Total Soal : " + levelController.questionCounter;
        QCorrect.text = "Total Benar : " + levelController.countCorrectAnswer;
        winPanel.SetActive(true);

        GameManager.instance.SaveProgress(objectives);
    }

    public void ShowLosePanel()
    {
        losePanel.SetActive(true);
    }

    //kalo load panel, panggil fungsi obj masing masing

}

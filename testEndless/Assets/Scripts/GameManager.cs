using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [Header("GM Reference")]
    public QuestionReader qReader;
    public LevelController levelController;
    public ButtonNav buttonNavigation;
    private Objective[] savedObjective;

    [Header("Save and Load Variables")]
    public int currentUnlockedLevel;//level terjauh yg terbuka
    public int lastFinishedLevel;//level terakhir yg terbuka

    [Header("Level Manager Variables")]
    public int selectedChapter;
    public int selectedLevel;
    public float selectedSpeedScaling;

    [Header("Level Controller Variables")]
    public int selectedMaxCounter;
    public int selectedMaxQuestion;
    public int selectedMaxReadingTime;
    public int selectedBabs;
    public bool[] selectedObjLevelStatus;


    [Header("Custom Level Variables")]
    //pilgan
    public SoalBab[] kumpulanSoal;
    public QuestionClass[] customSoal;

    //cocok gambar
    public DragImageController DIcontroller;
    public Sprite[] imageLists;
    public string[] answerLists;

    //all
    public int customType;//0 cocok gambar || 1 pilgan
    public string customTitle;
    
    

    private void Awake()
    {
        if(GameManager.instance != null)
        {
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);

        qReader = this.GetComponent<QuestionReader>();
        buttonNavigation = this.GetComponent<ButtonNav>();
        selectedChapter = -1;
        selectedLevel = -1;
        customType = -1;
    }

    private void Start()
    {
        currentUnlockedLevel = 15;
        selectedObjLevelStatus = new bool[3] { false, false, false };
    }

    public void LoadProgress()
    {
        string load = PlayerPrefs.GetString("ch" + selectedChapter + "lv" + selectedLevel, "-0-0-0");

        int i = 0;
        foreach(string a in load.Split('-'))
        {
            if(a != "")
            {
                if(a == "0")
                {
                    selectedObjLevelStatus[i] = false;
                }
                else
                {
                    selectedObjLevelStatus[i] = true;
                }
                i++;
            }
        }
    }

    public void SaveProgress(Objective[] finishedObj)
    {
        string save = ConvertOBJtoString(finishedObj);
        lastFinishedLevel = selectedLevel;
        lastFinishedLevel++;
        if(currentUnlockedLevel == lastFinishedLevel)
        {
            currentUnlockedLevel++;
            PlayerPrefs.SetInt("unlockedLevel", currentUnlockedLevel);
            PlayerPrefs.SetString("ch" + selectedChapter + "lv" + selectedLevel, save);
        }
    }

    private string ConvertOBJtoString(Objective[] obj)
    {
        string t = "";
        //t += "-" + ("ch" + selectedChapter);
        //t += "-" + ("lv" + selectedLevel);

        foreach(Objective a in obj)
        {
            if (a.statusChecker())
            {
                t += "-1";
            }
            else
            {
                t += "-0";
            }
        }
        //-ch1-lv1-0-1-1

        return t;
        
    }

}

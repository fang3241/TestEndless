using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestionReader qReader;
    public LevelController levelController;
    public ButtonNav buttonNavigation;

    public SoalBab[] kumpulanSoal;

    public int selectedChapter;
    public int selectedLevel;

    public int selectedMaxCounter;
    public int selectedMaxQuestion;
    public int selectedMaxReadingTime;
    public int selectedBabs;

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
    }
    
}

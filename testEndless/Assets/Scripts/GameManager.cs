using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public QuestionReader qReader;
    public LevelController levelController;
    public ButtonNav buttonNavigation;

    public int selectedChapter;
    public int selectedLevel;

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

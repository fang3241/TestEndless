using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public LevelScript levelScript;
    public int[] maxLevelCounters;
    public int[] maxReadingTime;
    public int[] maxLevelQuestions;

    public float[] speedScalings;

    public int[,] babLevels;


    private void Start()
    {

        maxLevelCounters = new int[3] { 25, 40, 50 };//per chapter
        maxReadingTime = new int[3] { 30, 24, 18 };//per chapter
        maxLevelQuestions = new int[5] { 3, 3, 3, 5, 5 };//max pertanyaaan per level di masing" chapter
        speedScalings = new float[3] { 1.1f, 1.25f, 1.5f };

        //berapa bab(urut) yang ada di suatu level
        //misal 1, berarti ada bab 1
        //misal 3, berarti ada bab 1 - 3(gk bisa cuman 1 dan 3)
        babLevels = new int[3, 5]
        {
            { 1, 1, 2, 2, 2,},
            { 3, 3, 4, 4, 4,},
            { 5, 5, 5, 5, 5,}
        };

    }

    public void LevelSetUp(int l)
    {
        GameManager.instance.selectedLevel = l + 1;
        GameManager.instance.selectedMaxCounter = maxLevelCounters[GameManager.instance.selectedChapter - 1];
        GameManager.instance.selectedMaxQuestion = maxLevelQuestions[l];
        GameManager.instance.selectedMaxReadingTime = maxReadingTime[GameManager.instance.selectedChapter - 1];
        GameManager.instance.selectedBab = babLevels[GameManager.instance.selectedChapter - 1, l];
        GameManager.instance.selectedSpeedScaling = speedScalings[GameManager.instance.selectedChapter - 1];

        GameManager.instance.LoadProgress();

        levelScript.showPreview();
    }

    public void StartGame()
    {
        bool read = false;
        Debug.Log(GameManager.instance.selectedLevel);

        int sLevel = ((GameManager.instance.selectedChapter-1) * 5) + GameManager.instance.selectedLevel;

        foreach(CharacterDialogues a in GameManager.instance.dialogues)
        {
            Debug.Log(a.level);
            if(sLevel == a.level)
            {
                read = true;
                GameManager.instance.selectedDialogue = a;
            }
        }

        if (read)
        {
            GameManager.instance.buttonNavigation.toDialogue();
        }
        else
        {
            if (GameManager.instance.selectedChapter == 1)
            {
                GameManager.instance.buttonNavigation.toLevelLand();
            }
            else if (GameManager.instance.selectedChapter == 2)
            {
                GameManager.instance.buttonNavigation.toLevelWater();
            }
            else if (GameManager.instance.selectedChapter == 3)
            {
                GameManager.instance.buttonNavigation.toLevelAir();
            }
        }
        

        


    }
}

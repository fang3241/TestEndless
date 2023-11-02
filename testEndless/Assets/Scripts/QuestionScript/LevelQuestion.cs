using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]//buat manual level
public class LevelQuestion
{
    public enum charOptions
    {
        A = 'A',
        B = 'B',
        C = 'C',
        D = 'D',
    }

    public Sprite questionImage;
    public bool hasImage;
    public string question;
    public string[] options;
    public charOptions answer;

   
    public bool AnswerChecker(int opt)
    {
        if(opt == (int)answer)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}

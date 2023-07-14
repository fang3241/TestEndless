using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class LevelQuestion
{
    public enum charOptions
    {
        A,
        B,
        C,
        D,
    }

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

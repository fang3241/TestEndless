using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionClass
{
    public string question;
    public string[] options;
    private string rawOption;
    public char answer;


    public QuestionClass(string q, string ro, char ans)
    {
        question = q;
        rawOption = ro;
        answer = ans;

        options = optionSplitter();
    }

    public string[] optionSplitter()
    {
        string[] temp = new string[4];

        int t = 0;
        foreach (string a in rawOption.Split('\n'))
        {
            if (a != "")
            {
                temp[t] = a;
                t++;
            }
        }

        return temp;
    }


    public string Question { get => question; set => question = value; }
    public string[] Options { get => options; set => options = value; }
    public char Answer { get => answer; set => answer = value; }
    public string RawOption { set => rawOption = value; }
}

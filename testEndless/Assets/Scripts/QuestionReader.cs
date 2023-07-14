using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuestionReader : MonoBehaviour
{
    public TextAsset soalText;
    public string filename = "SoalMudah";

    public List<string> splitSoal;
    public QuestionClass[] questions;

    private string isiFile;

    private void Awake()
    {
        //for(int i = 0; i < filename.Length; i++)
        //{
        //    if(Resources.Load(filename[i]) != null)
        //    {
        //        soalText[i] = Resources.Load(filename[i]) as TextAsset ;
        //    }
        //}

        if (Resources.Load(filename) != null)
        {
            soalText = Resources.Load(filename) as TextAsset;
            ParseText();
        }
    }

    public void ParseText()
    {
        isiFile = soalText.ToString();
        splitSoal = new List<string>(isiFile.Split('-'));//split per soal


        foreach (string a in splitSoal.ToArray())//ngebersihin array split persoal
            if (a == "")
                splitSoal.Remove(a);

        questions = new QuestionClass[splitSoal.Count];

        for (int i = 0; i < splitSoal.Count; i++)
        {
            string[] temp = splitSoal[i].Split('=');
            //Debug.Log(i + " " + temp.Length);

            QuestionClass qt = new QuestionClass(temp[0], temp[1], temp[2][0]);

            questions[i] = qt;

            temp = null;

        }

    }

}

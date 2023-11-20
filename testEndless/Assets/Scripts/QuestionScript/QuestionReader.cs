using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;

public class QuestionReader : MonoBehaviour
{
    public static event Action<string> PilganErrorListener;
    public List<string> splitSoal;
    public QuestionClass[] questions;
    public Sprite[] qSprites;
    
    public int totalSoal;

    private string isiFile;

    public string[] temp;
    public void ParseText(string folderPath, string mainFolderName)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        totalSoal = 0;
        string dataInfo = "";

        GameManager.instance.customTitle = mainFolderName;

        //baca file txt nya buat tau ada berapa isinya
        StreamReader reader = new StreamReader(folderPath + "/" + "IsiSoal.txt");
        if (reader != null)
        {
            while (!reader.EndOfStream)
            {
                dataInfo = dataInfo + reader.ReadLine();
            }
        }
        
        isiFile = dataInfo;
        splitSoal = new List<string>(isiFile.Split('$'));//split per soal
        
        Debug.Log("SPLITT " + splitSoal.Count);
        foreach (string a in splitSoal.ToArray())//ngebersihin array split persoal
            if (a == "")
                splitSoal.Remove(a);

        if (splitSoal.Count == 1 && splitSoal[0] == isiFile)
        {
            PilganErrorListener += GameObject.FindObjectOfType<ErrorCheckingCustom>().OpenError;
            PilganErrorListener?.Invoke("Qr");
            return;
        }

        totalSoal = splitSoal.ToArray().Length;
        questions = new QuestionClass[totalSoal];//assign question sesuai banyak soal
        for (int i = 0; i < splitSoal.Count; i++)
        {
            temp = splitSoal[i].Split('|');
            Debug.Log(temp.Length);
            QuestionClass qt = new QuestionClass(temp[0], temp[1], temp[2][0]);
            questions[i] = qt;
            temp = null;
        }

        qSprites = new Sprite[totalSoal];
        string filename = folderPath.Remove(0, streamingAssetsPath.Length);
        foreach (string a in Directory.GetFiles(folderPath))
        {
            string name = "";
            int order;
            if (!a.Contains(".meta") && !a.Contains(".txt"))
            {
                if (a.Contains(".jpg"))
                {
                    name = a.Remove(0, streamingAssetsPath.Length + filename.Length + 1).Replace(".jpg", "");
                }
                else if (a.Contains(".png"))
                {
                    name = a.Remove(0, streamingAssetsPath.Length + filename.Length + 1).Replace(".png", "");
                }
                order = int.Parse(name.Replace("Soal", ""));
                Debug.Log(order);
                ImageLoader(a, name, order);
            }
        }

        for(int i = 0; i < totalSoal; i++)
        {
            questions[i].questionSprite = qSprites[i];
        }

        GameManager.instance.customSoal = questions;
        GameManager.instance.selectedMaxQuestion = totalSoal;
        GameManager.instance.buttonNavigation.TQUI();

    }

    private void QuestionReader_PilganErrorListener(string obj)
    {
        throw new NotImplementedException();
    }

    public void ImageLoader(string path, string filename, int imageOrder)//spesific image loader
    {
        //convert foto ke byte
        byte[] pngBytes = File.ReadAllBytes(path);

        //bikin texture2d
        Texture2D tex = new Texture2D(100, 100);
        tex.LoadImage(pngBytes);

        //bikin sprite dari texture2d
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        fromTex.name = filename;
        qSprites[imageOrder - 1] = fromTex;

    }

}

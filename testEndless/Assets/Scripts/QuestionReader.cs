using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;


public class QuestionReader : MonoBehaviour
{
    public List<string> splitSoal;
    public QuestionClass[] questions;
    public Sprite[] qSprites;


    public string folderPath;

    public int totalSoal;

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

        //if (Resources.Load(filename) != null)
        //{
        //    soalText = Resources.Load(filename) as TextAsset;
        //    ParseText();
        //}
    }

    public void ParseText(string path)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        folderPath = path;
        totalSoal = 0;
        string dataInfo = "";

        GameManager.instance.customTitle = folderPath.Remove(0, streamingAssetsPath.Length + 1);

        //baca file txt nya buat tau ada berapa isinya
        StreamReader reader = new StreamReader(path + "/" + "IsiSoal.txt");
        if (reader != null)
        {
            while (!reader.EndOfStream)
            {
                dataInfo = dataInfo + reader.ReadLine();
            }
        }
        
        isiFile = dataInfo;
        splitSoal = new List<string>(isiFile.Split('-'));//split per soal
        foreach (string a in splitSoal.ToArray())//ngebersihin array split persoal
            if (a == "")
                splitSoal.Remove(a);

        totalSoal = splitSoal.ToArray().Length;
        questions = new QuestionClass[totalSoal];//assign question sesuai banyak soal
        for (int i = 0; i < splitSoal.Count; i++)
        {
            string[] temp = splitSoal[i].Split('=');
            QuestionClass qt = new QuestionClass(temp[0], temp[1], temp[2][0]);
            questions[i] = qt;
            temp = null;
        }

        qSprites = new Sprite[totalSoal];
        string filename = path.Remove(0, streamingAssetsPath.Length);
        foreach (string a in Directory.GetFiles(path))
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
        GameManager.instance.selectedMaxCounter = 15;//hard code dulu
        GameManager.instance.buttonNavigation.TQUI();

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

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System;
using System.Linq;

public class DragImageReader : MonoBehaviour
{
    public static event Action<string> DragErrorListener;
    public List<Sprite> imageLists;

    public int totalSoal;
    public List<string> answerLists;

    List<string> imageName;

    public void ParseText(string folderPath, string mainFolderTitle)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        
        totalSoal = 0;
        string dataInfo = "";

        GameManager.instance.customTitle = mainFolderTitle;

        //read file txt
        StreamReader reader = new StreamReader(folderPath + "/" + "Jawaban.txt");
        if (reader != null)
        {
            while (!reader.EndOfStream)
            {
                dataInfo = dataInfo + reader.ReadLine();
            }
        }


        //split per soal
        answerLists = new List<string>(dataInfo.Split('-'));
        foreach (string a in answerLists.ToArray())//ngebersihin array split persoal
            if (a == "")
                answerLists.Remove(a);

        //ambil banyak soal
        totalSoal = answerLists.ToArray().Length;

        imageLists = new List<Sprite>();
        imageName = new List<string>();
        
        string filename = folderPath.Remove(0, streamingAssetsPath.Length);

        string[] imagePath = Directory.GetFiles(folderPath);
        foreach (string a in imagePath)
        {
            string name = "";
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
                imageName.Add(name);
            }
        }

        if (!(answerLists.Count == imageLists.Count && answerLists.All(imageName.Contains)))
        {
            DragErrorListener += GameObject.FindObjectOfType<ErrorCheckingCustom>().OpenError;
            DragErrorListener?.Invoke("Dr");
            return;
        }
        
        for (int i = 0; i < imageName.Count; i++)
        {
            ImageLoader(imagePath[i], imageName[i]);
        }
        
        GameManager.instance.imageLists = imageLists.ToArray();
        GameManager.instance.answerLists = answerLists.ToArray();
        GameManager.instance.selectedMaxQuestion = totalSoal;
        GameManager.instance.buttonNavigation.TDragImage();


    }

    public void ImageLoader(string path, string filename)//spesific image loader
    {
        //convert foto ke byte
        byte[] pngBytes = File.ReadAllBytes(path);

        //bikin texture2d
        Texture2D tex = new Texture2D(100, 100);
        tex.LoadImage(pngBytes);

        //bikin sprite dari texture2d
        Sprite fromTex = Sprite.Create(tex, new Rect(0.0f, 0.0f, tex.width, tex.height), new Vector2(0.5f, 0.5f), 100.0f);
        fromTex.name = filename;
        imageLists.Add(fromTex);

    }

}

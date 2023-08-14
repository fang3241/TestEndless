using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DragImageReader : MonoBehaviour
{
    public List<Sprite> imageLists;

    public int totalSoal;
    public List<string> answerLists;


    public void ParseText(string folderPath, string mainFolderTitle)
    {
        string streamingAssetsPath = Application.streamingAssetsPath;
        
        totalSoal = 0;
        string dataInfo = "";

        GameManager.instance.customTitle = mainFolderTitle;

        //baca file txt nya buat tau ada berapa isinya
        StreamReader reader = new StreamReader(folderPath + "/" + "Jawaban.txt");
        if (reader != null)
        {
            while (!reader.EndOfStream)
            {
                dataInfo = dataInfo + reader.ReadLine();
            }
        }
        

        answerLists = new List<string>(dataInfo.Split('-'));//split per soal
        foreach (string a in answerLists.ToArray())//ngebersihin array split persoal
            if (a == "")
                answerLists.Remove(a);

        
        //foreach(string a in answerLists)
        //{
        //    Debug.Log(a);
        //}

        totalSoal = answerLists.ToArray().Length;

        imageLists = new List<Sprite>();
        
        string filename = folderPath.Remove(0, streamingAssetsPath.Length);
        foreach (string a in Directory.GetFiles(folderPath))
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
                
                ImageLoader(a, name);
            }
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

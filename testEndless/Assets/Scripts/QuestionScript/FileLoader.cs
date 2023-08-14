using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class FileLoader : MonoBehaviour
{
    public QuestionReader QuestionReader;
    public DragImageReader imageReader;
    public GameObject btnPrefab;
    public GameObject parentItem;

    public string folderPath;
    public string[] folderList;

    public int customType;
    

    // Start is called before the first frame update
    void Start()
    {
        customType = GameManager.instance.customType;
        //baca folder yg mau dituju
        folderPath = GetLevelPath();
        folderList = Directory.GetDirectories(folderPath);

        foreach (string a in folderList)
        {
            if (a != "")
            {
                GenerateButton(a);
            }
        }

    }

    public string GetLevelPath()
    {
        string path = Application.streamingAssetsPath;
        return customType == 0 ? path + "/Cocok Gambar" : path + "/Pilihan Ganda";
    }

    public void GenerateButton(string buttonName)
    {
        string name = buttonName.Remove(0, folderPath.Length + 1);

        GameObject btn = Instantiate(btnPrefab, parentItem.transform.position, Quaternion.identity, parentItem.transform);
        btn.GetComponentInChildren<TextMeshProUGUI>().text = name;

        if (customType == 0)
        {
            btn.GetComponent<Button>().onClick.AddListener(() => imageReader.ParseText(buttonName, name));
        }
        else
        {
            btn.GetComponent<Button>().onClick.AddListener(() => QuestionReader.ParseText(buttonName, name));

        }
    }
}

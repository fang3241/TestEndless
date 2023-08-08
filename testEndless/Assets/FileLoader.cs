using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.IO;

public class FileLoader : MonoBehaviour
{
    public QuestionReader reader;
    public GameObject btnPrefab;
    public GameObject parentItem;

    public string folderPath;
    public string[] folderList;
    

    // Start is called before the first frame update
    void Start()
    {
        //baca folder yg mau dituju
        folderPath = Application.streamingAssetsPath;
        folderList = Directory.GetDirectories(folderPath);

        foreach (string a in folderList)
        {
            if (a != "")
            {
                string name = a.Remove(0, folderPath.Length + 1);
                GameObject btn = Instantiate(btnPrefab, parentItem.transform.position, Quaternion.identity, parentItem.transform);

                btn.GetComponent<Button>().onClick.AddListener(() => reader.ParseText(a));
                btn.GetComponentInChildren<TextMeshProUGUI>().text = name;

            }
        }
    }

}

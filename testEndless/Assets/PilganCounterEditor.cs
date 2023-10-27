using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PilganCounterEditor : MonoBehaviour
{
    public TMP_InputField input;

    public QuestionReader QuestionReader;

    private string path, folderName;

    private void Start()
    {
        path = "";
        folderName = "";
    }

    public void StartGame()
    {
        if(input.text == "")
        {
            Debug.Log("Empty");
            GameManager.instance.selectedMaxCounter = 15;
        }
        else
        {
            Debug.Log(input.text);
            GameManager.instance.selectedMaxCounter = int.Parse(input.text);
        }

        QuestionReader.ParseText(path, folderName);
    }

    public void OpenMenu(string folderPath, string mainFolderName)
    {
        path = folderPath;
        folderName = mainFolderName;
        transform.GetChild(0).gameObject.SetActive(true);

        //Debug.Log(path + " " + folderName);
    }

    public void CloseMenu()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

}

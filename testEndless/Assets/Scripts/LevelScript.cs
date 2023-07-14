using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelScript : MonoBehaviour
{
    public GameObject[] lvlButton;
    public GameObject levelPreviewPanel;

    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < 5; i++)
        {
            Debug.Log(lvlButton[i].name);
            int t = GameManager.instance.selectedChapter * 5 + i + 1;
            lvlButton[i].name = "Level" + t;    
            lvlButton[i].GetComponentInChildren<Text>().text = t.ToString();
            Debug.Log("Loaded Level : " + (GameManager.instance.selectedChapter * 5 + i));
        }
    }

    public void showPreview()
    {
        levelPreviewPanel.SetActive(true);
    }

    public void hidePreview()
    {
        levelPreviewPanel.SetActive(false);
    }


}

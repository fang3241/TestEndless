using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelScript : MonoBehaviour
{
    public GameObject[] lvlButton;
    public GameObject levelPreviewPanel, helpPanel;
    public TextMeshProUGUI chapterText;

    public Image bgImage;
    public Sprite[] selectedImages;
    public Sprite fullStar;
    public Sprite emptyStar;


    public TextMeshProUGUI previewLevelText;
    public Image[] objectiveStars;
    public TextMeshProUGUI[] objectiveText;


    // Start is called before the first frame update
    void Start()
    {
        int maxLoadedLevel = GameManager.instance.currentUnlockedLevel;
        int ch = GameManager.instance.selectedChapter + 1;
        
        /*
         *mis : 
         * maxLoadedLevel = 7 => 5 level ch 1 + 2 level ch 2
         * ch = 1 => ch*5
         * 
         * unlocked level = 7 mod 5
         * = 2 => load 2 level di ch 2, sisanya locked
         *
        // */
        //Debug.Log("MAX " + maxLoadedLevel);
        //Debug.Log("CH " + ch);
        
        if(maxLoadedLevel < (5 * ch))
        {
            maxLoadedLevel %= 5;
        }


        bgImage.sprite = selectedImages[ch - 1];

        chapterText.text = "Bagian " + ch;
        for(int i = 0; i < 5; i++)
        {
            int t = i + 1;
            lvlButton[i].name = "Level" + t;
            if (maxLoadedLevel <= i)
            {
                Debug.Log(i + " Locked");
                lvlButton[i].GetComponent<Button>().interactable = false;
            }
            lvlButton[i].GetComponentInChildren<TextMeshProUGUI>().text = ch + "-" + t.ToString();
            //Debug.Log("Loaded Level : " + (GameManager.instance.selectedChapter * 5 + i));
        }


        //backup
        //for (int i = 0; i < 5; i++)
        //{
        //    //Debug.Log(lvlButton[i].name);
        //    //int t = GameManager.instance.selectedChapter * 5 + i + 1;
        //    int t = i + 1;
        //    lvlButton[i].name = "Level" + t;
        //    lvlButton[i].GetComponentInChildren<TextMeshProUGUI>().text = (ch + 1) + "-" + t.ToString();
        //    //Debug.Log("Loaded Level : " + (GameManager.instance.selectedChapter * 5 + i));
        //}
    }

    public void setObjectivePreview()
    {
        int maxQuestion = GameManager.instance.selectedMaxQuestion;
        int minAnswer = (maxQuestion / 2) + 1;
        int maxHit = 2;

        previewLevelText.text = "Level " + (GameManager.instance.selectedChapter + 1)+ "-" + (GameManager.instance.selectedLevel + 1);

        objectiveText[0].text = $"Isi bar pengetahuan sebanyak {maxQuestion}x";
        objectiveText[1].text = $"Jawab {minAnswer} atau lebih soal dengan benar";
        objectiveText[2].text = $"Tidak menabrak lebih dari {maxHit} rintangan";
        
        for (int i = 0; i < 3; i++)
        {
            if (GameManager.instance.selectedObjLevelStatus[i])
            {
                objectiveStars[i].sprite = fullStar;
            }
            else
            {
                objectiveStars[i].sprite = emptyStar;
            }
        }
    }

    public void showPreview()
    {
        setObjectivePreview();
        levelPreviewPanel.SetActive(true);
    }

    public void hidePreview()
    {
        levelPreviewPanel.SetActive(false);
    }

    public void showHelp()
    {
        helpPanel.SetActive(!helpPanel.activeSelf);
    }

}

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
        int ch = GameManager.instance.selectedChapter;

        Debug.Log("LS " + maxLoadedLevel + " " + ch);

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
        for(int i = 1; i <= 5; i++)
        {
            lvlButton[i-1].name = "Level" + i;
            
            if (i > maxLoadedLevel)
            {
                Debug.Log(i + " Locked");
                lvlButton[i-1].GetComponent<Button>().interactable = false;
            }
            else
            {
                bool[] data = GameManager.instance.LoadSelectedProgress(ch, i);

                Debug.Log("ch" + ch + "lv" + i + " : " + data[0] + data[1] + data[2]);
                
                int num = 0;
                foreach (Image s in lvlButton[i - 1].transform.GetChild(1).GetComponentsInChildren<Image>())
                {
                    if (data[num])
                    {
                        s.sprite = fullStar;
                    }
                    else
                    {
                        s.sprite = emptyStar;
                    }
                    num++;
                }
            }
            lvlButton[i-1].GetComponentInChildren<TextMeshProUGUI>().text = ch + "-" + i.ToString();
            //Debug.Log("Loaded Level : " + (GameManager.instance.selectedChapter * 5 + i));
        }

    }

    public void setObjectivePreview()
    {
        int maxQuestion = GameManager.instance.selectedMaxQuestion;
        int minAnswer = (maxQuestion / 2) + 1;
        int maxHit = 2;

        previewLevelText.text = "Level " + (GameManager.instance.selectedChapter)+ "-" + (GameManager.instance.selectedLevel);

        objectiveText[0].text = $"Isi Garis pengetahuan sebanyak {maxQuestion}x";
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

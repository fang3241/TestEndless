using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class DragImageController : MonoBehaviour
{
    public GameObject imagePrefab;
    public GameObject answerPrefab;

    public GameObject imageParent;
    public GameObject answerParent;

    public GameObject tempParent;

    public GameObject winPanel, pausePanel, notFinishedPanel;
    public TextMeshProUGUI tTotal, tCorrect, tNilai;
    
    public List<ImgScript> imageList;

    public TextMeshProUGUI gameTitle;

    public Scrollbar imgScroll, ansScroll;

    private List<int> temp;

    public int total;

    private int totalAnswered, correct;

    private void Awake()
    {
        GameManager.instance.DIcontroller = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        totalAnswered = 0;
        correct = 0;
           
        gameTitle.text = GameManager.instance.customTitle;

        RandomInstatiate();
    }

    private void Update()
    {
        //sementara
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ShowPause();
        }

    }

    public void ShowPause()
    {
        pausePanel.SetActive(!pausePanel.activeInHierarchy);
        //if (pausePanel.activeSelf)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }

    public void RandomInstatiate()
    {
        total = GameManager.instance.selectedMaxQuestion;

        temp = new List<int>();
        for (int i = 0; i < total; i++)
        {
            temp.Add(i);
        }

        RandomizeList();
        
        for(int i = 0; i < total; i++)
        {
            GameObject imgObj = Instantiate(imagePrefab, new Vector3(0, 0, 0), Quaternion.identity, imageParent.transform);
            Image img = imgObj.transform.GetChild(0).GetComponent<Image>();
            imgObj.name = "Img" + GameManager.instance.imageLists[temp[i]].name;
            img.sprite = GameManager.instance.imageLists[temp[i]];


            imageList.Add(imgObj.GetComponent<ImgScript>());
        }

        RandomizeList();

        for (int i = 0; i < total; i++)
        {
            GameObject txtObj = Instantiate(answerPrefab, new Vector3(0, 0, 0), Quaternion.identity, answerParent.transform);
            TextMeshProUGUI txt = txtObj.GetComponentInChildren<TextMeshProUGUI>();
            txtObj.name = GameManager.instance.answerLists[temp[i]];
            txt.text = GameManager.instance.answerLists[temp[i]];

        }


        imgScroll.value = 0;
        ansScroll.value = 0;
    }

    public void RandomizeList()
    {
        for (int i = 0; i < total; i++)
        {
            int r = Random.Range(0, total);
            int t = temp[i];
            temp[i] = temp[r];
            temp[r] = t;
        }
        
    }

    public void NotFinished()
    {
        notFinishedPanel.SetActive(!notFinishedPanel.activeSelf);
    }

    public void CheckStatus()
    {
        totalAnswered = 0;
        correct = 0;

        foreach(ImgScript img in imageList)
        {
            if(img.txtGrabber != null)
            {
                totalAnswered++;
            }
        }

        if(totalAnswered == total)
        {
            foreach(ImgScript img in imageList)
            {
                if(img.name == "Img" + img.txtGrabber.name)
                {
                    correct++;
                }
            }

            tTotal.text = "Total Soal : " + total;
            tCorrect.text = "Benar : " + correct;
            tNilai.text = "Nilai : " + Mathf.Floor(((float)correct / total) * 100);

            winPanel.SetActive(true);
        }
        else
        {
            NotFinished();
        }



        Debug.Log("total answer : " + totalAnswered);
        Debug.Log("Correct : " + correct);
    }

}

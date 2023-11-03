using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TutorialPageController : MonoBehaviour
{
    public Image imgContainer;
    public Sprite[] tutorSprites;
    public TextMeshProUGUI pageText;
    //public Button next, prev, close;

    public int i;

    // Start is called before the first frame update
    void Start()
    {
        i = 0;
        imgContainer.sprite = tutorSprites[i];
        pageText.text = "(" + (i + 1) + " / " + tutorSprites.Length + ")";
    }
    
    public void TutorNext()
    {
        LimitChecker('+');
        imgContainer.sprite = tutorSprites[i];
    }

    public void TutorPrev()
    {
        LimitChecker('-');
        imgContainer.sprite = tutorSprites[i];
    }

    void LimitChecker(char op)
    {
        Debug.Log(tutorSprites.Length);
        if(op == '-' && i > 0)
        {
            i--;
        }else if(op == '+' && i+1 < tutorSprites.Length)
        {
            i++;
        }
        pageText.text = "(" + (i + 1) + " / " + tutorSprites.Length + ")";
    }

    public void TutorClose()
    {
        this.gameObject.SetActive(false);
    }

}

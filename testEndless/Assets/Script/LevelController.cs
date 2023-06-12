using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    /* - mengatur level loop level
     * - menjadi penghubung semua script di level (reference)
     * 
     * 
     */ 


    public GameObject hpParentGO;
    public GameObject ObsHolder;
    public List<Image> hpIcoImg;

    public float speedScaling;
    public float counter = 0;
    public float maxCounter;


    public int hp;
    public char selectedAnswer;

    public char correctAnswer;

    public PlayerController player;
    public GameManager gameManager;
    public QuestionScript QuestionScript;
    public AnswerSpawner answerSpawner;
    public SliderScript slider;

    public bool isAnswerSpawned;
    public bool isQuestionSpawned;

    //private bool isQuestionAnswered;//apakah player udah memilih opsi
    //public bool isQuestionTriggered;
    //private bool isDelayTriggered;
    //private bool isDelayFinished;
    //private bool isQuestionStarted;

    private void Awake()
    {
        QuestionScript = GameObject.FindObjectOfType<QuestionScript>();
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        gameManager = GameObject.FindGameObjectWithTag("GameController").GetComponent<GameManager>();
        slider = GameObject.FindGameObjectWithTag("Slider").GetComponent<SliderScript>();
        hp = 3;

        speedScaling = 1;
        maxCounter += 0.1f;

        isAnswerSpawned = false;
        isQuestionSpawned = false;
    }

    void Start()
    {
        foreach(Image image in hpParentGO.GetComponentsInChildren<Image>())
        {
            if(image.name != hpParentGO.name)
                hpIcoImg.Add(image);
        }
        //answerSpawner.gameObject.SetActive(false);

    }

    private void Update()
    {

        //EnableQuestion();

    }
    
    public void AnswerChecker()
    {
        if(selectedAnswer != correctAnswer)
        {
            player.Hit();
        }

        QuestionMode();
        AnswerMode();
        slider.Switch();
        QuestionScript.ClosePanel();
    }

    public void QuestionMode()
    {
        isQuestionSpawned = !isQuestionSpawned;

        if (isQuestionSpawned)
        {
            foreach (Transform a in ObsHolder.transform)
            {
                Destroy(a.gameObject);
            }
        }
        
        Debug.Log("isSpawned : " + isQuestionSpawned);
    }

    public void AnswerMode()
    {
        isAnswerSpawned = !isAnswerSpawned;
        Debug.Log("isSpawned : " + isAnswerSpawned);
    }

    public void Lose()
    {
        Debug.Log("Lose");
        speedScaling = 0;
        player.gameObject.SetActive(false);
        //tampilkan panel lose
    }

   

    //IEnumerator Delay(float delay)
    //{
    //    isDelayTriggered = true;
    //    yield return new WaitForSeconds(delay);
    //    //QuestionScript.OpenPanel();
    //    isDelayFinished = true;
    //    isDelayTriggered = false;
    //    //Debug.Log("Delay Complete");

    //}
    
    public void ToggleQuestion()
    {

        //if(counter <= maxCounter + 0.99f && !isQuestionStarted)
        //{
        //    /*UBAH
        //     * speedScaling => bikin baru, pointScaling
        //     * 
        //     * point rate = 0.5p/s
        //     * max point : 10 | 15 | 20
        //     */

        //    //counter += speedScaling * Time.deltaTime;
        //}
        //else
        //{//benerin bagian ini nya, 
        //    if (isQuestionTriggered)
        //    {
        //        if (isDelayFinished)
        //        {
        //            //if()bar blom kosong
        //            //bar--;
        //            //else, spawn jawaban
        //        }
        //        else
        //        {
        //            if (!isDelayTriggered)
        //                StartCoroutine(Delay(1));
        //        }


        //    }
        //    else
        //    {
        //        isQuestionTriggered = true;
        //        isQuestionStarted = true;
        //        QuestionScript.OpenPanel();
        //        //open panel;
        //    }



        //    //if (isDelayTriggered)
        //    //{
        //    //    //spawn soal

        //    //    //baru countdown counter
        //    //    if (counter >= 0)
        //    //    {
        //    //        AnswerSpawner.gameObject.SetActive(true);
        //    //        counter -= speedScaling * Time.deltaTime;   //waktu buat baca soal
        //    //    }
        //    //    else
        //    //    {
        //    //        QuestionScript.ClosePanel();
        //    //    }



        //    //}//ubah isdelayed jadi isdelaytriggered, nanti juga inget lagi, kuncinya jangan dibalikin 
        //    //else
        //    //{
        //    //    StartCoroutine(Delay(3));
        //    //}
        //}
    }

}

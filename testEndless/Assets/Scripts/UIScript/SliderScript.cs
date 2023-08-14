using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public enum CounterState
    {
        Filling, 
        Emptying,
        Waiting,
    }

    public LevelController levelController;
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public float pointRate;

    public int fillBarCounter;
    public int barValue;

    public bool isTesting = false;

    public CounterState state;
    public CounterState lastState;


    public bool fillOrEmpty;//1 = fill 0 = empty(decrease to 0)

    
    //private void Awake()
    //{
    //    StartCoroutine(WaitForLC());
    //}

    //IEnumerator WaitForLC()
    //{
    //    if(LevelController == null)
    //    {
    //        Debug.Log("WAITING");
    //        LevelController = GameManager.instance.levelController;
    //        yield return new WaitUntil(() => LevelController != null);
            
    //    }
    //    else
    //    {
    //        StartCoroutine(WaitForLC());
    //    }
    //}

    private void Start()
    {
        //levelController = GetComponent<QuestionScript>().levelController;
        //Debug.Log(levelController == null);
        state = CounterState.Filling;
        lastState = state;
        fillOrEmpty = true;
        fillBarCounter = 0;
        
        //Debug.Log("max " + slider.maxValue);

        StartCoroutine(CheckState());
    }

    public void addPoint(int add)//buat button testing
    {
        Debug.Log("ADD " + add);
        Debug.Log("PR " + this.pointRate);
        slider.value += (add * pointRate);
        Debug.Log("Added " + (add * pointRate));
        
    }
    
    public void Counting()
    {
        if (!levelController.isLevelEnd)
        {
            if (state == CounterState.Filling)
            {
                pointRate = 1;
            }
            else if (state == CounterState.Emptying)
            {
                pointRate = -1;
            }
            slider.value += Time.deltaTime * pointRate;
            //kalo mau pake persen
            //barValue = (int)((slider.value / slider.maxValue) * 100);

            //kalo mau kyk biasa
            barValue = (int)(slider.value);
            sliderText.text = barValue.ToString();
            state = CounterState.Waiting;

            StartCoroutine(CheckState());
        }
        
    }

    IEnumerator CheckState()
    {
        if(levelController == null)
        {
            levelController = GameManager.instance.levelController;
        }

        if(state == CounterState.Waiting)
        {
            if (slider.value == slider.maxValue)
            {
                //summon question
                state = CounterState.Emptying;
                Debug.Log("emptying");
                levelController.SpawnQuestion();
                levelController.objectiveController.objectives[0].addProgress();
                slider.maxValue = levelController.maxReadingTime + 0.1f;
                slider.value = slider.maxValue;
                
            }
            else if (slider.value == slider.minValue)
            {
                state = CounterState.Waiting;
                if (levelController.isAnswerSpawned)
                {
                    Debug.Log("waiting for answer");
                    if (!levelController.isAnswered)
                    {
                        state = CounterState.Waiting;
                    }
                    else
                    {
                        levelController.StateReset();
                        state = CounterState.Filling;
                        slider.maxValue = levelController.maxSliderCounter;
                        slider.value = 0;
                    }
                }
                else
                {
                    levelController.SpawnAnswer();
                    yield return new WaitForSeconds(1);
                }
                
            }

            lastState = state;
            
        }

        
        yield return null;
        Counting();
    }
    

    //private void SliderUpdate()
    //{
    //    if (!LevelController.isLevelEnd && !isTesting)
    //    {
    //        slider.value += Time.deltaTime * pointRate;

    //        if(slider.value == slider.maxValue && fillOrEmpty)
    //        {
    //            pointRate *= -1;
    //        }else if(slider.value == slider.minValue && !fillOrEmpty)
    //        {
    //            pointRate *= -1;
    //        }
    //        //Debug.Log(slider.value);
    //        sliderText.text = ((int)slider.value).ToString();
    //        //IncDecToggle();
    //    }
    //}

    //private void SliderUpdate()
    //{
    //    if (!LevelController.isLevelEnd && !isTesting)
    //    {
    //        slider.value += Time.deltaTime * pointRate;
    //        //Debug.Log(slider.value);
    //        sliderText.text = ((int)slider.value).ToString();
    //        IncDecToggle();
    //    }
    //}

    //public void IncDecToggle()
    //{
    //    if(slider.value == slider.maxValue)
    //    {
    //        //if max : spawn q
    //        //if min : spawn ans
    //        //fillBarCounter++;

    //        LevelController.objectiveController.objectives[0].addProgress();

    //        //LevelController.QuestionScript.OpenPanel();
    //        LevelController.StartQuestion();
    //        Switch();
    //    }
    //    else if(slider.value == slider.minValue && !LevelController.isAnswerSpawned)
    //    {
    //        Debug.Log("triggered Answer");
    //        //LevelController.answerSpawner.SpawnAns();

    //        LevelController.StartAnswer();
    //    }
    //}

    //public void DecreaseMode()
    //{
    //    slider.maxValue = 10.1f;
    //    slider.value = 10.1f;
    //}

    //public void FillMode()
    //{
    //    slider.maxValue = LevelController.maxSliderCounter + 0.1f;
    //    slider.value = 0.1f;
    //}

    //public void Switch()
    //{
    //    if (fillOrEmpty)
    //    {
    //        DecreaseMode();
    //        fillOrEmpty = false;
    //    }
    //    else
    //    {
    //        FillMode();
    //        fillOrEmpty = true;
    //    }
    //    pointRate *= -1;
    //}

}

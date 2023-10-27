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
    
    public int barValue;

    public bool isTesting = false;

    public CounterState state;

    
    private void Start()
    {
        state = CounterState.Filling;
    }

    public void addPoint(int add)
    {
        Debug.Log("ADD " + add);
        Debug.Log("PR " + this.pointRate);
        slider.value += add;
        Debug.Log("Added " + (add * pointRate));
        
    }
    
    private void Update()
    {
        Counting();
        CheckState();
    }

    public void Counting()
    {
        if (!levelController.isLevelEnd)
        {
            if (state == CounterState.Filling)
            {
                pointRate = 1;
                slider.value += (Time.deltaTime);
            }

            if (state == CounterState.Emptying)
            {
                pointRate = -1;
                slider.value -= (Time.deltaTime);
            }
            

            barValue = (int)(slider.value);
            sliderText.text = barValue.ToString();
        }
        
    }

    public void CheckState()
    {
        if(levelController == null)
        {
            levelController = GameManager.instance.levelController;
        }

        if (state == CounterState.Filling)
        {
            if (slider.value == slider.maxValue && !levelController.isQuestionSpawned)
            {
                Debug.Log("Full");
                levelController.SpawnQuestion();
                slider.maxValue = levelController.maxReadingTime;
                slider.value = slider.maxValue - 0.1f;
                state = CounterState.Emptying;
            }
        }
        else if (state == CounterState.Emptying)
        {
            //Debug.Log("EMPTY");
            if (slider.value == slider.minValue && !levelController.isAnswerSpawned)
            {
                state = CounterState.Waiting;
                levelController.SpawnAnswer();
            }
        }
        else
        {
            if (levelController.isAnswered)
            {
                Debug.Log("Reseted");
                levelController.StateReset();
                slider.maxValue = levelController.maxSliderCounter;
                state = CounterState.Filling;
            }

        }
        
        
    }
    
}

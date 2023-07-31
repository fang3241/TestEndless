using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;
    public float pointRate;

    public int fillBarCounter;

    public bool isTesting = false;

    private LevelController LevelController;

    
    private void Awake()
    {
        LevelController = GameObject.FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        fillBarCounter = 0;
        slider.maxValue = LevelController.maxSliderCounter + 0.1f;
        //Debug.Log("max " + slider.maxValue);
    }

    public void addPoint(int add)
    {
        slider.value += add * pointRate;
        Debug.Log("Added 8 point");
        
    }
    
    void Update()
    {
        SliderUpdate();
    }

    private void SliderUpdate()
    {
        if (!LevelController.isLevelEnd && !isTesting)
        {
            slider.value += Time.deltaTime * pointRate;
            //Debug.Log(slider.value);
            sliderText.text = ((int)slider.value).ToString();
            IncDecToggle();
        }
    }

    public void IncDecToggle()
    {
        if(slider.value == slider.maxValue)
        {
            //if max : spawn q
            //if min : spawn ans
            //fillBarCounter++;

            LevelController.objectiveController.objectives[0].addProgress();

            //LevelController.QuestionScript.OpenPanel();
            LevelController.StartQuestion();
            Switch();
        }
        else if(slider.value == slider.minValue && !LevelController.isAnswerSpawned)
        {
            Debug.Log("triggered Answer");
            //LevelController.answerSpawner.SpawnAns();
            LevelController.StartAnswer();
        }
    }

    public void Switch()
    {
        pointRate *= -1;
    }
    
}

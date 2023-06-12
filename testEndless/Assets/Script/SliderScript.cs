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

    private LevelController LevelController;

    
    private void Awake()
    {
        LevelController = GameObject.FindObjectOfType<LevelController>();
    }

    private void Start()
    {
        slider.maxValue = LevelController.maxCounter;
        //Debug.Log("max " + slider.maxValue);
    }

    public void addPoint()
    {
        slider.value += 8 * (pointRate+pointRate);
        Debug.Log("Added 8 point");
        
    }
    
    void Update()
    {
        SliderUpdate();
    }

    private void SliderUpdate()
    {
        slider.value += Time.deltaTime * pointRate;
        //Debug.Log(slider.value);
        sliderText.text = ((int)slider.value).ToString();
        IncDecToggle();
    }

    public void IncDecToggle()
    {
        if(slider.value == slider.maxValue)
        {
            //if max : spawn q
            //if min : spawn ans
            LevelController.QuestionScript.OpenPanel();
            Switch();
        }
        else if(slider.value == slider.minValue && !LevelController.isAnswerSpawned)
        {
            Debug.Log("triggered Answer");
            LevelController.answerSpawner.SpawnAns();
            LevelController.AnswerMode();
        }
    }

    public void Switch()
    {
        pointRate *= -1;
    }
    
}

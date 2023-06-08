using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SliderScript : MonoBehaviour
{
    public Slider slider;
    public TextMeshProUGUI sliderText;

    private LevelController LevelController;

    private void Awake()
    {
        LevelController = GameObject.FindObjectOfType<LevelController>();
    }



    // Update is called once per frame
    void Update()
    {
        slider.value = (LevelController.counter) / (LevelController.maxCounter + 0.99f);
        sliderText.text = ((int)LevelController.counter).ToString();
    }
}

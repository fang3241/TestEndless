using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSetting : MonoBehaviour
{
    public Slider volSlider;

    // Start is called before the first frame update
    void Start()
    {
        LoadSetting();
        
    }

    private void Update()
    {
        AudioListener.volume = volSlider.value;
    }

    private void SaveSetting()
    {
        PlayerPrefs.SetFloat("audioVolume", volSlider.value);
    }

    private void LoadSetting()
    {
        volSlider.value = PlayerPrefs.GetFloat("audioVolume", 1);
    }


    public void OpenMenu()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    public void CloseMenu()
    {
        SaveSetting();
        transform.GetChild(0).gameObject.SetActive(false);
    }
}

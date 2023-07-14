using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ObjectiveController : MonoBehaviour
{
    public GameObject pausePanel;
    public Text[] objText;
    public Objective[] objectives;


    public enum ObjectiveName
    {
        bar, hp, hit
    }


    public int[] objCounter;
    
    
    private void Start()
    {
        objCounter = new int[3] { 0, 0, 0 };


        objectives = new Objective[3];
        objectives[0] = new Objective_Bar(3);
        objectives[1] = new Objective_Answer(2);
        objectives[2] = new Objective_Hit(2);

        for(int i = 0; i < 3; i++)
        {
            objText[i].text = objectives[i].getNama();
        }

    }

    private void Update()
    {
        //sementara
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            pausePanel.SetActive(!pausePanel.activeInHierarchy);
            
        }

        //tambahin counter sesudah pause
        updateObjectiveCounter();
    }

    public void updateObjectiveCounter()
    {
        for (int i = 0; i < 3; i++)
        {
            objCounter[i] = objectives[i].getProgress();
        }
    }

    //kalo load panel, panggil fungsi obj masing masing

}

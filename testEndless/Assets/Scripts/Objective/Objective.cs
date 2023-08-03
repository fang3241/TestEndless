using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Objective 
{
    protected string namaObjective;
    protected int banyak;
    protected int progress;
    protected bool status;
    protected typeList type;
    protected string progressStatus;

    public enum typeList
    {
        xHP, xBar, xCollect, xAnswer, xHit
    }

    protected LevelController levelController;

    public Objective()
    {
        //levelController = GameManager.instance.levelController;
    }


    public virtual void addProgress()
    {
        progress++;
        progressStatus = "(" + progress + "/" + banyak + ")";
        statusChecker();
    }
    
    public string getNama()
    {
        return namaObjective;
    }

    public string getProgressStatus()
    {
        return progressStatus;
    }

    public int getProgress()
    {
        return progress;
    }

    public virtual bool statusChecker()
    {
        return status;
    }
}

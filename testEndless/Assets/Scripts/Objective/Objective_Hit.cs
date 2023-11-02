using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Hit : Objective
{
    public Objective_Hit(int maxHit)
    {
        this.progress = maxHit;
        this.banyak = maxHit;
        this.progressStatus = "(" + progress + "/" + banyak + ")";
        this.namaObjective = $"Tidak menabrak lebih dari {banyak} rintangan";//nyoba cara baru
        this.status = false;
    }

    public override void addProgress()
    {
        if(progress > 0)
        {
            progress--;
        }
        statusChecker();
    }

    public override bool statusChecker()
    {
        progressStatus = "(" + progress + "/" + banyak + ")";
        if (progress == 0)
        {
            return status = false;
        }
        else
        {
            return status = true;
        }
    }
}

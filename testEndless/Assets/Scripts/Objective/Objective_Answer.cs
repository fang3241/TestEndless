using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Answer : Objective
{
    public Objective_Answer(int kkm)
    {
        this.progress = 0;
        this.banyak = kkm;
        this.progressStatus = "(" + progress + "/" + banyak + ")";
        this.namaObjective = $"Jawab {banyak} atau lebih soal dengan benar";//nyoba cara baru
        this.status = false;
    }

    public override void addProgress()
    {
        if(progress < banyak)
        {
            progress++;
        }
        statusChecker();
    }

    public override bool statusChecker()
    {
        progressStatus = "(" + progress + "/" + banyak + ")";
        if (progress >= banyak)
        {
            return status = true;
        }
        else
        {
            return status = false;
        }
    }
}

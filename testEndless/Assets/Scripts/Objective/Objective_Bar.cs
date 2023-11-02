using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Bar : Objective
{
    private SliderScript slider;

    public Objective_Bar(LevelController l, int b)
    {
        slider = l.slider;
        this.progress = 0;
        this.banyak = b;
        this.progressStatus = "(" + progress + "/" + banyak + ")";
        this.namaObjective = $"Isi Garis pengetahuan sebanyak {banyak}x";//nyoba cara baru
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
        if (progress == banyak)
        {
            return status = true;
        }
        else
        {
            return status = false;
        }
    }
}


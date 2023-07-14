using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Bar : Objective
{
    private SliderScript slider;
    private int barCounter;

    public Objective_Bar(int b)
    {
        slider = levelController.slider;

        this.progress = 0;
        this.banyak = b;
        this.namaObjective = $"Isi bar pengetahuan sebanyak {banyak}x";//nyoba cara baru
        this.status = false;
    }
    
    public override bool statusChecker()
    {
        barCounter = slider.fillBarCounter;
        if(barCounter >= banyak)
        {
            return status = true;
        }
        else
        {
            return status = false;
        }
    }
}


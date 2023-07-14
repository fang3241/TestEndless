using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_Hit : Objective
{
    public Objective_Hit(int maxHit)
    {
        this.progress = 0;
        this.banyak = maxHit;
        this.namaObjective = $"Tidak menabrak lebih dari {banyak} rintangan";//nyoba cara baru
        this.status = false;
    }

    public override bool statusChecker()
    {
        if (progress > banyak)
        {
            return status = false;
        }
        else
        {
            return status = true;
        }
    }
}

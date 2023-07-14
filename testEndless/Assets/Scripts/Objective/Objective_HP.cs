using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Objective_HP : Objective
{   
    public Objective_HP()
    {
        this.namaObjective = "Jangan mati :D";
        this.type = typeList.xHP;
        this.status = false;
        
    }

    public override bool statusChecker()
    {
        bool temp;
        if(levelController.hp == 0)
        {
            temp = false;
        }
        else
        {
            temp = true;
        }

        return status = temp;
    }
}

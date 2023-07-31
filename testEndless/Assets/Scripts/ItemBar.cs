using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemBar : Collectable
{
    public int addBar;
    
    protected override void HitEffect()
    {
        //tambahin bar sebanyak x
        levelController.slider.addPoint(addBar);
    }
}

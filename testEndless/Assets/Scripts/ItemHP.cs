using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemHP : Collectable
{
    public int healHP;
    
    protected override void HitEffect()
    {
        playerController.Heal(healHP);
    }
}

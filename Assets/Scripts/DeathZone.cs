﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

public class DeathZone : CollectableItem
{
    
    public override void ActivateEffect()
    {
        GM.instance.player.Die();
        StartCoroutine(restart());
    }

    IEnumerator restart(){
        yield return new WaitForSeconds(1.8f);
        GM.instance.RestartLevel();
    }
}

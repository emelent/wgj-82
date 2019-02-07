using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeadlyGap : CollectableItem
{
    public override void ActivateEffect()
    {
        if(!GM.instance.player.isAttracted){    
            GM.instance.player.Die();
            StartCoroutine(restart());
        }
        
    }

    
    IEnumerator restart(){
        yield return new WaitForSeconds(1.8f);
        GM.instance.RestartLevel();
    }
}

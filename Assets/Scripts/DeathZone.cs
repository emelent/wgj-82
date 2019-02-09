using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

public class DeathZone : CollectableItem
{
    
    public bool ignoreIfAttracted = true;
    public override void ActivateEffect()
    {
        if(ignoreIfAttracted && GM.instance.player.isAttracted)
            return;

        GM.instance.player.Die();
        StartCoroutine(restart());
    }

    IEnumerator restart(){
        yield return new WaitForSeconds(1.8f);
        GM.instance.RestartLevel();
    }
}

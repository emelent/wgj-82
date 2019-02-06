using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

public class DeathZone : CollectableItem
{
    
    public override void ActivateEffect()
    {
        int index = Random.Range(1, 2);

        GM.instance.audioManager.PlaySound("PlayerDeath" + index.ToString());
        GM.instance.player.Die();
        StartCoroutine(restart());
    }

    IEnumerator restart(){
        yield return new WaitForSeconds(1.8f);
        GM.instance.RestartLevel();
    }
}

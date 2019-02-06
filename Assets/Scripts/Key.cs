using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : CollectableItem
{
    public override void ActivateEffect()
    {
        
        Player player = GM.instance.player;
        if(player != null){
            player.hasKey = true;
            GM.instance.audioManager.PlaySound("Collect");
            Destroy(gameObject);
        }
    }
}

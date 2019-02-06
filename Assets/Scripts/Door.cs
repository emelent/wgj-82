using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : CollectableItem
{
    override public void ActivateEffect() {
        
        Player player = GM.instance.player;
        if(player != null && player.hasKey){
            Destroy(gameObject);
        }
    
    }
}

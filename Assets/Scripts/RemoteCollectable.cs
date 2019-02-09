using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RemoteCollectable : CollectableItem
{
    public override void ActivateEffect()
    {
        GM.instance.audioManager.PlaySound("Collect");
        GM.instance.player.remote.GetComponent<SpriteRenderer>().enabled = true;
        Destroy(gameObject);
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CollectableItem : MonoBehaviour
{
    public string targetTag = "Player";
    void OnTriggerEnter2D(Collider2D collider) {
        if(collider.tag == targetTag){
            ActivateEffect();
        }        
    }

    public abstract void ActivateEffect();
    
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvForce : MonoBehaviour
{
    public bool isOn { get; protected set; } = false;
    public float attractSpeed = 300f;

    [Range(0.1f, 1f)]
    public float tickRate = 0.5f;
    public List<Sprite> sprites;

    public SpriteRenderer spriteRenderer;
    public SpriteRenderer indicator;

    Player player;
    // Start is called before the first frame update
    void Start()
    {
        ToggleTvForce(isOn);
    }

    void OnTriggerEnter2D(Collider2D other) {
        if(!isOn || other.tag != "Player") return;

        this.player = other.GetComponent<Player>();
        if(player != null){
            player.BeginAttract(this);
        }      
    }

    void OnTriggerExit2D(Collider2D other) {
        if(!isOn || other.tag != "Player") return;

        var p = other.GetComponent<Player>();
        if(player == p){
            player = null;
        }
    }
    void OnTriggerStay2D(Collider2D other) {
        if(!isOn || other.tag != "Player") return;

        player = other.GetComponent<Player>();

        if(player != null && !player.isAttracted){
            player.BeginAttract(this);
        }
    }
    public void ToggleTvForce(bool value){
        isOn = value;
        if(!value && player != null){
            player.StopAttract();
            player = null;
        }
        
        // nudge the tv so the trigger stay 
        // can pick up entries that were there
        // when the tv was off and are now in inactive
        this.transform.Translate(0.001f, 0f, 0f);
        // update sprite based on on state
        int index = (isOn)? 1: 0;
        indicator.enabled = isOn;
        if(sprites.Count > index)
            spriteRenderer.sprite = sprites[index];
    }
}

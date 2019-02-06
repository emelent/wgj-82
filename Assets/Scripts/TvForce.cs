using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TvForce : MonoBehaviour
{
    public bool isOn = false;
    public float attractSpeed = 300f;
    Collider2D coll2d;

    // Start is called before the first frame update
    void Awake()
    {
        coll2d = GetComponent<Collider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void OnTriggerEnter2D(Collider2D other) {
        var player = other.GetComponent<Player>();
        if(player != null){
            player.BeginAttract(this);
        }      
    }

    public void ToggleTvForce(bool value){
        coll2d.enabled = value;
    }
}

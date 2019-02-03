﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{

    public float rotationOffset = 90f;
    public RemoteReceiver receiver;
    Animator animator;

    void Start() {
        animator = GetComponent<Animator>();    
    }
	void Update () {
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition)  - transform.position;
		diff.Normalize();

		float  rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
		transform.rotation = Quaternion.Euler(0f, 0f, rotZ + rotationOffset);	
	}

    protected void remoteClick(){
        animator.Play("Click");
        GM.instance.audioManager.PlaySound("RemoteClick");
    }
    public void PressPause(){
        print("Remote: Pause");
        remoteClick();
    }
    
    public void PressPowerOff(){
        print("Remote: Power off");
        remoteClick();
    }
    
    public void PressChannelUp(){
        receiver.ChannelUp();
    }

    public void PressChannelDown(){
        receiver.ChannelDown();
    }

    
}

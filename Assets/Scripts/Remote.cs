using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{

    public float rotationOffset = 0f;
    public RemoteReceiver receiver;
    Animator animator;

    public float angle { get; protected set; }

    void Start() {
        animator = GetComponent<Animator>();    
    }
	void Update () {
		Vector3 diff = Camera.main.ScreenToWorldPoint(Input.mousePosition)  - transform.position;
		diff.Normalize();

		float  rotZ = Mathf.Atan2(diff.y, diff.x) * Mathf.Rad2Deg;
        angle = rotZ + rotationOffset;
		transform.rotation = Quaternion.Euler(0f, 0f, angle);	
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

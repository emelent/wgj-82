using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

[RequireComponent(typeof(TopDownMovement2D))]
public class Player : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Remote remote;
    TopDownMovement2D mover;
    Animator animator;
    bool isMoving = false;
    
    float scaleX;
    public float footstepDelay = 0.3f;
    float footStepTime = 0f;
    void Start()
    {
        mover = GetComponent<TopDownMovement2D>();    
        animator = GetComponent<Animator>();
        scaleX = spriteRenderer.transform.localScale.x;
    }

    // Update is called once per frame
    void Update()
    {
        // basic motion
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        var motion = new Vector2(horizontal, vertical);
        mover.Move(motion);

        // face player towards motion
        var localScale = spriteRenderer.transform.localScale;
        if(horizontal < 0){
            localScale.x = scaleX * -1;
        }else if(horizontal > 0){
            localScale.x = scaleX;
        }
        spriteRenderer.transform.localScale = localScale;

        isMoving = motion != Vector2.zero;
        animator.SetBool("moving", isMoving);
        
        // pause and unpause
        if(Input.GetKeyDown(KeyCode.P)){
            GM.instance.TogglePause();
        }

        if(Input.GetMouseButtonDown(0)){
            remote.PressPause();
        }
        float t = Time.time;
        if(isMoving && t - footStepTime > footstepDelay){
            GM.instance.audioManager.PlaySound("Step");
            footStepTime = t;
        }
    }
}

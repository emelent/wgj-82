using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

[RequireComponent(typeof(TopDownMovement2D))]
public class Player : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public Remote remote;
    
    public float footstepDelay = 0.3f;
    float footStepTime = 0f;

    public bool isJumping { get; private set; } = false;

    public Transform dustSpawnPoint;
    public GameObject DustParticles;
    TopDownMovement2D mover;
    Animator animator;
    PlayerLandSMB plb;
    
    float scaleX;
    int obstacleLayer;

    void Awake()
    {
        mover = GetComponent<TopDownMovement2D>();    
        animator = GetComponent<Animator>();
        obstacleLayer = LayerMask.NameToLayer("Obstacle");
    }

    void Start(){
        
        plb = animator.GetBehaviour<PlayerLandSMB>();
        plb.player = this as Player;
        scaleX = spriteRenderer.transform.localScale.x;
    }
    // Update is called once per frame
    void Update()
    {
        handleInput();
        // play footstep sound
        float t = Time.time;
        // if it's time for another footstep and the player is jumping
        if(mover.moving && t - footStepTime > footstepDelay && !isJumping){
            footStepTime = t;
            GM.instance.audioManager.PlaySound("Step");
            // spawn dust
            spawnDust();
        }
    }

    void handleInput(){
        // basic motion, movement is frozen while jumping
        if(!isJumping){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            var motion = new Vector2(horizontal, vertical);
            mover.Move(motion);
        }

        // face player towards mouse
        var localScale = spriteRenderer.transform.localScale;
        if(remote.angle > 90 || remote.angle < -90){
            localScale.x = scaleX * -1;
        }else{
            localScale.x = scaleX;
        }

        // play whoosh sound if player changes direction
        if(localScale != spriteRenderer.transform.localScale){
            GM.instance.audioManager.PlaySound("Whoosh");
        }
        spriteRenderer.transform.localScale = localScale;
        animator.SetBool("moving", mover.moving);
        
        // pause and unpause
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
            GM.instance.TogglePause();
        }

        // Remote action button
        if(Input.GetMouseButtonDown(0)){
            remote.PressPause();
        }

        // player can only jump while running, also you can't jump while jumping
        if((Input.GetMouseButtonDown(1) || Input.GetKeyDown(KeyCode.LeftShift)) 
            && mover.moving && !isJumping){
            print("Jumping");
            isJumping = true;
            animator.SetBool("jumping", isJumping);
            animator.Play("Jump");
            GM.instance.audioManager.PlaySound("Jump");

            // disable collisions with objects only
            Physics2D.IgnoreLayerCollision(gameObject.layer, obstacleLayer);

            // spawn dust
            spawnDust();
        }
    }

    void spawnDust(){
        if(DustParticles != null){
            Instantiate(DustParticles, dustSpawnPoint.position, dustSpawnPoint.rotation);
        }
    }
    public void Land(){
        isJumping = false;
        animator.SetBool("jumping", isJumping);
        GM.instance.audioManager.PlaySound("Step");
        print("Landing");

        // enable collisions
        Physics2D.IgnoreLayerCollision(gameObject.layer, obstacleLayer, false);

        // spawn dust
        spawnDust();
    }
}

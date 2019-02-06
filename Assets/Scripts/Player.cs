using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

[RequireComponent(typeof(TopDownMovement2D))]
public class Player : MonoBehaviour
{

    public SpriteRenderer spriteRenderer;
    public GameObject attractedIndicator;
    public Remote remote;
    
    public float footstepDelay = 0.3f;
    float footStepTime = 0f;

    public bool isJumping { get; private set; } = false;
    public bool isAttracted = false;
    public Transform dustSpawnPoint;
    public GameObject DustParticles;
    TopDownMovement2D mover;
    Animator animator;
    PlayerLandSMB plb;
    Rigidbody2D rb;
    float scaleX;
    int obstacleLayer;
    TvForce tvForce;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
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
        // if it's time for another footstep and the player is not jumping or being attracted
        if(mover.moving && t - footStepTime > footstepDelay && !isJumping){
            footStepTime = t;
            GM.instance.audioManager.PlaySound("Step");
            spawnDust();
        }
        
        if(isAttracted)
            attract();
    }

    void handleInput(){
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
        
        
        // pause and unpause
        if(Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape)){
            GM.instance.TogglePause();
        }

        // Remote action button
        if(Input.GetMouseButtonDown(0)){
            remote.PressPause();
        }

        if(isAttracted) return;

        /*
            You can't do anything below here if the player is being
            attracted (by a tv or whatever else I code in)
        */

        // basic motion, movement is frozen while jumping
        if(!isJumping && !isAttracted){
            float horizontal = Input.GetAxisRaw("Horizontal");
            float vertical = Input.GetAxisRaw("Vertical");
            var motion = new Vector2(horizontal, vertical);
            mover.Move(motion);
        }
        animator.SetBool("moving", mover.moving);
        
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

    public void BeginAttract(TvForce tvForce){
        isAttracted = true;
        mover.allowMovement = false;
        mover.Move(new Vector2(0,0));
        animator.SetBool("moving", false);
        this.tvForce = tvForce;
        attractedIndicator.SetActive(true);
    }

    void attract(){
        Vector2 dir = tvForce.transform.position - transform.position;
        rb.velocity = dir * tvForce.attractSpeed * Time.deltaTime;
    }

    public void StopAttract(){
        tvForce = null;
        isAttracted = false;
        attractedIndicator.SetActive(false);
    }
}

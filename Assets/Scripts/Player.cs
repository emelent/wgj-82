using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

[RequireComponent(typeof(TopDownMovement2D))]
public class Player : MonoBehaviour
{

    TopDownMovement2D mover;
    Animator animator;
    Vector3 localScale;
    void Start()
    {
        mover = GetComponent<TopDownMovement2D>();    
        animator = GetComponent<Animator>();
        localScale = transform.localScale;
    }

    // Update is called once per frame
    void Update()
    {
        // basic motion
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        mover.Move(new Vector2(horizontal, vertical));

        if(horizontal < 0){
            var scale = localScale;
            scale.x = localScale.x * -1;
            transform.localScale = scale;
        }else {
            transform.localScale = localScale;
        }
        
        animator.SetBool("moving", horizontal != 0 || vertical != 0);

        // pause and unpause
        if(Input.GetKeyDown(KeyCode.P)){
            GM.instance.TogglePause();
        }
    }
}

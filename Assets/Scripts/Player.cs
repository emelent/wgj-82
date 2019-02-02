using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BasicMotion;

[RequireComponent(typeof(TopDownMovement2D))]
public class Player : MonoBehaviour
{

    TopDownMovement2D mover;

    // Start is called before the first frame update
    void Start()
    {
        mover = GetComponent<TopDownMovement2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        // basic motion
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        mover.Move(new Vector2(horizontal, vertical));
        
        // pause and unpause
        if(Input.GetKeyDown(KeyCode.P)){
            GM.instance.TogglePause();
        }
    }
}

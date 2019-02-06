using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Remote : MonoBehaviour
{

    public float rotationOffset = 0f;
    public float range = 3f;
    public Transform firePoint;

    public LayerMask whatToHit;

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

    public void Click(){
        animator.Play("Click");
        GM.instance.audioManager.PlaySound("RemoteClick");

        // Raycast remote range
		Vector2 firePos = firePoint.position;
		Vector2 dir = (firePos - (Vector2) transform.position).normalized;
		RaycastHit2D hit = Physics2D.Raycast(firePos, dir, range, whatToHit);

		if(hit.collider){
			Vector2 normal = hit.normal;
			Debug.DrawLine(firePos, hit.point, Color.red);
			TvForce tvForce = hit.collider.GetComponentInParent<TvForce>();
            tvForce.ToggleTvForce(!tvForce.isOn);
		}
    }
    
}

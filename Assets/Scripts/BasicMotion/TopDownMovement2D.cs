using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BasicMotion
{
	[RequireComponent(typeof(Rigidbody2D))]
	public class TopDownMovement2D: MonoBehaviour{

		[Header("Platformer Movement")]
		public bool  allowMovement = true;
		public float movementSpeed = 200f;

		public Vector2 motion { get; protected set; } = Vector2.zero;

		public bool moving { 
			get {
				return allowMovement && motion != Vector2.zero;
			}
		}

		protected Rigidbody2D rb { get; set; }

		void Awake() {
			onAwake();
		}

		void FixedUpdate(){
			handleMovement();
		}

		protected void onAwake(){
			rb =  GetComponent<Rigidbody2D>();
		}

		protected virtual void handleMovement(){
			if(!allowMovement) return;
			rb.velocity = motion * movementSpeed * Time.deltaTime;
		}

		public void Move(Vector2 dir){
			motion = dir.normalized;
		}
	}
}
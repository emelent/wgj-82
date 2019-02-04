using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

[RequireComponent (typeof(Rigidbody2D))]
[RequireComponent (typeof(Seeker))]

public class EnemyAI : MonoBehaviour {

	public float updateRate = 2f;
	public float speed = 300f;
	public float nextWayPointDistance = 3;
	public Transform target;
	public Path path;
	public ForceMode2D fMode;

	[HideInInspector]
	public bool pathIsEnded = false;
	
	private Seeker seeker;
	private Rigidbody2D rb;
	private int  currentWaypoint = 0;
	private bool searchingForPlayer = false;

	void Start(){
		seeker = GetComponent<Seeker>();
		rb = GetComponent<Rigidbody2D>();

		if(target == null){
			if(!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}

		seeker.StartPath(
			transform.position, 
			target.position, 
			OnPathComplete
		);

		StartCoroutine(UpdatePath());
	}

	IEnumerator SearchForPlayer(){
		GameObject result = GameObject.FindGameObjectWithTag("Player");
		if(result == null){
			yield return new WaitForSeconds(0.5f);
			StartCoroutine(SearchForPlayer());
		} else {
			searchingForPlayer = false;
			target = result.transform;
			StartCoroutine(UpdatePath());
		}
	}

	IEnumerator UpdatePath(){

		if(target == null){
			if(!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			yield return false;
		}else{
			seeker.StartPath(
				transform.position, 
				target.position, 
				OnPathComplete
			);

			yield return new WaitForSeconds(1f/updateRate);
			StartCoroutine(UpdatePath());
		}
	}

	public void OnPathComplete(Path p){
		print("Path received: " + p.error);
		if(!p.error){
			path = p;
			currentWaypoint = 0;
		}
	}

	void FixedUpdate(){
		if(target == null){
			if(!searchingForPlayer){
				searchingForPlayer = true;
				StartCoroutine(SearchForPlayer());
			}
			return;
		}

		// TODO Always look at player?
		if(path == null)
			return;
		
		if(currentWaypoint >= path.vectorPath.Count){
			if(pathIsEnded)
				return;
			
			print("End of path  reached.");
			pathIsEnded = true;
			return;
		}
		pathIsEnded = false;

		Vector3 dir = path.vectorPath[currentWaypoint] - transform.position;
		dir.Normalize();
		dir *= speed * Time.fixedDeltaTime;

		//  Move the AI
		rb.AddForce(dir, fMode);

		float dist = Vector3.Distance(transform.position, path.vectorPath[currentWaypoint]);
		if(dist < nextWayPointDistance){
			currentWaypoint ++;
			return;
		}
	}
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour {

	//public Transform[] wayPoints = new Transform[2];
	public bool playerGone = false;

	const float minPathUpdateTime = 0.2f;
	const float pathUpadteMoveThreshold = 0.5f;

	public Transform target;
	public float speed = 20f;
	public float turnDst = 5;
	public float turnSpeed = 3;
	public float stoppingDst = 10;

	int targetIndex;

	public bool finished = false;

	Path path;

	void Awake (){
		//transform.position = wayPoints [0].position;
		//target = wayPoints [1];
	}

	void Start(){
		StartCoroutine (UpdatePath ());
	}

	public void SetPlayerGone(bool value){
		playerGone = value;
	}

	public void SetTarget(Transform target){
		this.target = target;
	}


	public void OnPathFound(Vector3[] wayPoints, bool pathSuccessful){
		if (pathSuccessful) {
			path = new Path(wayPoints, transform.position, turnDst, stoppingDst);
			StopCoroutine ("FollowPath");
			StartCoroutine ("FollowPath");
		}
	}

	IEnumerator UpdatePath(){

		if (Time.timeSinceLevelLoad < 0.3f) {
			yield return new WaitForSeconds (0.3f);
		}
		PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));

		float sqrMoveThreshold = pathUpadteMoveThreshold * pathUpadteMoveThreshold;
		Vector3 targetPosOld = target.position;

		while (true) {
			yield return new WaitForSeconds (minPathUpdateTime);
			if ((target.position - targetPosOld).sqrMagnitude > sqrMoveThreshold) {
				PathRequestManager.RequestPath (new PathRequest(transform.position, target.position, OnPathFound));
				targetPosOld = target.position;
			}
		}
	}

	IEnumerator FollowPath(){

		bool followingPath = true;
		int pathIndex = 0;

		if (target.tag != "Player") {
			Quaternion newRotation = Quaternion.Euler (new Vector3 (0f, path.lookPoints [0].y, 0f));
			transform.rotation = Quaternion.Slerp (transform.rotation, newRotation, 0.2f);
			//transform.LookAt (path.lookPoints [0]);
		}

		float speedPercent = 1;

		while (followingPath) {
			Vector2 pos2D = new Vector2 (transform.position.x, transform.position.z);
			while (path.turnBoundaries [pathIndex].HasCrossedLine (pos2D)) {
				if (pathIndex == path.finishLineIndex) {
					followingPath = false;
					break; 
				} else {
					pathIndex++;
				}
			}

			if (followingPath) {

				if (playerGone && target.gameObject.tag == "Player") {
					followingPath = false;
					playerGone = false;
					break;
				}

				if (target.gameObject.tag == "Player" && pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}
					
			
					Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
					transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);

				if (target.tag == "Player") {
					CalculateSteerStrenght ();
				}
				transform.Translate (Vector3.forward * Time.deltaTime * speed * speedPercent, Space.Self);
			}


			yield return null;
		}

		finished = true;

	}

	public bool Finished(){
		return finished;
	}

	public void SetFinished(){
		finished = false;
	}


	public void OnDrawGizmos(){
		if (path != null) {
			path.DrawWithGizmos (); 
		}
	}

	//seek
	public 	float	Mass			= 1f;
	public	float	MaxStrengh		= 0.1f;
	public	float	MaxVelocity		= 0.2f;
	public	float	TickFixedTime 	= 0.01f;

	public 	Vector3 Velocity;
	public 	Vector3	SteerStrengh;
	public Vector3 desiredVelocity;

	public void CalculateSteerStrenght(){
		desiredVelocity = (target.position - transform.position).normalized * MaxVelocity;
		SteerStrengh = desiredVelocity - Velocity;

		Velocity = maxVector (Velocity + SteerStrengh * (1.0f / Mass), MaxVelocity);
		transform.position += Velocity;
	}

	private Vector3 maxVector(Vector3 v, float c){
		if (v.magnitude > c)
			return v.normalized * c;
		return v;
	}
		
}

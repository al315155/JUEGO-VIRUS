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

		//finished = false;
		bool followingPath = true;
		int pathIndex = 0;

		if (target.tag != "Player") {
			transform.LookAt (path.lookPoints [0]);
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
					Debug.Log (target.tag);
					followingPath = false;
					playerGone = false;
					break;
				}

				if (pathIndex >= path.slowDownIndex && stoppingDst > 0) {
					speedPercent = Mathf.Clamp01 (path.turnBoundaries [path.finishLineIndex].DistanceFromPoint (pos2D) / stoppingDst);
					if (speedPercent < 0.01f) {
						followingPath = false;
					}
				}
					
			
					Quaternion targetRotation = Quaternion.LookRotation (path.lookPoints [pathIndex] - transform.position);
					transform.rotation = Quaternion.Lerp (transform.rotation, targetRotation, Time.deltaTime * turnSpeed);
				
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
}

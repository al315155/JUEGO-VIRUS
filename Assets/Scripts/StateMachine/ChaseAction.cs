using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Chase")]
public class ChaseAction : Action {

	public override void Act (StateController controller){
		Chase (controller);
	}

	private void Chase(StateController controller){

		Debug.Log ("entro en chase action");
		if (controller.isPlayerHeard && !controller.isPlayerOnSight) {
			controller.GetComponent<Unit> ().SetTarget (controller.GetComponent<EnemyDetection> ().lastPoint);
		} else {
			controller.GetComponent<Unit> ().SetTarget (controller.player.GetComponent<Transform> ());
		}
	}
}


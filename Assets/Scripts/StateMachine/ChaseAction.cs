using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Actions/Chase")]
public class ChaseAction : Action {

	public override void Act (StateController controller){
		Chase (controller);
	}

	private void Chase(StateController controller){
		//controller.navMeshAgent.destination = controller.chaseTarget.position;
		//controller.navMeshAgent.isStopped = false;

		//pasarle al pathfinding la posicion del jugador

		if (controller.isPlayerHeard && !controller.isPlayerOnSight) {
			controller.GetComponent<Unit> ().SetTarget (controller.GetComponent<EnemyDetection> ().lastPoint);
		} else {

			controller.GetComponent<Unit> ().SetTarget (controller.player.GetComponent<Transform> ());
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/ActiveState")]
public class ActiveStateDecision : Decision {

	public override bool Decide (StateController controller){

		if (controller.pState == StateController.pursuitState.SCAPED) {
			controller.pathfining.SetPlayerGone (true);
			controller.pState = StateController.pursuitState.PATROL;
			return false;

		} else {
			if (controller.isPlayerOnSight)
			{
				controller.chaseTarget = controller.player;
			}

			if (controller.isPlayerOnSight) controller.pState = StateController.pursuitState.FOLLOWING;

			return controller.isPlayerOnSight || controller.isPlayerHeard;
		}
	}
}

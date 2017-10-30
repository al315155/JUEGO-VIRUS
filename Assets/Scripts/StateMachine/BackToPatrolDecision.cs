using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName = "AI/Decisions/BackToPatrol")]
public class BackToPatrolDecision : Decision {
	
	public override bool Decide(StateController controller)
	{
		if (controller.navMeshAgent.remainingDistance >= 70) {
			return true;
		} else {
			return false;
		}
	}

}

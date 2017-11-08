using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(menuName = "AI/Decision/Look")]
public class LookDecision : Decision {

	public override bool Decide (StateController controller)
	{
		bool targetVisible = Look (controller);
		return targetVisible;
	}

	private bool Look(StateController controller)
	{
		RaycastHit hit;
		Debug.DrawRay (controller.eyes.position, controller.eyes.forward.normalized * controller.enemeyStats.lookRange, Color.green);

		if (Physics.SphereCast (controller.eyes.position, controller.enemeyStats.lookSphereCastRadius, controller.eyes.forward, out hit, controller.enemeyStats.lookRange) && hit.collider.CompareTag ("Player")) {
			controller.chaseTarget = hit.transform;
			return true;
		} else {
			return false;
		}
	}


}

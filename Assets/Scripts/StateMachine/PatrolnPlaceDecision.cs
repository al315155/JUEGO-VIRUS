using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	[CreateAssetMenu(menuName = "AI/Decision/PatrolInPlace")]
	public class PatrolnPlaceDecision : Decision
	{
		public override bool Decide(StateController controller)
		{
			controller.patrolIsActive = false;

			if (controller.pathfining.Finished ()&& controller.patrolIsActive==false) {
				controller.BeginPasses ();
				controller.leftVector = controller.transform.rotation.eulerAngles + new Vector3(0, -90f, 0);
				controller.rightVector = controller.transform.rotation.eulerAngles + new Vector3(0, 90f, 0);

				controller.destination = controller.leftVector;
				controller.patrolIsActive = true;
				return true;
			}
			return false;
		}
	}
}


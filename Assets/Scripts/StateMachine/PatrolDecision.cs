using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AssemblyCSharp
{
	[CreateAssetMenu(menuName = "AI/Decision/Patrol")]
	public class PatrolDecision : Decision
	{

		public override bool Decide(StateController controller)
		{
			if (controller.CountPasses () >= controller.Pases) {
				if (controller.pathfining.Finished ())
					controller.changePoint = true;
				return true;
			}

			return false;
		}
	}
}


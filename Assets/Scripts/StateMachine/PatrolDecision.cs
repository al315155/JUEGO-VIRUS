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
				Debug.Log (controller.CountPasses ());
				Debug.Log (controller.Pases);

				controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
				//controller.pathfining.SetFinished ();
				Debug.Log("return true deberia cambiar");
				return true;
			} else {
				Debug.Log ("return false");
				return false;
			}
		}
	}
}


using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu (menuName="AI/Actions/Patrol")]
public class PatrolAction : Action {

	public override void Act (StateController controller)
	{
		Patrol (controller);  
	}

	private void Patrol (StateController controller)
	{
		if (controller.changePoint) {
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
			controller.pathfining.SetFinished ();
			controller.changePoint = false;
		}
			
		controller.pathfining.SetTarget(controller.wayPointList[controller.nextWayPoint].transform);
	}
		
}

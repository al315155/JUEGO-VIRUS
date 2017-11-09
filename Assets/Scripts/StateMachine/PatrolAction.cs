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
		controller.pathfining.SetTarget(controller.wayPointList[controller.nextWayPoint].transform);
	}
		
}

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
		//controller.navMeshAgent.destination = controller.wayPointList [controller.nextWayPoint].position;
		//controller.navMeshAgent.isStopped = false;

		/*if (controller.navMeshAgent.remainingDistance <= controller.navMeshAgent.stoppingDistance && !controller.navMeshAgent.pathPending) {
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
		}*/

		//controller.pathfining.SetTarget (controller.pathfining.wayPoints [1].transform);

		if (controller.pathfining.Finished()) {
			controller.nextWayPoint = (controller.nextWayPoint + 1) % controller.wayPointList.Count;
			controller.pathfining.SetFinished ();
		}

		controller.pathfining.SetTarget(controller.wayPointList[controller.nextWayPoint].transform);
	}
		
}

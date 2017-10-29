using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public State currentState;
	public Transform eyes;
	//public State remainState;
	public List<Transform> wayPointList;

	[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;

	void Awake()
	{
		navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = true;
	}

	void Update()
	{
		currentState.UpdateState (this);
	}

	void OnDrawGizmos()
	{
		if (currentState != null && eyes != null) {
			Gizmos.color = currentState.sceneGizmoColor;
			//cambiar radio;
			Gizmos.DrawWireSphere(eyes.position,1f);
		}
	}


}

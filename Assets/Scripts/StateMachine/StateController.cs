using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public Unit pathfining;

	public State currentState;
	public State remainState;
	public pursuitState pState;
    public string currentStateName;

	//Sensors
	public Transform eyes;
	public EnemyStats enemeyStats;

	public List<Transform> wayPointList;
    public bool isPlayerOnSight;
    public bool isPlayerHeard;
    public bool playerHasFled;
    public Transform player;
    //public float acceleration_speed;
    public float basic_speed;

	//[HideInInspector] public NavMeshAgent navMeshAgent;
	[HideInInspector] public int nextWayPoint;
	[HideInInspector] public Transform chaseTarget;

	void Awake()
	{
		chaseTarget = null;

		pathfining = GetComponent<Unit> ();

		player = GameObject.FindGameObjectWithTag("Player").transform;

		pState = pursuitState.PATROL;
		currentStateName = "PatrolChaser";

		playerHasFled = false;
		isPlayerOnSight = false;



		/*navMeshAgent = GetComponent<NavMeshAgent> ();
		navMeshAgent.enabled = true;
        acceleration_speed = navMeshAgent.speed * 2f;
        basic_speed = navMeshAgent.speed;*/
	}

	void Update()
	{
        //TODO: Cambiar esto con listeners para que sea más eficiente.
        //if (isPlayerOnSight || isPlayerHeard) navMeshAgent.speed = acceleration_speed;
       //else navMeshAgent.speed = basic_speed;

		currentState.UpdateState (this);
	}

	void OnDrawGizmos()
	{
		if (currentState != null && eyes != null) {
			Gizmos.color = currentState.sceneGizmoColor;

			Gizmos.DrawWireSphere(eyes.position,enemeyStats.lookSphereCastRadius);
		}
	}

	public void TrasitionToState (State nextState)
	{
		if (nextState != remainState) {
			currentState = nextState;
            currentStateName = nextState.name;
		}
	}

    public enum pursuitState { PATROL, FOLLOWING, ALERT, SCAPED }
}

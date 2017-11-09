using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StateController : MonoBehaviour {

	public Vector3 destination;
	private int finishedPasses;
	public Vector3 leftVector;
	public Vector3 rightVector;
	public bool changePoint = false;
	public bool patrolIsActive = true;

	public Unit pathfining;
	public int Pases;

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


	public void AddPass(){ finishedPasses += 1; }
	public int CountPasses(){ return finishedPasses; }
	public void BeginPasses(){finishedPasses = 0;}


	void Awake()
	{
		finishedPasses = 0;

		chaseTarget = null;

		pathfining = GetComponent<Unit> ();

		player = GameObject.FindGameObjectWithTag("Player").transform;

		pState = pursuitState.PATROL;
		currentStateName = "PatrolChaser";

		playerHasFled = false;
		isPlayerOnSight = false;
	}

	void Update()
	{
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

    public enum pursuitState { PATROL, FOLLOWING, ALERT, SCAPED, PATROLINPLACE }
}

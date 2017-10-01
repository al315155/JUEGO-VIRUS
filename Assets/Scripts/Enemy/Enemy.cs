using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    [Tooltip("Initial position where enemy will spawn")]
    [SerializeField]
    private Vector3 spawnPosition;

    [Tooltip("Movement speed in any direction")]
    [SerializeField]
    private float movementSpeed;

    [Tooltip("Movement speed in any direction")]
    [SerializeField]
    private EnemyState enemState;

    void Start () {
        gameObject.transform.position = spawnPosition;
	}
	
	// Update is called once per frame
	void Update () {
        switch (enemState)
        {
            case EnemyState.ALERT:
                //TODO State
                break;
            case EnemyState.ATTACKING:
                //TODO State
                break;
            case EnemyState.DEAD:
                //TODO State
                break;
            case EnemyState.IDLE:
                //TODO State
                break;
            case EnemyState.MOVING:
                //TODO State
                break;
            case EnemyState.PATROL:
                //TODO State
                break;
            case EnemyState.PURSUIT:
                //TODO State
                break;
        }
	}
}

public enum EnemyState
{
    IDLE, MOVING, PATROL, PURSUIT, ALERT, ATTACKING, DEAD
}

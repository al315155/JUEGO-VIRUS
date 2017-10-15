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

	public float RotationSpeed;
	public float x, y, z;
	public int dirX, dirY, dirZ;

	void Awake()
	{
		Vector3 angles = transform.eulerAngles;

		if (Random.Range (-1, 1) < 0) {dirX = -1;} else {dirX = 1;}
		if (Random.Range (-1, 1) < 0) {dirY = -1;} else {dirY = 1;}
		if (Random.Range (-1, 1) < 0) {dirZ = -1;} else {dirZ = 1;}

		x = angles.x * dirX;
		y = angles.y * dirY;
		z = angles.z * dirZ;
	}

    void Start () {

		//Inicialmente todos los scripts de comportamiento están deshabilitados
		//Actualizamos el estado de enemigo a IDLE
		//Y habilitamos el Script correspondiente
		enemState = EnemyState.IDLE;
		GetComponent<EnemyIdle> ().enabled = true;

        //gameObject.transform.position = spawnPosition;
	}
	
	// Update is called once per frame
	void Update () {

        /*switch (enemState)
        {
            case EnemyState.ALERT:
                //TODO State
                break;

		case EnemyState.ATTACKING:
			GetComponent<EnemyAttack> ().enabled = true;
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
        }*/
	}

	public void ChangeState(EnemyState state)
	{	
		//Deshabilito el Script del comportamiento actual
		switch (enemState) 
		{
		case EnemyState.ALERT:
			break;

		case EnemyState.ATTACKING:
			GetComponent<EnemyAttack> ().enabled = false;
			break;

		case EnemyState.DEAD:
			break;

		case EnemyState.IDLE:
			GetComponent<EnemyIdle> ().enabled = false;
			break;

		case EnemyState.MOVING:
			break;

		case EnemyState.PATROL:
			break;

		case EnemyState.PURSUIT:
			break;
		}

		//Y habilito el Script del nuevo comportamiento
		switch (state) 
		{
		case EnemyState.ALERT:
			break;

		case EnemyState.ATTACKING:
			GetComponent<EnemyAttack> ().enabled = true;
			break;

		case EnemyState.DEAD:
			break;

		case EnemyState.IDLE:
			GetComponent<EnemyIdle> ().enabled = true;
			break;

		case EnemyState.MOVING:
			break;

		case EnemyState.PATROL:
			break;

		case EnemyState.PURSUIT:
			break;
		}

		//Actualizo la variable del estado del enemigo al nuevo
		enemState = state;
	}

	public void RotateInPlace()
	{
		x += Time.deltaTime * RotationSpeed * dirX;
		y += Time.deltaTime * RotationSpeed * dirY;
		z += Time.deltaTime * RotationSpeed * dirZ;

		if (Mathf.Abs(x) > 360f) x /= 360f;
		if (Mathf.Abs(y) > 360f) y /= 360f;
		if (Mathf.Abs(z) > 360f) z /= 360f;


		transform.rotation = Quaternion.Euler (x, y, z);
	}
}

public enum EnemyState
{
    IDLE, MOVING, PATROL, PURSUIT, ALERT, ATTACKING, DEAD
}



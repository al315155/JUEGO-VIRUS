using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seek : SteerAgent {

	public Transform target;
	public Vector3 desiredVelocity;

	public override void CalculateSteerStrenght(){
		Debug.Log("llego");
		desiredVelocity = (target.position - transform.position).normalized * MaxVelocity;
		SteerStrengh = desiredVelocity - Velocity;
	}

	void Update(){
		UpdateObjective ();
		UpdatePosition ();
	}

}
